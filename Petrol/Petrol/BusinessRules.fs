module BusinessRules

// Single-use DUs
type CustomerId = CustomerId of string
type Address = Address of string
type Email = Email of string
type Telephone = Telephone of string

let myAddress = Address "1 The Street"
//let isTheSameAddress = (myAddress = "1 The Street")
let (Address addressData) = myAddress


type Customer = {
    CustomerId : CustomerId
    Email : Email
    Telephone : Telephone
    Address : Address
}

let createCustomer (customerId: CustomerId) (email: Email)
    (telephone: Telephone) (address: Address) =
    { CustomerId = customerId
      Email = email
      Telephone = telephone
      Address = address }
    
type ContactDetails =
    | Address of string
    | Telephone of string
    | Email of string
    
type ShortCustomer = {
    CustomerId: CustomerId
    PrimaryContactDetails: ContactDetails
    SecondaryContactDetails: ContactDetails option
}

let createShortCustomer customerId primaryContactDetails secondaryContactDetails =
    { CustomerId = customerId
      PrimaryContactDetails = primaryContactDetails
      SecondaryContactDetails = secondaryContactDetails}
    
// customer type to represent business state
type GenuineCustomer = GenuineCustomer of ShortCustomer

let validateCustomer customer =
    match customer.PrimaryContactDetails with
    | Email e when e.EndsWith "supercorp.com" -> Some(GenuineCustomer customer)
    | Address _ | Telephone _ -> Some(GenuineCustomer customer)
    | Email _ -> None
    
let sendWelcomeEmail (GenuineCustomer customer) =
    printfn "Hello, %A, and welcome to our site!" customer.CustomerId
    
