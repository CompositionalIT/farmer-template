open Farmer
open Farmer.Resources
open Farmer.Deploy
open System

// Create ARM resources here e.g. webApp { } or storageAccount { } etc.
// See https://compositionalit.github.io/farmer/api-overview/resources/ for more details.

// Add resources to the ARM deployment using the add_resource keyword.
// See https://compositionalit.github.io/farmer/api-overview/resources/arm/ for more details.
let deployment = arm {
    location NorthEurope
}

module Config =
    let private getEnv name =
        match Environment.GetEnvironmentVariable name with
        | null -> failwithf "Missing environment variable %s" name
        | name -> name
    let private getEnvGuid = getEnv >> Guid
    let credentials =
        { ClientId = getEnvGuid "CLIENT_ID"
          ClientSecret = getEnvGuid "CLIENT_SECRET"
          TenantId = getEnvGuid "TENANT_ID" }
    let subscriptionId = getEnvGuid "SUBSCRIPTION_ID"
    let resourceGroupName = getEnv "RESOURCE_GROUP_NAME"

let response =
    deployment
    |> fullDeploy Config.credentials Config.subscriptionId Config.resourceGroupName []

let description =
    match response.Result with
    | Ok outputs -> sprintf "Success! Outputs: %A" outputs
    | Error (DeploymentRejected error) -> sprintf "Rejected! %A" error
    | Error (DeploymentFailed error) -> sprintf "Failed! %A" error

printfn "Deployment %s finished with result: %s" response.DeploymentName description