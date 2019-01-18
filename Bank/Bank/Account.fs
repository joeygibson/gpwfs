module Account

open Customer

type Account = {
    Customer : Customer
    Balance : float
 }

let newAccount customer (openingBalance: string) =
    let balance = float openingBalance

    { Customer = customer
      Balance = balance }
    
let deposit account (amount: float) =
    if amount < 0. then
        failwith "Amount less than 0"

    {account with Balance = (account.Balance + amount)}
    
let withdraw account amount =
    if amount > account.Balance then
        failwith "Insufficient funds"
        
    {account with Balance = (account.Balance - amount)}
    
let printBalance account =
    printfn "Balance: %.2f" account.Balance
    