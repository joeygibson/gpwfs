module Bank

open Account
open Command
open FileSystem
open Journal
open System
open Transaction

let getName() =
    printf "Name: "
    Console.ReadLine()

let processDeposit account =
    printf "Amount to deposit: "
    let amount = Console.ReadLine()

    deposit account (float amount)

let processWithdrawal account =
    printf "Amount to withdraw: "
    let amount = Console.ReadLine()

    withdraw account (float amount)

let withdrawJournal amount = journalAs
                                (createTransaction amount "withdraw")
                                 composedJournal
                                 withdraw
                                 amount
let depositJournal amount = journalAs
                                (createTransaction amount "deposit")
                                 composedJournal
                                 deposit
                                 amount
                         
let getAmount command =
    Console.WriteLine()
    Console.Write "Enter amount: "
    command, Console.ReadLine() |> float
    
[<EntryPoint>]
let main argv =
    printfn "Welcome to FooBar Bank\n"
    
    let name = getName()

    let customer = Customer.newCustomer name
    let openingAccount = loadOrCreate customer
    
    printfn "Account: %O" openingAccount.Id
    printfn "Starting balance: %0.2f\n" openingAccount.Balance
    
    let commands = seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadLine()
            Console.WriteLine()
    }
    
    let processCommand account (command, amount) =
        printfn ""
        let account =
            match command with
            | Deposit -> depositJournal amount account
            | Withdraw -> withdrawJournal amount account
            | Exit -> account
        printfn "Current balance: $%0.2f" account.Balance
        account
    
    let closingAccount =
        commands
        |> Seq.choose tryParseCommand
        |> Seq.takeWhile ((<>) Exit)
        |> Seq.map getAmount
        |> Seq.fold processCommand openingAccount
        
    printfn "\nClosing balance: $%0.2f" closingAccount.Balance

    0
