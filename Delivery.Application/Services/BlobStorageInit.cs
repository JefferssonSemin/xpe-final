using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Delivery.Application.Services;

public abstract class BlobStorageInit
{
    public readonly BlobContainerClient BlobContainerClient;
    
    public BlobStorageInit(IConfiguration configuration)
    {
        var storageAccountName = configuration["StorageAccount:Name"];
        var containerName = configuration["StorageAccount:ContainerName"];

        var tenantId = configuration["AzureAd:TenantId"];
        var clientId = configuration["AzureAd:ClientId"];
        var clientSecret = configuration["AzureAd:ClientSecret"];
        
        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        
        var blobServiceUri = new Uri($"https://{storageAccountName}.blob.core.windows.net");
        
        var blobServiceClient = new BlobServiceClient(blobServiceUri, credential);
        
        BlobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
    }

}