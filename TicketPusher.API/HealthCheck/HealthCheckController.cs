using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Common;

namespace TicketPusher.API.HealthCheck
{
    [Route("api/healthcheck")]
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}