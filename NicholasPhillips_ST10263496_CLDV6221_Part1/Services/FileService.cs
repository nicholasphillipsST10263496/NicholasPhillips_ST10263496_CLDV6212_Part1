using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;


namespace NicholasPhillips_ST10263496_CLDV6221_Part1.Services;
public class FileService
{
    // The ShareServiceClient is used to interact with Azure File Storage service
    private readonly ShareServiceClient _shareServiceClient;

    // Constructor that initializes the ShareServiceClient using the connection string from the configuration
    public FileService(IConfiguration configuration)
    {
        // Initialize a new instance of the ShareServiceClient using the connection string from the configuration settings
        _shareServiceClient = new ShareServiceClient(configuration["AzureStorage:ConnectionString"]);
    }

    // Method to upload a file to the specified file share asynchronously
    public async Task UploadFileAsync(string shareName, string fileName, Stream content)
    {
        // Get a reference to the specific file share using the provided share name
        var shareClient = _shareServiceClient.GetShareClient(shareName);

        // Create the file share if it does not already exist
        await shareClient.CreateIfNotExistsAsync();

        // Get a reference to the root directory within the file share
        var directoryClient = shareClient.GetRootDirectoryClient();

        // Get a reference to the specific file within the directory using the provided file name
        var fileClient = directoryClient.GetFileClient(fileName);

        // Create the file in Azure File Storage with the specified size
        await fileClient.CreateAsync(content.Length);

        // Upload the content to the created file
        await fileClient.UploadAsync(content);
    }
}