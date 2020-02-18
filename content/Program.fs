open Farmer
open Farmer.Resources
open Farmer.Deploy
open System

// Create ARM resources here e.g. webApp { } or storage { } etc.

// Add the resources to the deployment using the add_resource keyword. 
let deployment = arm {
    location NorthEurope
}

let output =
    deployment
    |> Writer.quickWrite "output"