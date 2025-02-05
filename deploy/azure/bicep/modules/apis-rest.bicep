param location string
param managedEnvironmentId string
param serviceName string
param environment string
param envFriendlyName string

param containerImage string
param registryServer string
param registryUsername string

@secure()
param registryPassword string

var containerAppName = '${serviceName}-rest-api-${envFriendlyName}'
var containerRegistryPasswordName = 'container-registry-password'

resource containerApp 'Microsoft.App/containerApps@2024-10-02-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: managedEnvironmentId
    configuration: {
      ingress: {
        targetPort: 8080
        external: true
      }
      secrets: [
        {
          name: containerRegistryPasswordName
          value: registryPassword
        }
        {
          name: 'ENV'
          value: environment
        }
      ]
      registries: [
        {
          server: registryServer
          username: registryUsername
          passwordSecretRef: containerRegistryPasswordName
        }
      ]
    }
    template: {
      containers: [
        {
          image: containerImage
          name: containerAppName
        }
      ]
    }
  }
}
