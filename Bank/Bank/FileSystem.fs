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

let loadTransactions customer path =
    let accountDirs = Directory.GetDirectories path
    let accountId = Path.GetFileName accountDirs.[0]

    let existingAccount = {
        Id = Guid.Parse accountId
        Customer = customer
        Balance = 0.0
    }
    
    let accountPath = sprintf "%s/%s" path accountId
    
    let filledAccount =
        Directory.EnumerateFiles accountPath
        |> Seq.map (File.ReadAllText >> deserialize)
        |> Seq.sortBy (fun txn -> txn.Timestamp)
        |> Seq.fold (fun account (txn : Transaction) ->
            match txn.Action with
            | Withdraw -> withdraw existingAccount txn.Amount
            | Deposit -> deposit account txn.Amount) existingAccount
    
    filledAccount

let loadAccountFromDisk customer =
    let path = buildCustomerDirectory customer
    
    if Directory.Exists path then
        Some (loadTransactions customer path)
    else
        None
