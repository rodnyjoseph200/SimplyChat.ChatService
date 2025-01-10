param environmentName string
param restApiContainerImage string
param registryServer string
param registryUsername string

@secure()
param registryPassword string

param location string = resourceGroup().location

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
    managedEnvironmentId: shared.outputs.managedEnvironmentId
    containerImage: restApiContainerImage
    registryServer: registryServer
    registryUsername: registryUsername
    registryPassword: registryPassword
  }
}

//provide outputs
