module MapsDictionariesSets

open System
open System.Collections.Generic
open System.IO

let inventory = Dictionary<string, float>()
inventory.Add("Apples", 0.33)

let inventory1 = Dictionary<_,_>()
inventory1.Add("Apples", 0.33)

let inventory2 = Dictionary()
inventory2.Add("Apples", 0.33)

// imutable, functional
let inventory3 : IDictionary<string, float> =
    ["Apples", 0.33; "Oranges", 0.23; "Bananas", 0.45]
    |> dict
    
// maps are immutable and "better"
let inventoryMap = ["Apples", 0.33; "Oranges", 0.23; "Bananas", 0.45] |> Map.ofList
let apples = inventoryMap.["Apples"]
//let pineapples = inventoryMap.["Pineapples"] -> KeyNotFoundException
let newInventory =
    inventoryMap
        |> Map.add "Pineapples" 0.87
        |> Map.remove "Apples"
        
let cheapFruit, expensiveFruit =
    inventoryMap
        |> Map.partition (fun fuit cost -> cost < 0.3)

let dirDates =
    System.IO.Directory.EnumerateDirectories "/Users/jgibson"
    |> Seq.map (fun name -> DirectoryInfo(name))
    |> Seq.map (fun dirInfo -> dirInfo.Name, dirInfo.CreationTimeUtc)
    |> Map.ofSeq
    |> Map.map (fun name creationDate -> DateTime.UtcNow - creationDate)
    
// Sets
let myBasket = [ "Apples"; "Apples"; "Apples"; "Bananas"; "Pineapples" ]
let fruitsILike = myBasket |> Set.ofList
let yourBasket = [ "Kiwi"; "Bananas"; "Grapes" ]
let combinedBasket = (myBasket @ yourBasket) |> Set.ofList
let fruitsYouLike = yourBasket |> Set.ofList
let allFruits = fruitsILike + fruitsYouLike
let fruitsJustForMe = allFruits - fruitsYouLike
let fruitsWeCanShare = fruitsILike |> Set.intersect fruitsYouLike
let doILikeAllYourFruit = fruitsILike |> Set.isSubset fruitsYouLike

