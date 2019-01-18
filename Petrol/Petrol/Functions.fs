module Petrol.Functions

open System

(* tupled functions *)
let tupledAdd(a, b) = a + b
let answer = tupledAdd(10, 23)

(* curried functions *)
let add first second = first + second
let addFive = add 5
let fifteen = addFive 10

(* wrapper functions *)
let buildDt year month day = DateTime(year, month, day)
//let buildDtThisYear month day = buildDt DateTime.UtcNow.Year month day
//let buildDtThisMonth day = buildDtThisYear DateTime.UtcNow.Month day

(* curried versions of the above *)
let buildDtThisYear = buildDt DateTime.UtcNow.Year
let buildDtThisMonth = buildDtThisYear DateTime.UtcNow.Month

let writeToFile (date: DateTime) filename text =
    let path = sprintf "%s-%s.txt" (date.ToString "yyMMdd") filename
    System.IO.File.WriteAllText(path, text)
    
let writeToToday = writeToFile DateTime.UtcNow.Date
let writeToTomorrow = writeToFile (DateTime.UtcNow.Date.AddDays 1.)
let writeToTodayHelloWorld = writeToToday "hello-world"

let answer1 = 10 |> add 5 |> add 20

let safeDiv x y =
    match y with
    | 0 -> None
    | _ -> Some(x/y)
    
    
    