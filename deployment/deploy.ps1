param(
    [Parameter(Mandatory=$true)]
    [string] $SubscriptionId,
    [string] $ResourceGroup = "RBCWcfTesting",
    [string] $Location = "westeurope",
    [Parameter(Mandatory=$true)]
    [string] $WcfServiceZipPath
    # [Parameter(Mandatory=$true)]
    # [string] $WcfClientProjectPath
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
    --parameters location=$Location `
    --resource-group $ResourceGroup | ConvertFrom-Json)

## code deployment
az webapp deploy --src-path $WcfServiceZipPath --resource-group $ResourceGroup `
    --name $deploymentResults.properties.outputs.wcfServiceName.value `
    --restart

# az webapp deploy --src-path $WcfClientZipPath --resource-group $ResourceGroup `
#     --name $deploymentResults.properties.outputs.wcfClientName.value