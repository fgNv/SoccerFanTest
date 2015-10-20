module Infrastructure

open System

type OperationResult = | Success
                       | Error of seq<String>

let rec getExceptionMessages (ex : Exception) =
    seq{
        if ex.InnerException <> null then 
            for inner in getExceptionMessages ex.InnerException  
                do yield inner           
        yield ex.Message
    }

