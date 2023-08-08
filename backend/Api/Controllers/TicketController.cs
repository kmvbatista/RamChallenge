using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
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
        [Route("{ticketId}/image")]
        [HttpPost]
        public async Task<TicketResponseModel> AddTicketLink([FromRoute] Guid ticketId)
        {
            var file = Request.Form.Files[0];
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");
            var fileUrl = await UploadFile(file);
            return await _ticketService.AddTicketLink(ticketId, fileUrl);
        }

        private async Task<string> UploadFile(IFormFile file)
        {
            StorageSharedKeyCredential storageCredentials =
                    new StorageSharedKeyCredential("cs210032000e93dbb8e", "fxZTAGic2k6Uuk2hndDTa+eraKCYlW77+fkeJdMrBwa9AFORDHk/VVH3N+zWZHOrNAnvd6W0ikzT+AStmoAoSw==");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            Uri blobUri = new Uri("https://" +
                            "cs210032000e93dbb8e" +
                            ".blob.core.windows.net/" +
                            "new" +
                            "/" + fileName);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            // Upload the file
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            await Task.FromResult(true);
            return blobUri.ToString();

        }
    }
}
