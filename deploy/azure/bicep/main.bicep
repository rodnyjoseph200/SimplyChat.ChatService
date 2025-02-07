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

module monitorModule './modules/shared/monitor.bicep' = {
  name: 'monitor'
  params: {
    serviceName: serviceName
    envFriendlyName: envFriendlyName
    location: location
  }
}

module containerAppEnvironmentModule './modules/shared/container-app-environment.bicep' = {
  name: 'container-app-environment'
  params: {
    serviceName: serviceName
    envFriendlyName: envFriendlyName
    location: location
    logAnalyticsWorkspaceName: monitorModule.outputs.logAnalyticsWorkspaceName
  }
}

module cosmosDbModule './modules/shared/cosmosdb.bicep' = {
  name: 'cosmos-db'
  params: {
    envFriendlyName: envFriendlyName
    location: location
    serviceName: serviceName
  }
}

module keyVaultModule './modules/shared/key-vault.bicep' = {
  name: 'key-vault'
  params: {
    serviceName: serviceName
    location: location
    envFriendlyName: envFriendlyName
    objectId: objectId
  }
}

module keyVaultSecretsModule './modules/shared/key-vault-secrets.bicep' = {
  name: 'key-vault-secrets'
  params: {
    keyVaultName: keyVaultModule.outputs.name
    cosmosAccountName: cosmosDbModule.outputs.accountName
  }
}

module restApiModule './modules/container-app-rest-api.bicep' = {
  name: 'rest-api'
  params: {
    location: location
    serviceName: serviceName
    managedEnvironmentId: containerAppEnvironmentModule.outputs.managedEnvironmentId
    containerImage: restApiContainerImage
    registryServer: registryServer
    registryUsername: username
    registryPassword: password
    envFriendlyName: envFriendlyName
    cosmosDbConnectionStringName: keyVaultSecretsModule.outputs.cosmosDbConnectionStringName
    cosmosAccountName: cosmosDbModule.outputs.accountName
    cosmosDbDatabaseName: cosmosDbModule.outputs.databaseName
    cosmosDbContainerName: cosmosDbModule.outputs.containerName
  }
}

//provide outputs
