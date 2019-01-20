module Collections

type FootballResult = {
    HomeTeam : string
    AwayTeam : string
    HomeGoals : int
    AwayGoals : int
}

let create (ht, hg) (at, ag) =
    { HomeTeam = ht
      AwayTeam = at
      HomeGoals = hg
      AwayGoals = ag }
    
let results = [
      create ("Messiville", 1) ("Ronaldo City", 2)
      create ("Messiville", 1) ("Bale Town", 3)
      create ("Bale Town", 3) ("Ronaldo City", 1)
      create ("Bale Town", 2) ("Messiville", 1)
      create ("Ronaldo City", 4) ("Messiville", 2)
      create ("Ronaldo City", 1) ("Bale Town", 2)
]

let isAwayWin result = result.AwayGoals > result.HomeGoals

let answer = results |> List.filter isAwayWin
            |> List.countBy(fun result -> result.AwayTeam)
            |> List.sortByDescending(fun (_, awayWins) -> awayWins)
            
// arrays
let numbersArray = [| 1; 2; 3; 4; 6 |]
let firstNumber = numbersArray.[0]
let firstThreeNumbers = numbersArray.[0..2]
numbersArray.[0] <- 99

// lists - like arrays, but immutable
let numbers = [1; 2; 3; 4; 5; 6]
let numbersQuick = [1..6]
let head :: tail = numbers
let moreNumbers = 0 :: numbers
let evenMoreNumbers = moreNumbers @ [7..9]

// collection functions
let numbersAgain = [1..10]
let timesTwo n = n * 2
let output = numbersAgain |> List.map timesTwo

// List.iter is like List.map, but returns unit
numbersAgain |> List.iter (printfn "%d")

// collect is like flatMap
type Order = { OrderId : int }
type Customer = { CustomerId : int; Orders : Order list; Town : string }
let customers = []
let orders : Order list = customers |> List.collect(fun c -> c.Orders)

// pairwise returns list of tuples of adjacent pairs
let pairs = numbersAgain |> List.pairwise

open System

let times = [
    DateTime(2010,5,1)
    DateTime(2010,6,1)
    DateTime(2010,6,12)
    DateTime(2010,7,3)
]

let timesBetween = times |> List.pairwise |> List.map (fun (a, b) -> b - a) |> List.map (fun time -> time.TotalDays)
                    
// windowed lets you specify how many adjacent items get grouped
let numberWindows = numbersAgain |> List.windowed 3

let homeTeams = results |> List.groupBy (fun result -> result.HomeTeam)
let homeTeamGames = results |> List.countBy (fun result -> result.HomeTeam)

let messivilleGames = results |> List.partition (fun result -> result.HomeTeam = "Messiville")

// aggregates
let floats = [1.0..10.0]
let total = floats |> List.sum
let average = floats |> List.average
let max = floats |> List.max
let min = floats |> List.min

let firstMessiville = results |> List.find (fun result -> result.HomeTeam = "Messiville")
let firstFloat = floats |> List.head
let tailFloats = floats |> List.tail
let fourthFloat = floats |> List.item 3
let firstThreeFloats = floats |> List.take 3
let fiveExists = floats |> List.exists (fun f -> f = 5.)
let allOverTen = floats |> List.forall (fun f -> f > 10.)
let containsFive = floats |> List.contains 5.
let greaterThanFive = floats |> List.filter (fun f -> f > 5.)
let howManyFloats = floats |> List.length
let distinctFloats = floats @ floats |> List.distinct

// converting between collection types
let numberOne =
    [1..5]
    |> List.toArray
    |> Seq.ofArray
    |> Seq.head

open System.IO

type DirInfo = { FolderName : string; Size : int64; NumberOfFiles : int; AvgFileSize: float;
                 Extensions : string list }

let getFiles directory = List.ofArray (Directory.GetFiles directory)
let getFileSizes files = files |> List.map (fun file -> FileInfo(file).Length) 
 
let newDirInfo dir fileList =
    let fileSizes = getFileSizes fileList
    let averageFileSize =
        if List.length fileSizes > 0 then
            fileSizes |> List.map float |> List.average 
        else
            0.
    let distinctFileExtensions =
        fileList
            |> List.map (fun fileName -> Path.GetExtension fileName)
            |> List.filter (fun extension -> extension <> "")
            |> List.distinct
            |> List.sort
        
    { FolderName = dir
      Size = List.sum fileSizes
      NumberOfFiles = List.length fileSizes
      AvgFileSize = averageFileSize
      Extensions = distinctFileExtensions }
    
let directories = Directory.GetDirectories "/Users/jgibson/tmp"
let sortedDirs = directories
                |> List.ofArray
                |> List.map (fun dir -> dir , (getFiles dir))
                |> List.map (fun (dir, fileList) -> dir, (newDirInfo dir fileList))                
                |> List.sortByDescending (fun (dir, dirInfo) -> dirInfo.Size)

