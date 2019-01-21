module Matching
open System

let getCreditLimit customer =
    match customer with
    | "medium", 1 -> 500
    | "good", years when years < 2 -> 750
//    | "good", 0 | "good", 1 -> 750
    | "good", 2 -> 1000
    | "good", _ -> 2000
    | _ -> 250
    
// limit use of nesting to extreme cases when code complexity is a reasonable tradeoff. 
let nestedGetCreditLimit customer =
    match customer with
    | "medium", 1 -> 500
    | "good", years ->
        match years with
        | 0 | 1 -> 750
        | 2 -> 1000
        | _ -> 2000
    | _ -> 250
    
type Customer = {Name: string; Balance: float}

// matching against lists
let handleCustomer customers =
    match customers with
    | [] -> failwith "No customers!"
    | [ customer ] -> printfn "Single customer, name is %s" customer.Name
    | [ first; second ] -> printfn "Two customers!"
    | customers -> printfn "Several customers: %d" customers.Length
    
// matching against record fields
let getStatus customer =
    match customer with
    | { Balance = 0. } -> "Customer has empty account."
    | { Name = "Isaac" } -> "Great customer!"
    | { Name = name; Balance = 50. } -> sprintf "%s has a large balance." name
    | { Name = name } -> sprintf "%s is a normal customer" name
    
let rng = Random()

let randos = [ for i in [1..100] -> rng.Next() ]
let answer =
    match randos with
    | [] -> "Empty List"
    | [_; _; _] -> "List of three"
    | x::xs when x = 5 -> "Head is 5"
    | _ -> "Something else"

