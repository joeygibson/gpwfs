module Transaction

open Command
open System

type Transaction = {
    Timestamp: DateTime
    Action: BankCommand
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
    sprintf "%O***%A***%0.2f***%b"
        transaction.Timestamp
        transaction.Action
        transaction.Amount
        transaction.Succeeded
        
let deserialize (contents : string) =
    let chunks = contents.Split([|"***"|], StringSplitOptions.None)
    
    {Timestamp = DateTime.Parse chunks.[0]
     Action = match (tryParseSerializedCommand chunks.[1]) with
                 | Some command -> command
                 | None -> failwithf "Invalid action: %s" chunks.[1]
     Amount = float chunks.[2]
     Succeeded = Boolean.Parse chunks.[3]}