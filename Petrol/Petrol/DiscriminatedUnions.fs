module DiscriminatedUnions

type MMCDisk =
    | RsMmc
    | MmcPlus
    | SecureMMC
    
type Disk =
    | HardDisk of RPM:int * Platters:int
    | SolidState
    | MMC of MMCDisk * NumberOfPins:int
    
let myHardDisk = HardDisk(RPM = 7500, Platters = 7)
let myHardDisk1 = HardDisk(7500, 7)
let args = 7500, 7
let myHardDisk2 = HardDisk args
let myMMC = MMC(RsMmc, 5)
let mySsd = SolidState

let seek disk =
    match disk with
    | HardDisk _ -> "Seeking loudly at a reasonable speed!"
    | MMC _ -> "Seeking quietly, but slowly."
    | SolidState -> "Already found it"
    
let seek2 disk =
    match disk with
    | HardDisk(5400, 5) -> "Very slowly"
    | HardDisk(rpm, 7) -> sprintf "7 spindles at %d RPM" rpm
    | MMC(_, 3) -> "Seeking with three pins"
    
let describe disk =
    match disk with
    | SolidState -> "I'm a newfangled SSD."
    | MMC(_, 1) -> "I have only one pin."
    | MMC(_, pins) when pins < 5 -> "I'm an MMC with a few pins."
    | MMC(_, pins) -> sprintf "I'm an MMC with %d pins." pins
    | HardDisk(5400, _) -> "I'm a slow hard disk."
    | HardDisk(_, 7) -> "I have 7 spindles."
    | HardDisk _ -> "I'm a hard disk."
    
// combining records and DUs
type DiskInfo = {
    Manufacturer: string
    SizeGb: int
    DiskData: Disk
}

type Computer = {
    Manufacturer: string
    Disks: DiskInfo list
}

let myPc = {
    Manufacturer = "Apple, Inc."
    Disks = [
        { Manufacturer = "Apple, Inc."
          SizeGb = 100
          DiskData = HardDisk(7500, 7) }
        { Manufacturer = "WD"
          SizeGb = 1000
          DiskData = SolidState }
    ]
 }

// C# enum
type Printer =
    | InkJet = 0
    | LaserJet = 1
    | DotMatrix = 2
    