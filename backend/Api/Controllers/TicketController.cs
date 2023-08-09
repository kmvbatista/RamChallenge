using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly TicketMapper _ticketMapper;
        private readonly LinkMapper _linkMapper;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
            _ticketMapper = new TicketMapper();
            _linkMapper = new LinkMapper();
        }

        [HttpPost]
        public async Task<TicketResponseModel> Create([FromBody] TicketRequestModel request)
        {
            Ticket ticket = MapTicketFromRequestModel(request);
            var createdTicket = await _ticketService.Create(ticket);
            return MapTicketToResponseModel(createdTicket);
        }
        [HttpGet]
        public async Task<IList<TicketResponseModel>> GetAll()
        {
            var tickets = await _ticketService.GetAll();
            return tickets.Select(MapTicketToResponseModel).ToList();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<TicketResponseModel> GetById([FromRoute] Guid id)
        {
            var ticket = await _ticketService.GetById(id);
            return MapTicketToResponseModel(ticket);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<TicketResponseModel> UpdateTicketById([FromRoute] Guid id, [FromBody] TicketRequestModel request)
        {
            var updatedTicket = await _ticketService.Update(id, request);
            return MapTicketToResponseModel(updatedTicket);

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
            var ticket = await _ticketService.AddTicketLink(ticketId, fileUrl);
            return MapTicketToResponseModel(ticket);
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
        private TicketResponseModel MapTicketToResponseModel(Ticket ticket)
        {
            var mappedTicket = _ticketMapper.MapToResponseModel(ticket);
            var ticketLinks = ticket?.Links?.Select(link => _linkMapper.MapToResponseModel(link))?.ToList();
            mappedTicket.links = ticketLinks;
            return mappedTicket;
        }
        private Ticket MapTicketFromRequestModel(TicketRequestModel ticket)
        {
            var mappedTicket = _ticketMapper.MapFromRequestModel(ticket);
            mappedTicket.Links = ticket.Links.Select(x => _linkMapper.MapFromRequestModel(x)).ToList();
            mappedTicket.Validate();
            return mappedTicket;
        }
    }
}
