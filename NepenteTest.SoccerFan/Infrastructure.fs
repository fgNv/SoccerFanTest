module Infrastructure

open System

type OperationResult = | Success
                       | Error of seq<String>


