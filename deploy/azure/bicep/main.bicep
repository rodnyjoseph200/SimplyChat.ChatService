param environment string
param restApiContainerImage string
param registryServer string
param location string = resourceGroup().location

@secure()
param username string
@secure()
param password string
@secure()
param objectId string

var appName = 'simplychat'
var serviceName = 'chat-service'

module monitor './modules/shared/monitor.bicep' = {
  name: 'monitor'
  params: {
    appName: appName
    environment: environment
    location: location
  }
}

module containerAppEnvironment './modules/shared/container-app-environment.bicep' = {
  name: 'container-app-environment'
  params: {
    appName: appName
    environment: environment
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
    environment: environment
  }
}

module cosmosDb './modules/shared/cosmosdb.bicep' = {
  name: 'cosmos-db'
  params: {
    environment: environment
    location: location
    appName: appName
  }
}

module keyVault './modules/shared/key-vault.bicep' = {
  name: 'key-vault'
  params: {
    appName: appName
    location: location
    environment: environment
    objectId: objectId
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
