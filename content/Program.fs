open Farmer
open Farmer.Builders
open Farmer.Deploy
open System

// Create ARM resources here e.g. webApp { } or storageAccount { } etc.
// See https://compositionalit.github.io/farmer/api-overview/resources/ for more details.

// Add resources to the ARM deployment using the add_resource keyword.
// See https://compositionalit.github.io/farmer/api-overview/resources/arm/ for more details.
let deployment = arm {
    location Location.NorthEurope
}

printf "Generating ARM template..."
deployment |> Writer.quickWrite "output"
printfn "all done! Template written to output.json"