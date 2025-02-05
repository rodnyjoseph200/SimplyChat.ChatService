param environment string
param envFriendlyName string
param restApiContainerImage string
param registryServer string
param location string = resourceGroup().location

@secure()
param username string
@secure()
param password string
@secure()
param objectId string

var serviceName = 'chat-service'

module monitor './modules/shared/monitor.bicep' = {
  name: 'monitor'
  params: {
    serviceName: serviceName
    envFriendlyName: envFriendlyName
    location: location
  }
}

module containerAppEnvironment './modules/shared/container-app-environment.bicep' = {
  name: 'container-app-environment'
  params: {
    serviceName: serviceName
    envFriendlyName: envFriendlyName
    location: location
    logAnalyticsWorkspaceName: monitor.outputs.logAnalyticsWorkspaceName
  }
}

module restApi './modules/apis-rest.bicep' = {
  name: 'rest-api'
  params: {
    location: location
    serviceName: serviceName
    managedEnvironmentId: containerAppEnvironment.outputs.managedEnvironmentId
    containerImage: restApiContainerImage
    registryServer: registryServer
    registryUsername: username
    registryPassword: password
    environment: environment
    envFriendlyName: envFriendlyName
  }
}

module cosmosDb './modules/shared/cosmosdb.bicep' = {
  name: 'cosmos-db'
  params: {
    envFriendlyName: envFriendlyName
    location: location
    serviceName: serviceName
  }
}

module keyVault './modules/shared/key-vault.bicep' = {
  name: 'key-vault'
  params: {
    serviceName: serviceName
    location: location
    envFriendlyName: envFriendlyName
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
