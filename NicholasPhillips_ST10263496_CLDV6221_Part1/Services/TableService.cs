using Azure;
using Azure.Data.Tables;
using NicholasPhillips_ST10263496_CLDV6221_Part1.Models;
using System.Threading.Tasks;

namespace NicholasPhillips_ST10263496_CLDV6221_Part1.Services
{
    public class TableService
    {
        // The TableClient is used to interact with the Azure Table Storage service
        private readonly TableClient _tableClient;

        // Constructor that initializes the TableClient using the connection string from the configuration
        public TableService(IConfiguration configuration)
        {
            // Retrieve the Azure Storage connection string from the configuration settings
            var connectionString = configuration["AzureStorage:ConnectionString"];

            // Initialize a new instance of the TableServiceClient using the connection string
            var serviceClient = new TableServiceClient(connectionString);

            // Get a reference to the specific table "CustomerProfiles"
            _tableClient = serviceClient.GetTableClient("CustomerProfiles");

            // Create the table "CustomerProfiles" if it does not already exist
            _tableClient.CreateIfNotExists();
        }

        // Method to add a new entity (CustomerProfile) to the table asynchronously
        public async Task AddEntityAsync(CustomerProfile profile)
        {
            // Add the provided CustomerProfile entity to the "CustomerProfiles" table
            await _tableClient.AddEntityAsync(profile);
        }
    }
}