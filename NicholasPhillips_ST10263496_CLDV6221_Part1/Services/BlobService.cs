using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;


namespace NicholasPhillips_ST10263496_CLDV6221_Part1.Services
{
    public class BlobService
    {
        // The BlobServiceClient is used to interact with Azure Blob Storage service
        private readonly BlobServiceClient _blobServiceClient;

        // Constructor that initializes the BlobServiceClient using the connection string from the configuration
        public BlobService(IConfiguration configuration)
        {
            // Initialize a new instance of the BlobServiceClient using the connection string from the configuration settings
            _blobServiceClient = new BlobServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        // Method to upload a blob to the specified container asynchronously
        public async Task UploadBlobAsync(string containerName, string blobName, Stream content)
        {
            // Get a reference to the specific blob container using the provided container name
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            // Create the blob container if it does not already exist
            await containerClient.CreateIfNotExistsAsync();

            // Get a reference to the specific blob within the container using the provided blob name
            var blobClient = containerClient.GetBlobClient(blobName);

            // Upload the content to the created blob
            // The 'true' parameter indicates that if the blob already exists, it should be overwritten
            await blobClient.UploadAsync(content, true);
        }
    }
}