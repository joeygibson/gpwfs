module Journal
open Account
open System
open System.IO

let formatMessage account message =
    sprintf "%s - %-8s - %s - Balance: %.2f" (DateTime.UtcNow.ToString()) message
        (account.Id.ToString()) (account.Balance)
        
let fileSystemJournal account message =
    let dirName = sprintf "/tmp/bank/%s_%s" account.Customer.FirstName account.Customer.LastName
    
    Directory.CreateDirectory(dirName) |> ignore
    let filePath = sprintf "%s/%O.txt" dirName account.Id
    
    let msg = sprintf "%s\n" (formatMessage account message)
    
    System.IO.File.AppendAllText(filePath, msg)
    
let consoleJournal account message =
    printfn "%s" (formatMessage account message)
      
let journalAs (opName : string) (journal: Account -> string -> unit)
    (op : Account -> float -> Account) (amount: float) (account: Account) : Account =
        let updatedAccount = op account amount
        
        journal updatedAccount opName
        
        updatedAccount
        
        

