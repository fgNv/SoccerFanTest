namespace NepenteTest.SoccerFan.Interop


type TitularService() =
    member x.Create(data) = Domain.Titular.create Persistence.titular.create data
    member x.Update(id, data) = Domain.Titular.update Persistence.titular.update id data
    member x.GetAll() = Persistence.titular.getTitulares()

