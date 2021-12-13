param(
    [Parameter(Mandatory=$true)]
    [string] $SubscriptionId,
    [string] $ResourceGroup = "RBCWcfTesting",
    [string] $Location = "westeurope"
)

## login
az login
az account set --subscription $SubscriptionId

## template
$template = Get-ChildItem -Path "$PSScriptRoot/templates/azuredeploy.json"
$params = Get-ChildItem -Path "$PSScriptRoot/templates/azureparameters.json"

az group create --location $Location --name $ResourceGroup

$deploymentResults = $(az deployment group create --template-file $template.FullName `
    --parameters $params.FullName `
    --resource-group $ResourceGroup | ConvertFrom-Json)