module Car

let driveTo destination petrol =
    let petrolNeeded =
        match destination with
            | "home" | "Home" -> 25
            | "office" | "Office" -> 50
            | "stadium" | "Stadium" -> 25
            | "gas station" | "Gas Station" -> 10
            | _ -> failwithf "Invalid destination: %s" destination

    if petrolNeeded > petrol then
        failwith "Not enough petrol, mate."
    else
        let resultPetrol = petrol - petrolNeeded

        if destination = "gas station" then
            resultPetrol + 50
        else
            resultPetrol

