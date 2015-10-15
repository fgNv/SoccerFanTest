module Domain

open System
open Infrastructure

let private Create<'T> (validate : 'T -> OperationResult)
                       (persist : ('T -> OperationResult))
                        data 
                        =
        match validate data with
        | Success -> persist data
        | Error err -> Error err

let private Update<'T> (validate : 'T -> OperationResult)                       
                       (persist : (int -> 'T -> OperationResult))
                       id 
                       data  =
        match validate data with
        | Success -> persist id data 
        | Error err -> Error err

let private Remove id (remove : (int -> OperationResult)) =        
        remove id

let private validate<'T> (validate : ('T -> seq<string>)) data =
    let errors = validate data
    match errors |> Seq.length with 
        | 0 -> Success
        | _ -> Error errors   



module Address = 
    [<CLIMutable>]
    type Data = {Street : String; Number : int;  City : string;  PostCode : String;
                    State : string; Neighborhood : string}

    let private getErrors address = 
            seq {  if String.IsNullOrWhiteSpace address.Street then 
                        yield "Informe a rua do endereço" 
                   if address.Number <= 0 then 
                        yield "O número deve ser maior que 0" 
                   if String.IsNullOrWhiteSpace address.City then 
                        yield "Informe a cidade" 
                   if String.IsNullOrWhiteSpace address.PostCode then 
                        yield "Informe o CEP" }

    let validate = validate getErrors    

type Plan = {Name : String; Price : double}

module Dependent = 
    type Data = {FirstName : String;  LastName : String;  CPF : String;  
                 Email : String; TitularId : int}
    
    let private getErrors dependent = 
            seq {  if String.IsNullOrWhiteSpace dependent.FirstName then 
                        yield "Informe o nome do dependente" 
                   if String.IsNullOrWhiteSpace dependent.LastName then 
                        yield "Informe o sobrenome do dependente" 
                   if String.IsNullOrWhiteSpace dependent.CPF then 
                        yield "Informe o CPF do dependente" 
                   if String.IsNullOrWhiteSpace dependent.Email then 
                        yield "Informe o Email do dependente" }

    let validate = validate getErrors    

module Titular =      
    [<CLIMutable>]
    type SaveCommand = {FirstName : string; LastName : string;  Phone : string;  
                        Email : string; BirthDate : DateTime;  CPF : string;
                        Address : Address.Data  }
    

    type Data = {FirstName : string; LastName : string;  Phone : string;  Email : string; 
                 BirthDate : DateTime;  CPF : String; Address : Address.Data; 
                 Dependents : seq<Dependent.Data> }
    
    let private getErrors (titular : SaveCommand) = 
            seq {  if String.IsNullOrWhiteSpace titular.FirstName then 
                        yield "Informe o nome do titular" 
                   if String.IsNullOrWhiteSpace titular.LastName then 
                        yield "Informe o sobrenome do tituar" 
                   if String.IsNullOrWhiteSpace titular.CPF then 
                        yield "Informe o CPF do titular" 
                   if String.IsNullOrWhiteSpace titular.Phone then 
                        yield "Informe o CPF do titular" 
                   if titular.BirthDate = DateTime.MinValue then 
                        yield "Informe a data de nascimento do titular" 
                   if String.IsNullOrWhiteSpace titular.Email then 
                        yield "Informe o Email do titular"

                   for error in match Address.validate titular.Address with
                                    | Success -> Seq.empty<String>
                                    | Error errors -> errors 
                     do yield error
                }

    let validate = validate getErrors 
    let create = Create validate
    let update = Update validate
    let remove = Remove