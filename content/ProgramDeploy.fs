open Farmer
open Farmer.Builders
open System

// Create ARM resources here e.g. webApp { } or storageAccount { } etc.
// See https://compositionalit.github.io/farmer/api-overview/resources/ for more details.

// Add resources to the ARM deployment using the add_resource keyword.
// See https://compositionalit.github.io/farmer/api-overview/resources/arm/ for more details.
let deployment = arm {
    location Location.WestEurope
}

let resourceGroupName =
    match Environment.GetEnvironmentVariable "RESOURCE_GROUP_NAME" with
    | null -> failwith "Missing RESOURCE_GROUP_NAME environment variable"
    | value -> value

deployment
|> Deploy.execute resourceGroupName Deploy.NoParameters
|> ignore