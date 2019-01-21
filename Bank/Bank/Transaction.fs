module Transaction

open System

type Transaction = {
    Timestamp: DateTime
    Action: string
    Amount: float
    Succeeded: bool
}

let createTransaction amount action = {
    Timestamp = DateTime.UtcNow
    Action = action
    Amount = amount
    Succeeded = true
}    
    
let serialize transaction =
    sprintf "%O***%s***%0.2f***%b"
        transaction.Timestamp
        transaction.Action
        transaction.Amount
        transaction.Succeeded
        
let deserialize (contents : string) =
    let chunks = contents.Split([|"***"|], StringSplitOptions.None)
    
    {Timestamp = DateTime.Parse chunks.[0]
     Action = chunks.[1]
     Amount = float chunks.[2]
     Succeeded = Boolean.Parse chunks.[3]}