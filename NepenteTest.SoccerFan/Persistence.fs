module Persistence

open System.Data.Linq
open System.Data.Entity
open Microsoft.FSharp.Data.TypeProviders
open Domain
open Domain.Address
open Domain.Dependent
open Domain.Titular
open Infrastructure

[<LiteralAttribute>]
let connectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\NepenteTest.SoccerFan\NepenteTest.SoccerFan.Database\Database.mdf;Integrated Security=True"

type private EntityConnection = SqlEntityConnection<ConnectionString=connectionString,
                                                    Pluralize = true>

let private commit func =
    try 
        let context = EntityConnection.GetDataContext()
        func context
        context.DataContext.SaveChanges() |> ignore
        Success
    with ex -> Error [| ex.Message |]

let private addressReferenceToModel (reference : EntityConnection.ServiceTypes.Address )= 
         { Street = reference.Street; 
           Number = reference.Number; 
           City = reference.City; 
           PostCode = reference.PostCode;
           State = reference.State;
           Neighborhood = reference.Neighborhood}

module internal address =
    let private getReference id =
        query{ for address in EntityConnection.GetDataContext().Addresses do
               where (address.Id = id)
               select address} |> Seq.head

    let getAddress id = getReference id |> addressReferenceToModel
    
    let getByTitularId titularId = 
        query { for address in EntityConnection.GetDataContext().Addresses do
                where (address.TitularId = titularId)
                select address } |> Seq.head |>  addressReferenceToModel

let private dependentReferenceToModel (reference : EntityConnection.ServiceTypes.Dependent ) : Dependent.Data = 
         { FirstName = reference.FirstName; 
           LastName = reference.LastName; 
           Email = reference.Email; 
           CPF = reference.CPF;
           TitularId = reference.TitularId }

module internal dependent =     
    let getByTitularId titularId =
        query { for dependent in EntityConnection.GetDataContext().Dependents do
                where (dependent.TitularId = titularId)
                select dependent } |> Seq.map dependentReferenceToModel

module titular = 
    let private getTitularReference (context : EntityConnection.ServiceTypes.SimpleDataContextTypes.EntityContainer) id =
        query{ for titular in context.Titulars do
               where (titular.Id = id)
               select titular} |> Seq.head

    let private referenceToModel (ref : EntityConnection.ServiceTypes.Titular) = 
        {FirstName =  ref.FirstName; 
         LastName = ref.LastName; 
         Phone = ref.Phone; 
         Email = ref.Email; 
         BirthDate = ref.BirthDate;
         CPF = ref.CPF;
         Address = ref.Address |> addressReferenceToModel;
         Dependents = ref.Dependents |> Seq.map dependentReferenceToModel }

    let private modelToReference (model : SaveCommand) =        
        new EntityConnection.ServiceTypes.Titular(FirstName = model.FirstName,
                                                  LastName = model.LastName,
                                                  Phone = model.Phone, 
                                                  Email = model.Email, 
                                                  BirthDate = model.BirthDate,
                                                  CPF = model.CPF)

    let create (titular : SaveCommand) = 
        commit (fun (context) -> let reference = titular |> modelToReference
                                 context.Titulars.AddObject(reference))
    
    let update id (titular : SaveCommand) = 
        commit (fun (context) -> let ref = getTitularReference context id
                                 ref.BirthDate <- titular.BirthDate
                                 ref.CPF <- titular.CPF
                                 ref.Email <- titular.Email
                                 ref.FirstName <- titular.FirstName
                                 ref.LastName <- titular.LastName
                                 ref.Phone <- titular.Phone)
    
    let getTitulares() =
        let context = EntityConnection.GetDataContext()
        context.Titulars |> Seq.map referenceToModel

    let getTitular id =
        let address = address.getByTitularId id
        let dependents = dependent.getByTitularId id
        let context = EntityConnection.GetDataContext()

        getTitularReference context id  |> fun (x) -> {FirstName =  x.FirstName; 
                                                       LastName = x.LastName; 
                                                       Phone = x.Phone; 
                                                       Email = x.Email; 
                                                       BirthDate = x.BirthDate;
                                                       CPF = x.CPF;
                                                       Address = address;
                                                       Dependents = dependents }
            