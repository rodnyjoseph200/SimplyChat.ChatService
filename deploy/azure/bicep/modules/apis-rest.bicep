param location string
param managedEnvironmentId string
param appName string
param serviceName string

param containerImage string
param registryServer string
param registryUsername string

@secure()
param registryPassword string

var containerAppName = '${appName}-${serviceName}-rest-api'
var containerRegistryPasswordName = 'container-registry-password'

resource containerApp 'Microsoft.App/containerApps@2024-10-02-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: managedEnvironmentId
    configuration: {
      ingress: {
        targetPort: 8081
        external: true
      }
      secrets: [
        {
          name: containerRegistryPasswordName
          value: registryPassword
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
