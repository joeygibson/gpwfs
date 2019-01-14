module Petrol

open System
open Car

let getDestination() =
    Console.WriteLine("Enter destination: ")
    Console.ReadLine()

let mutable petrol = 100

[<EntryPoint>]
let main argv =
    while true do
        try
            printfn "Current petrol: %d" petrol
            let destination = getDestination()
            printfn "Trying to drive to %s." destination
            petrol <- driveTo destination petrol
            printfn "Made it to %s." destination
        with ex -> printfn "Error: %s" ex.Message


    0
