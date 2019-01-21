module Bank

open Account
open Journal
open System

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

let withdrawWithConsoleJournal amount = journalAs
                                         (createTransaction amount "withdraw")
                                         consoleJournal
                                         withdraw
                                         amount
let depositWithConsoleJournal amount = journalAs
                                         (createTransaction amount "deposit")
                                          consoleJournal
                                          deposit
                                          amount

let withdrawWithFileJournal amount = journalAs
                                        (createTransaction amount "withdraw")
                                         fileSystemJournal
                                         withdraw
                                         amount
let depositWithFileJournal amount = journalAs
                                        (createTransaction amount "deposit")
                                         fileSystemJournal
                                         deposit
                                         amount

let isValidCommand cmd = [ "deposit"; "Deposit"; "d"; "withdraw"; "Withdraw"; "w"; "x"]
                         |> List.contains cmd
                         
let isStopCommand = (=) "x"
let getAmount command =
    Console.WriteLine()
    Console.Write "Enter amount: "
    command, Console.ReadLine() |> float
    
[<EntryPoint>]
let main argv =
    printfn "Welcome to FooBar Bank\n"

    let useConsole = true
    
    let depositJournal, withdrawJournal =
        if useConsole then
            depositWithConsoleJournal, withdrawWithConsoleJournal
        else
            depositWithFileJournal, withdrawWithFileJournal
    
    let name = getName()

    let customer = Customer.newCustomer name
    let openingAccount = Account.newAccount customer 0.0
    
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
            | "deposit" | "Deposit" | "d" -> depositJournal amount account
            | "withdraw" | "Withdraw" | "w" -> withdrawJournal amount account
            | _ -> failwithf "Invalid command: %A" command
        printfn "Current balance: $%0.2f" account.Balance
        account
    
    let closingAccount =
        commands
        |> Seq.filter isValidCommand
        |> Seq.takeWhile (not << isStopCommand)
        |> Seq.map getAmount
        |> Seq.fold processCommand openingAccount
        
    printfn "\nClosing balance: $%0.2f" closingAccount.Balance

    0
