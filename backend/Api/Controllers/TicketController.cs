using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketRequestModel request)
        {
            var response = await _ticketService.Create(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IList<TicketResponseModel>> GetAll()
        {
            return await _ticketService.GetAll();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<TicketResponseModel> GetById([FromRoute] Guid id)
        {
            return await _ticketService.GetById(id);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<TicketResponseModel> UpdateTicketById([FromRoute] Guid id, [FromBody] TicketRequestModel request)
        {
            return await _ticketService.Update(id, request);
        }
        [Route("{id}")]
        [HttpDelete]
        public async Task Delete([FromRoute] Guid id)
        {
            await _ticketService.Delete(id);
        }
        [Route("file")]
        [HttpPost]
        public async Task<IActionResult> UploadFileEndoint()
        {
            var file = Request.Form.Files[0];
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");
            var fileUrl = await UploadFile(file);
            return Ok(fileUrl);
        }
        [Route("{ticketId}/file")]
        [HttpPost]
        public async Task<TicketResponseModel> AddTicketLink([FromRoute] Guid ticketId)
        {
            var file = Request.Form.Files[0];
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");
            var fileUrl = await UploadFile(file);
            return await _ticketService.AddTicketLink(ticketId, fileUrl);
        }
        [Route("file/{linkId}")]
        [HttpDelete]
        public async Task DeleteTicketLink([FromRoute] Guid linkId)
        {
            await _ticketService.RemoveTicketLink(linkId);
        }
        private async Task<string> UploadFile(IFormFile file)
        {
            StorageSharedKeyCredential storageCredentials =
                    new StorageSharedKeyCredential(Environment.GetEnvironmentVariable("AZURE_ACCOUNT_NAME"), Environment.GetEnvironmentVariable("AZURE_ACCOUNT_KEY"));
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            Uri blobUri = new Uri("https://" +
                            Environment.GetEnvironmentVariable("AZURE_ACCOUNT_NAME") +
                            ".blob.core.windows.net/" +
                            Environment.GetEnvironmentVariable("AZURE_CONTAINER_NAME") +
                            "/" + fileName);

            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);
            BlobUploadOptions options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "image/jpg"
                }
            };
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, options);
            }

            await Task.FromResult(true);
            return blobUri.ToString();

        }
    }
}
