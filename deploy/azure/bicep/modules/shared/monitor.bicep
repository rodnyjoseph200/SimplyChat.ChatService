param appName string
param envFriendlyName string
param location string

param workspaceName string = '${appName}-loganalytics-workspace-${envFriendlyName}'
param applicationInsightsName string = '${appName}-appinsights-${envFriendlyName}'

// Creates a Log Analytics workspace
resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: workspaceName
  location: location
  properties: {
    retentionInDays: 30
    sku: {
      name: 'PerGB2018'
    }
  }
}

// Creates an Application Insights instance
resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: applicationInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id
  }
}

output logAnalyticsWorkspaceName string = logAnalyticsWorkspace.name
