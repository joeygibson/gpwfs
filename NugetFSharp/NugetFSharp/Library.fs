module NugetFSharp

open Humanizer
open Newtonsoft.Json

type Person = { Name : string; Age : int }

let getPerson () =
    let text = """{"Name": "Sam", "Age": 23 }"""
    let person = JsonConvert.DeserializeObject<Person>(text)
    
    printfn "Person called %s is %d years old." person.Name person.Age
    
    printfn "%s" ("ScriptsAreAGreatWayToExplorePackages".Humanize())
    
    person
