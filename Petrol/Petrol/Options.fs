module Options

let aNumber : int = 10
let maybeANumber : int option = Some 10

let calculateAnnualPremiumUsd score =
    match score with
    | Some 0 -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    | None ->
        printfn "No score supplied!"
        300
        
type Customer = {
    Name : string
    Score: int option
}

let customers = [
    { Name = "Frank Sinatra"; Score = (Some 200) }
    { Name = "Dean Martin"; Score = (Some -23) }
    { Name = "Joey Bishop"; Score = None }
]

let calculateCustomerPremium customer =
    match customer.Score with
    | Some 0 -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    | None -> 300
    
let describe score = printfn "%A" score

let customersAndPremiums =
    customers |> List.map (fun customer -> customer, (calculateCustomerPremium customer))

let description customer =
    match customer.Score with
    | Some score -> Some (describe score)
    | None -> None

// Option.map handles None automatically    
let descriptionTwp customer =
    customer.Score
    |> Option.map (fun score -> describe score)

let shorthand customer = customer.Score |> Option.map describe
//let optionalDescribe = Option.map describe // ?
// Option.iter is like List.iter, but handles None

// Option.bind is like List.collect (flatmap)
let tryFindCustomer cId = if cId = 10 then Some customers.[0] else None
let getScore customer = customer.Score
let score = tryFindCustomer 10 |> Option.bind getScore // -> 200
let scoreNone = tryFindCustomer 23 |> Option.bind getScore // -> None

// Option.filter only runs predicate if value is Some
let test1 = Some 5 |> Option.filter (fun x -> x > 5)
let test2 = Some 5 |> Option.filter (fun x -> x = 5)
let test3 = None |> Option.filter (fun x -> x > 5)

let test4 = Some 5 |> Option.count // 1
let test5 = None |> Option.count // 0
let test6 = Some 5 |> Option.exists (fun x -> x > 0) // true
let test7 = None |> Option.exists (fun x -> x > 0) // false

let list = Option.toList (Some 5) // [5]
let list1 : int list = Option.toList None // []

let array = Option.toArray (Some 5) // [|5|]
//let array1 : int [] option = Option.toArray None

let rng = System.Random()
let flipCoin () = rng.NextDouble() > 0.5

let coinFlips = [ for i in [0..100] -> flipCoin() ]
let choices = List.choose (fun x -> if x then (Some x) else None) coinFlips