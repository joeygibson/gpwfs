module BusinessLogicTests

open BusinessLogic
open Xunit
open FsUnit.Xunit

let isTrue (b:bool) = Assert.True b

[<Fact>]
let ``Large, young teams are correctly identified``() =
    let department =
        { Name = "Super Team"
          Team = [ for i in 1..15 -> { Name= sprintf "Person %d" i; Age = 19 } ] }
        
    Assert.True(department |> isLargeAndYoungTeam)
    // or
    department |> isLargeAndYoungTeam |> Assert.True
    // or
    department |> isLargeAndYoungTeam |> isTrue
    
[<Fact>]
let ``Testing wit FSUnit.XUnit``() =
    let department =
        { Name = "Super Team"
          Team = [ for i in 1..15 -> { Name= sprintf "Person %d" i; Age = 19 } ] }
        
    department
    |> isLargeAndYoungTeam
    |> should equal true
    
    department.Team.Length |> should be (greaterThan 10)


    



