open CSharpProject
open System.Collections.Generic
open System

type PersonComparer() =
    interface IComparer<Person> with
        member this.Compare(x, y) = x.Name.CompareTo(y.Name)

let handlingNulls () =
    let blank : string = null
    let name = "Vera"
    let number = Nullable 10
    
    let blankAsOption = blank |> Option.ofObj
    let nameAsOption = name |> Option.ofObj
    let numberAsOption = number |> Option.ofNullable
    let unsafeName = Some "Fred" |> Option.toObj
    
    ()

[<EntryPoint>]
let main argv =
    let person = CSharpProject.Person "Frank"
    person.PrintName()
    
    let names = [ "Frank"; "Dean"; "Sammy"; "Joey"; "Peter"; "Angie" ]
    
    let longhand =
        names    
        |> List.map (fun name -> Person name)
    
    let shorthand =
        names
        |> List.map Person
        
    printfn "%A" longhand
    printfn "%A" shorthand
    
    let pComparer = PersonComparer() :> IComparer<Person>
    
    let rc = pComparer.Compare(person, Person "Frank")
    printfn "RC: %d" rc
    
    
    0 // return an integer exit code
