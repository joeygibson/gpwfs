module Journal

open Account
open System
open System.IO

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
        
let fileSystemJournal account transaction =
    let dirName = sprintf "/tmp/bank/%s_%s" account.Customer.FirstName account.Customer.LastName
    
    Directory.CreateDirectory(dirName) |> ignore
    let filePath = sprintf "%s/%O.txt" dirName account.Id
    
    System.IO.File.AppendAllText(filePath, (serialize transaction))
    
let consoleJournal account transaction =
    printfn "%s" (serialize transaction)
      
let journalAs (transaction : Transaction) (journal: Account -> Transaction -> unit)
    (op : Account -> float -> Account) (amount: float) (account: Account) : Account =
        let updatedAccount = op account amount
        
        journal updatedAccount transaction
        
        updatedAccount
        
        

