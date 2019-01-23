module Account

open Customer

type Account = 
    {Id : System.Guid
     Customer : Customer
     Balance : float}

type CreditAccount = CreditAccount of Account
type RatedAccount =
    | Credit of CreditAccount
    | Overdrawn of Account
    member this.GetField getter =
        match this with
        | Credit (CreditAccount account) -> getter account
        | Overdrawn account -> getter account

let classifyAccount account =
    if account.Balance >= 0.0 then (Credit(CreditAccount account))
    else Overdrawn account

let newAccount customer (openingBalance : float) =
    { Id = System.Guid.NewGuid()
      Customer = customer
      Balance = openingBalance }

let deposit (amount : float) account =
    if amount < 0. then
        failwith "Amount less than 0"

    let account =
        match account with
        | Credit (CreditAccount account) -> account
        | Overdrawn account -> account
        
    { account with Balance = (account.Balance + amount) }
    |> classifyAccount

let withdraw (amount : float) (CreditAccount account) =
    { account with Balance = account.Balance - amount }
    |> classifyAccount

let withdrawSafe amount ratedAccount =
    match ratedAccount with
    | Credit account -> account |> withdraw amount
    | Overdrawn _ ->
        printfn "Your account is overdrawn."
        ratedAccount

let printBalance (account : RatedAccount) =
    printfn "Balance: %.2f" (account.GetField (fun a -> a.Balance))


