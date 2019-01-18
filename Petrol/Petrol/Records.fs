module Petrol.Records

(* records *)

type Address = {
     Street : string
     City : string
     State : string
 }

type Customer = {
    FirstName : string
    Surname : string
    Age : int
    Address : Address
    EmailAddress : string
 }

let customer = {
    FirstName = "Frank"
    Surname = "Sinatra"
    Age = 48
    Address = {
        Street = "1313 Mockingbird Lane"
        City = "Burlington"
        State = "VT"
    }
    EmailAddress = "dean@example.com"
 }

let updatedCustomer = {
    customer with
       Age = 31
       EmailAddress = "frank@example.com"
 }

let randInt (min : int, max : int) =
    let rand = System.Random()
    let dbl = rand.NextDouble()
    let diff = float (max - min)
    let tmp = float (min) + (dbl * diff)

    int (tmp)

let alterAge customer =
    let rand = System.Random()
    let newAge = randInt (18, 45)

    let newCustomer = {
        customer with Age = int (newAge)
    }

    newCustomer
