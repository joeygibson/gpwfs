module Customer

type Customer = {
    FirstName : string
    LastName : string
 }

let newCustomer (name : string) =
    let nameChunks = name.Split ' '

    if nameChunks.Length = 2 then
        { FirstName = nameChunks.[0]
          LastName = nameChunks.[1] }
    else
        { FirstName = nameChunks.[0]
          LastName = "" }        
    
