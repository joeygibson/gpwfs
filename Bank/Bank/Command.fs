module Command

type Command =
    | Withdraw
    | Deposit
    | Exit
    
let tryParseCommand input =
    match input with
        | "deposit" | "Deposit" | "d" -> Some Deposit
        | "withdraw" | "Withdraw" | "w" -> Some Withdraw
        | "exit" | "Exit" | "x" -> Some Exit
        | _ -> None
        

