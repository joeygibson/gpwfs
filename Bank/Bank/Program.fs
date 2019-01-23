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

let withdrawJournal amount = journalAs
                                (createTransaction amount Withdraw)
                                 composedJournal
                                 withdrawSafe
                                 amount
                                 
let depositJournal amount = journalAs
                                (createTransaction amount Deposit)
                                 composedJournal
                                 deposit
                                 amount
                         
let tryGetAmount command =
    Console.WriteLine()
    Console.Write "Enter amount: "
    let amount = Console.ReadLine() |> Double.TryParse
    match amount with
        | true, amount -> Some(command, amount)
        | false, _ -> None
    
[<EntryPoint>]
let main argv =
    printfn "Welcome to FooBar Bank\n"
    
    let name = getName()

    let customer = Customer.newCustomer name
    let openingAccount = 
        match (loadAccountFromDisk customer) with
        | Some(account) -> account
        | None -> newAccount customer 0.0 |> classifyAccount
    
    printfn "Account: %O" (openingAccount.GetField (fun a -> a.Id))
    printfn "Starting balance: %0.2f\n" (openingAccount.GetField (fun a -> a.Balance))
    
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
            | AccountCommand Deposit -> depositJournal amount account
            | AccountCommand Withdraw -> withdrawJournal amount account
            | Exit -> account
        printBalance account
        account
    
    let closingAccount =
        commands
        |> Seq.choose tryParseCommand
        |> Seq.takeWhile ((<>) Exit)
        |> Seq.choose tryGetAmount
        |> Seq.fold processCommand openingAccount
        
    printBalance closingAccount

    0
