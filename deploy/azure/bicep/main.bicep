param environmentName string
param restApiContainerImage string
param registryServer string
param location string = resourceGroup().location

@secure()
param username string
@secure()
param password string

var appName = 'simplychat'
var serviceName = 'chat-service'

module monitor './modules/shared/monitor.bicep' = {
  name: 'monitor'
  params: {
    environmentName: environmentName
    location: location
  }
}

module containerAppEnvironment './modules/shared/container-app-environment.bicep' = {
  name: 'container-app-environment'
  params: {
    environmentName: environmentName
    location: location
    logAnalyticsWorkspaceName: monitor.outputs.logAnalyticsWorkspaceName
  }
}

module restApi './modules/apis-rest.bicep' = {
  name: 'rest-api'
  params: {
    location: location
    appName: appName
    serviceName: serviceName
    managedEnvironmentId: containerAppEnvironment.outputs.managedEnvironmentId
    containerImage: restApiContainerImage
    registryServer: registryServer
    registryUsername: username
    registryPassword: password
  }
}

module cosmosDb './modules/shared/cosmosdb.bicep' = {
  name: 'cosmos-db'
  params: {
    environmentName: environmentName
    location: location
    appName: appName
  }
}

module keyVault './modules/shared/key-vault.bicep' = {
  name: 'key-vault'
  params: {
    appName: appName
    location: location
    environmentName: environmentName
    objectId: username
  }
}

module keyVaultSecrets './modules/shared/key-vault-secrets.bicep' = {
  name: 'key-vault-secrets'
  params: {
    keyVaultName: keyVault.outputs.name
    cosmosAccountName: cosmosDb.outputs.accountName
  }
}

//provide outputs
