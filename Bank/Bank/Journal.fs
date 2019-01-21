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
        
let private accountsPath =
    let path = @"/tmp/Bank"
    Directory.CreateDirectory path |> ignore
    path
    
let private buildPath account =
    let customerName = sprintf "%s_%s" account.Customer.LastName account.Customer.FirstName
    sprintf @"%s/%s_%O" (accountsPath) customerName account.Id
        
let fileSystemJournal account transaction =
    let dir = buildPath account
    dir |> Directory.CreateDirectory |> ignore
    
    let filePath = sprintf "%s/%d.txt" dir (transaction.Timestamp.ToFileTimeUtc())
    
    System.IO.File.WriteAllText(filePath, (serialize transaction))
    
let consoleJournal account transaction =
    printfn "%s" (serialize transaction)
      
let journalAs (transaction : Transaction) (journal: Account -> Transaction -> unit)
    (op : Account -> float -> Account) (amount: float) (account: Account) : Account =
        let updatedAccount = op account amount
        
        journal updatedAccount transaction
        
        updatedAccount
        
        

