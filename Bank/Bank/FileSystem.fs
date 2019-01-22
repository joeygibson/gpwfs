module FileSystem

open System
open Account
open Command
open Customer
open System.IO
open Transaction

let private accountsPath =
    let path = @"/tmp/Bank"
    Directory.CreateDirectory path |> ignore
    path
    
let buildCustomerDirectory (customer : Customer) =
    let customerName = sprintf "%s_%s" customer.LastName customer.FirstName
    
    sprintf @"%s/%s" (accountsPath) customerName
    
let buildPath (account:Account) =
    let customerDir = buildCustomerDirectory account.Customer
    sprintf @"%s/%O" customerDir account.Id

let loadTransactions account path =
    let accountDirs = Directory.GetDirectories path
    let accountId = Path.GetFileName accountDirs.[0]
    
    let existingAccount = {
        Id = Guid.Parse accountId
        Customer = account.Customer
        Balance = 0.0
    }
    
    let accountPath = sprintf "%s/%s" path accountId
    
    let filledAccount =
        Directory.EnumerateFiles accountPath
        |> Seq.map (File.ReadAllText >> deserialize)
        |> Seq.sortBy (fun txn -> txn.Timestamp)
        |> Seq.fold (fun account (txn : Transaction) ->
            if txn.Action = "withdraw" then withdraw existingAccount txn.Amount
            else deposit account txn.Amount) existingAccount
    
    filledAccount
    
let loadOrCreate customer =
    let initialAccount = newAccount customer 0.0
    let path = buildCustomerDirectory initialAccount.Customer
    
    if Directory.Exists path then
        loadTransactions initialAccount path
    else
        initialAccount
