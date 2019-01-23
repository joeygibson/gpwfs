module Journal

open Account
open FileSystem
open System.IO
open Transaction

let fileSystemJournal account transaction =
    let dir = buildPath account
    dir |> Directory.CreateDirectory |> ignore
    
    let filePath = sprintf "%s/%d.txt" dir (transaction.Timestamp.ToFileTimeUtc())
    
    System.IO.File.WriteAllText(filePath, (serialize transaction))
    
let consoleJournal account transaction =
    printfn "%s" (serialize transaction)
      
let journalAs (transaction : Transaction) (journal: Account -> Transaction -> unit)
    (op : float -> RatedAccount -> RatedAccount) (amount: float) (account: RatedAccount) : RatedAccount =
        let updatedAccount = op amount account 
        
        journal (updatedAccount.GetField (fun a -> a)) transaction
        
        updatedAccount
        
let composedJournal =
    let loggers =
        [fileSystemJournal
         consoleJournal]
        
    fun account transaction ->
        loggers
        |> List.iter (fun logger -> logger account transaction)

