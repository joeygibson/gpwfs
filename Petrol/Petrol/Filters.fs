module Petrol.Filters

type Customer = { Age : int }

let where filter customers =
    seq {
        for customer in customers do
            if filter customer then
                yield customer
    }

let printCustomerAge writer customer =
    match customer.Age with
    | age when age < 13 -> writer "Child!"
    | age when age < 20 -> writer "Teenager!"
    | _ -> writer "Adult!"

let customers = [ { Age = 21 }; { Age = 35 }; { Age = 36 } ]

let isOVer35 customer = customer.Age > 35

let filtered0 = customers |> where isOVer35
let filtered1 = customers |> where (fun customer -> customer.Age > 35)

