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
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusRequestModel request)
        {
            var response = await _statusService.Create(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IList<StatusResponseModel>> GetAll()
        {
            return await _statusService.GetAll();
        }
        [Route("{id}")]
        [HttpDelete]
        public async Task Delete([FromRoute] Guid id)
        {
            await _statusService.Delete(id);
        }
    }
}
