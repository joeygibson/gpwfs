module Looping

// for..in loops
for number in 1..10 do
    printfn "Hello, %d" number
    
for number in 10 .. -2 .. 1 do
    printfn "Downward, %d" number    // count from 10 - 1 by 2
    
let customerIds = [45..99]
for customerId in customerIds do
    printfn "Customer %d" customerId
    
// custom stepping
for even in 2..2..10 do
    printfn "%d is an even number" even

open System.IO
// while loops
let reader = new StreamReader(File.OpenRead "/etc/hosts")
while (not reader.EndOfStream) do
    printfn "%s" (reader.ReadLine())
    
reader.Close()

open System

// comprehensions
let arrayOfChars = [| for c in 'a'..'z' -> Char.ToUpper c |]
let listOfSquares = [ for i in 1..10 -> i * i ]
let seqOfStrings = seq { for i in 2..4..20 -> sprintf "Number %d" i }