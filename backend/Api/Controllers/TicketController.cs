using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        [Route("{id}")]
        [HttpDelete]
        public async Task Delete([FromRoute] Guid id)
        {
            await _ticketService.Delete(id);
        }
    }
}
