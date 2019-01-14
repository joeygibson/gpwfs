// Learn more about F# at http://fsharp.org

open System
open System.Collections.Generic

let greetPerson name age =
    sprintf "Hello, %s. You are %d years old" name age

let doSomethingWitNumbers (first, second) =
    let added = first + second
    Console.WriteLine("{0} + {1} = {2}", first, second, added)
    let doubled = added * 2
    doubled

let estimateAges (familyName, year1, year2, year3): string =
    let calculateAge yearOfBirth =
        let year = System.DateTime.Now.Year
        year - yearOfBirth

    let estimatedAge1 = calculateAge year1
    let estimagedAge2 = calculateAge year2
    let estimatedAge3 = calculateAge year3

    let averageAge = (estimatedAge1 + estimagedAge2 + estimatedAge3) / 3
    sprintf "Average age for family %s is %d." familyName averageAge

let add (a : int, b : int) : int =
    let answer : int = a + b
    answer
    
let createList(first, second) =
    let output = List()
    output.Add(first)
    output.Add(second)
    
    output

[<EntryPoint>]
let main argv =
    let items = argv.Length

 //    printfn "Passed in %d args: %A" items argv
    let greeting = greetPerson argv.[0] (argv.[1] |> int)
    printfn "%s" greeting

    doSomethingWitNumbers (23, 32)

    let estimatedAge =
        let age =
            let year = DateTime.Now.Year
            year - 1970
        sprintf "You are about %d years old", age

    let age = estimatedAge
//    let lust = createList "foo", "bar"

//    printf age

    let mutable age = 23
    age <- 48
    
    printfn "Age: %d" age

    0 // return an integer exit code
