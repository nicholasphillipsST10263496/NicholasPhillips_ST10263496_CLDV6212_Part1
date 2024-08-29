using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;


namespace NicholasPhillips_ST10263496_CLDV6221_Part1.Services
{
    public class QueueService
    {
        // The QueueServiceClient is used to interact with the Azure Queue Storage service
        private readonly QueueServiceClient _queueServiceClient;

        // Constructor that initializes the QueueServiceClient using the connection string from the configuration
        public QueueService(IConfiguration configuration)
        {
            // Initialize a new instance of the QueueServiceClient using the connection string from the configuration settings
            _queueServiceClient = new QueueServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        // Method to send a message to the specified queue asynchronously
        public async Task SendMessageAsync(string queueName, string message)
        {
            // Get a reference to the specific queue using the provided queue name
            var queueClient = _queueServiceClient.GetQueueClient(queueName);

            // Create the queue if it does not already exist
            await queueClient.CreateIfNotExistsAsync();

            // Send the provided message to the queue
            await queueClient.SendMessageAsync(message);
        }
    }
}