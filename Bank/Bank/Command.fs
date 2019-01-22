module Command

type BankCommand =
    | Withdraw
    | Deposit
    
type Command =
    | AccountCommand of BankCommand
    | Exit
    
let tryParseSerializedCommand input =
    match input with
    | "deposit" | "Deposit" | "d" -> Some Deposit
    | "withdraw" | "Withdraw" | "w" -> Some Withdraw
    | _ -> None
    
let tryParseCommand input =
    match input with
        | "deposit" | "Deposit" | "d" -> Some (AccountCommand Deposit)
        | "withdraw" | "Withdraw" | "w" -> Some (AccountCommand Withdraw)
        | "exit" | "Exit" | "x" -> Some Exit
        | _ -> None
        

