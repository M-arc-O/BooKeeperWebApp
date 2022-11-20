@description('Application name')
param applicationName string

@description('Azure resources location')
param location string = resourceGroup().location

@description('Static Web App SKU')
param staticWebAppSku string

@description('Static Web App SKU name')
param staticWebAppSkuNuma string

@description('Static Web App application folder location')
param staticWebAppAppLocation string

@description('Static Web App API folder location')
param staticWebAppApiLocation string

@description('Static Web App application artifact location')
param staticWebAppAppArtifactLocation string

@description('Resource tags')
param resourceTags object

@description('Key vault name')
param keyVaultName string

@description('Service principal object id for key vault access policy')
param keyVaultSpObjectId string

@description('Database name')
param databaseName string

@description('Database admin login name')
@secure()
param adminLoginName string

@description('Database admin login password')
@secure()
param adminLoginPassword string

@description('Database admin login name')
@secure()
param dbUserLoginName string

@description('Database admin login password')
@secure()
param dbUserLoginPassword string

var adminConnectionString = 'Server=tcp:${applicationName}-dbs${environment().suffixes.sqlServerHostname},1433;Initial Catalog=${applicationName}-db;Persist Security Info=False;User ID=${adminLoginName};Password=${adminLoginPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
var userConnectionString = 'Server=tcp:${applicationName}-dbs${environment().suffixes.sqlServerHostname},1433;Initial Catalog=${applicationName}-db;Persist Security Info=False;User ID=${dbUserLoginName};Password=${dbUserLoginPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'

resource keyVault 'Microsoft.KeyVault/vaults@2019-09-01' = {
  name: keyVaultName
  location: location
  properties: {
    tenantId: subscription().tenantId
    enabledForTemplateDeployment: true
    enabledForDeployment: true
    sku: {
      family: 'A'
      name: 'standard'
    }
    accessPolicies: [
      {
        tenantId: subscription().tenantId
        objectId: keyVaultSpObjectId
        permissions: {
          keys: [ 'get', 'list', 'create' ]
          secrets: [ 'get', 'list', 'set' ]
          certificates: [ 'get', 'list', 'create' ]
        }
      }
    ]
  }
}

resource staticWebApp 'Microsoft.Web/staticSites@2021-01-15' = {
  name: '${applicationName}-swa'
  location: location
  tags: resourceTags
  properties: {
    buildProperties: {
      appLocation: staticWebAppAppLocation
      apiLocation: staticWebAppApiLocation
      appArtifactLocation: staticWebAppAppArtifactLocation
    }
  }
  sku: {
    tier: staticWebAppSku
    name: staticWebAppSkuNuma
  }
}

resource name_appsettings 'Microsoft.Web/staticSites/config@2021-01-15' = {
  parent: staticWebApp
  name: 'appsettings'
  properties: { BooKeeperWebAppConnectionString: userConnectionString }
}

resource webAppApiKey 'Microsoft.KeyVault/vaults/secrets@2019-09-01' = {
  name: 'webAppApiKey'
  parent: keyVault
  properties: {
    value: listSecrets(staticWebApp.id, staticWebApp.apiVersion).properties.apiKey
  }
}

resource dataBaseServer 'Microsoft.Sql/servers@2021-11-01-preview' = {
  name: databaseName
  location: location
  properties: {
    administratorLogin: adminLoginName
    administratorLoginPassword: adminLoginPassword
    version: '12.0'
    minimalTlsVersion: '1.2'
  }
}

resource allAzureIpsFirewallRul 'Microsoft.Sql/servers/firewallRules@2022-05-01-preview' = {
  name: 'AllowAllWindowsAzureIps'
  parent: dataBaseServer
  properties: {
    endIpAddress: '0.0.0.0'
    startIpAddress: '0.0.0.0'
  }
}

resource database 'Microsoft.Sql/servers/databases@2021-11-01-preview' = {
  parent: dataBaseServer
  name: '${applicationName}-db'
  location: location
  sku: {
    name: 'GP_S_Gen5'
    tier: 'GeneralPurpose'
    family: 'Gen5'
    capacity: 2
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 5368709120
    catalogCollation: 'SQL_Latin1_General_CP1_CI_AS'
    zoneRedundant: false
    readScale: 'Disabled'
    autoPauseDelay: 60
    minCapacity: any('0.5')
  }
}

resource dbConnectionString 'Microsoft.KeyVault/vaults/secrets@2019-09-01' = {
  name: 'dbConnectionString'
  parent: keyVault // Pass key vault symbolic name as parent
  properties: {
    value: adminConnectionString
  }
}
