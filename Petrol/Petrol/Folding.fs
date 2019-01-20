module Folding

open System
open System.IO

// sum
//let sum inputs =
//    Seq.fold
//        (fun state input -> state + input)
//        0
//        inputs

let sum inputs =
    Seq.fold (+) 0 inputs
        
let length inputs =
    Seq.fold
        (fun state _ -> state + 1)
        0
        inputs

//let max inputs =
//    Seq.fold
//        (fun currentMax value -> if value > currentMax then value else currentMax)
//        0
//        inputs

// a better way to do max
let max inputs =
    Seq.reduce
        (fun currentMax value -> if value > currentMax then value else currentMax)
        inputs
        
let sum2 inputs =
    (0, inputs) ||> Seq.fold (fun state input -> state + input)

let lines : string seq =
    seq {
        use sr = new StreamReader(File.OpenRead @"Petrol/Car.fs")
        while (not sr.EndOfStream) do
            yield sr.ReadLine()
    }
    
let charCount = ((0, lines) ||> Seq.fold (fun total line -> total + line.Length))

// list of rules
type Rule = string -> bool * string

let rules : Rule list =
    [fun text -> (text.Split ' ').Length = 3, "Must be three words"
     fun text -> text.Length <= 30, "Max length is 30 characters"
     fun text -> text
                 |> Seq.filter Char.IsLetter
                 |> Seq.forall Char.IsUpper, "All letters must be uppercase"
     ]
    
let buildValidator (rules: Rule list) =
    rules
        |> List.reduce (fun firstRule secondRule ->
            fun word ->
                let passed, error = printfn "firstRule"; firstRule word
                if passed then
                    let passed, error = printfn "secondRule"; secondRule word
                    if passed then true, "" else false, error
                else false, error
            )
        
let validate = buildValidator rules
let word = "HELLO FrOM F#"
let isValid = validate word

type FileRule = FileInfo -> bool * string
let fileRules = [
    fun (fileInfo: FileInfo) -> fileInfo.Length < 10000L, "Must be < 10000 bytes"
    fun (fileInfo: FileInfo) -> Path.GetExtension(fileInfo.Name) = "jpg", "Only JPGs allowed"
]

let getFiles directory =
    Directory.GetFiles(directory, "*")
    |> List.ofArray
    |> List.map (fun name -> FileInfo(name))

let buildFileValidator (rules: FileRule list) =
    rules
        |> List.reduce (fun firstRule secondRule ->
            fun word ->
                let passed, error = printfn "firstRule"; firstRule word
                if passed then
                    let passed, error = printfn "secondRule"; secondRule word
                    if passed then true, "" else false, error
                else false, error
            )
        
let fileValidator = buildFileValidator fileRules
let areFilesValid = fileValidator (getFiles "/Users/jgibson/tmp").[0]