$providerNamespace = "Microsoft.DocumentDB"

# Check the current registration state
$providerState = az provider show --namespace $providerNamespace --query "registrationState" -o tsv

if ($providerState -eq "Registered") {
    Write-Host "The provider '$providerNamespace' is already registered."
} else {
    Write-Host "The provider '$providerNamespace' is not registered. Registering now..."
    
    # Register the provider
    az provider register --namespace $providerNamespace

    # Wait for registration to complete
    do {
        Write-Host "Waiting for the provider to register..."
        Start-Sleep -Seconds 10
        $providerState = az provider show --namespace $providerNamespace --query "registrationState" -o tsv
    } while ($providerState -ne "Registered")

    Write-Host "The provider '$providerNamespace' has been successfully registered."
}
