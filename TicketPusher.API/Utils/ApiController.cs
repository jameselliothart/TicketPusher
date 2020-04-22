using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace TicketPusher.API.Utils
{
    [ApiController]
    public class ApiController: ControllerBase
    {
        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult FromResultWithValue<T, E>(Result<T, E> result)
            where E : Error
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            if (result.Error == Errors.General.NotFound())
                return NotFound(Envelope.Error(result.Error.Message));

            return BadRequest(Envelope.Error(result.Error.Message));
        }
    }
}