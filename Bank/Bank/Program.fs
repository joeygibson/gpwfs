module Bank

open Account
open System

type Action =
    | Deposit
    | Withdrawal
    | Exit

let getName() =
    printf "Name: "
    Console.ReadLine()

let getBalance() =
    printf "Opening balance: "
    Console.ReadLine()

let getAction() =
    printf "Deposit or Withdraw: "

    let input = Console.ReadLine()

    match input with
        | "deposit" | "Deposit" | "d" -> Deposit
        | "withdraw" | "Withdraw" | "w" -> Withdrawal
        | "exit" | "exit" -> Exit
        | _ -> failwithf "Invalid action: %s" input

let processDeposit account =
    printf "Amount to deposit: "
    let amount = Console.ReadLine()

    deposit account (float amount)

let processWithdrawal account =
    printf "Amount to withdraw: "
    let amount = Console.ReadLine()

    withdraw account (float amount)

[<EntryPoint>]
let main argv =
    printfn "Welcome to FooBar Bank\n"

    let name = getName()
    let balance = getBalance()

    let customer = Customer.newCustomer name
    let mutable account = Account.newAccount customer balance

    while true do
        printBalance account

        account <-
            try
                match getAction() with
                    | Deposit -> processDeposit account
                    | Withdrawal -> processWithdrawal account
                    | Exit -> Environment.Exit(0); account
            with ex -> printfn "Error: %s" ex.Message; account

    0
