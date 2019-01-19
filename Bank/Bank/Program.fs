module Bank

open Account
open Journal
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

let getActionAndAmount() =
    printf "Deposit or Withdraw: "

    let input = Console.ReadLine()

    let action = match input with
        | "deposit" | "Deposit" | "d" -> Deposit
        | "withdraw" | "Withdraw" | "w" -> Withdrawal
        | "exit" | "ex" | "Exit" -> Exit
        | _ -> failwithf "Invalid action: %s" input

    let amount =
        if action <> Exit then
            printf "Amount: "
            float (Console.ReadLine())
        else
            0.

    action, amount

let processDeposit account =
    printf "Amount to deposit: "
    let amount = Console.ReadLine()

    deposit account (float amount)

let processWithdrawal account =
    printf "Amount to withdraw: "
    let amount = Console.ReadLine()

    withdraw account (float amount)

let withdrawWithConsoleJournal = withdraw |> journalAs "withdraw" consoleJournal
let depositWithConsoleJournal = deposit |> journalAs "deposit" consoleJournal

let withdrawWithFileJournal = withdraw |> journalAs "withdraw" fileSystemJournal
let depositWithFileJournal = deposit |> journalAs "deposit" fileSystemJournal 

[<EntryPoint>]
let main argv =
    printfn "Welcome to FooBar Bank\n"

    let useConsole = false
    
    let depositJournal, withdrawalJournal =
        if useConsole then
            depositWithConsoleJournal, withdrawWithConsoleJournal
        else
            depositWithFileJournal, withdrawWithFileJournal
    
    let name = getName()
    let balance = getBalance()

    let customer = Customer.newCustomer name
    let mutable account = Account.newAccount customer balance

    while true do
        printBalance account

        account <-
            try
                match getActionAndAmount() with
                    | Deposit, amount -> depositJournal amount account
                    | Withdrawal, amount -> withdrawalJournal amount account
                    | Exit, _ -> Environment.Exit 0; account
            with ex -> printfn "Error: %s" ex.Message; account

    0
