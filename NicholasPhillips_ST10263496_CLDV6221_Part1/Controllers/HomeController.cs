using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NicholasPhillips_ST10263496_CLDV6221_Part1.Models;
using NicholasPhillips_ST10263496_CLDV6221_Part1.Services;
namespace NicholasPhillips_ST10263496_CLDV6221_Part1.Controllers
{
    public class HomeController : Controller
    {
        // Services for interacting with Azure Blob Storage, Table Storage, Queue Storage, and File Storage
        private readonly BlobService _blobService;
        private readonly TableService _tableService;
        private readonly QueueService _queueService;
        private readonly FileService _fileService;

        // Constructor that initializes the controller with the necessary services
        public HomeController(BlobService blobService, TableService tableService, QueueService queueService, FileService fileService)
        {
            _blobService = blobService;
            _tableService = tableService;
            _queueService = queueService;
            _fileService = fileService;
        }

        // Action method that returns the default view for the Index page
        public IActionResult Index()
        {
            return View();
        }

        // Action method to handle the uploading of an image to Azure Blob Storage
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // Check if a file was uploaded
            if (file != null)
            {
                // Open a stream to read the uploaded file's content
                using var stream = file.OpenReadStream();

                // Upload the file to the "product-images" container in Azure Blob Storage
                await _blobService.UploadBlobAsync("product-images", file.FileName, stream);
            }

            // Redirect back to the Index page after uploading
            return RedirectToAction("Index");
        }

        // Action method to handle the addition of a customer profile to Azure Table Storage
        [HttpPost]
        public async Task<IActionResult> AddCustomerProfile(CustomerProfile profile)
        {
            // Check if the model state is valid before proceeding
            if (ModelState.IsValid)
            {
                // Add the customer profile entity to the Azure Table Storage
                await _tableService.AddEntityAsync(profile);
            }

            // Redirect back to the Index page after adding the profile
            return RedirectToAction("Index");
        }

        // Action method to handle the processing of an order by sending a message to Azure Queue Storage
        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string orderId)
        {
            // Send a message to the "order-processing" queue indicating that the order is being processed
            await _queueService.SendMessageAsync("order-processing", $"Processing order {orderId}");

            // Redirect back to the Index page after processing the order
            return RedirectToAction("Index");
        }

        // Action method to handle the uploading of a contract file to Azure File Storage
        [HttpPost]
        public async Task<IActionResult> UploadContract(IFormFile file)
        {
            // Check if a file was uploaded
            if (file != null)
            {
                // Open a stream to read the uploaded file's content
                using var stream = file.OpenReadStream();

                // Upload the file to the "contracts-logs" share in Azure File Storage
                await _fileService.UploadFileAsync("contracts-logs", file.FileName, stream);
            }

            // Redirect back to the Index page after uploading
            return RedirectToAction("Index");
        }
    }
}