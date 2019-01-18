module Customer

type Customer = {
    FirstName : string
    LastName : string
 }

let newCustomer (name : string) =
    let nameChunks = name.Split ' '

    { FirstName = nameChunks.[0]
      LastName = nameChunks.[1] }
    
