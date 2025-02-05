param location string
param managedEnvironmentId string
param serviceName string
param envFriendlyName string

param containerImage string
param registryServer string
param registryUsername string

param cosmosDbConnectionStringName string

@secure()
param registryPassword string
@secure()
param cosmosDbConnectionString string

var containerAppName = '${serviceName}-rest-api-${envFriendlyName}'
var containerRegistryPasswordName = 'container-registry-password'

//todo - Reference secrets from Key Vault for added benefits like rotation
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
          name: cosmosDbConnectionStringName
          value: cosmosDbConnectionString
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
          env: [
            {
              name: cosmosDbConnectionStringName
              secretRef: cosmosDbConnectionStringName
            }
          ]
        }
      ]
    }
  }
}
