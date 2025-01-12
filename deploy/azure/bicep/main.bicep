param environmentName string
param restApiContainerImage string
param registryServer string
param registryUsername string
param location string = resourceGroup().location

@secure()
param registryPassword string

var appName = 'simplychat'
var serviceName = 'chat-service'

module shared './modules/shared/monitor-and-app-env.bicep' = {
  name: 'monitor-and-container-app-environment'
  params: {
    environmentName: environmentName
    location: location
  }
}

module restApi './modules/apis-rest.bicep' = {
  name: 'rest-api'
  params: {
    location: location
    appName: appName
    serviceName: serviceName
    managedEnvironmentId: shared.outputs.managedEnvironmentId
    containerImage: restApiContainerImage
    registryServer: registryServer
    registryUsername: registryUsername
    registryPassword: registryPassword
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

//provide outputs
