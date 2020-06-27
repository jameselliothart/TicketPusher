using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Common
{
    [ApiController]
    public class ApiController: ControllerBase
    {
        protected new IActionResult Ok() =>
            base.Ok(Envelope.Ok());

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));

            // TODO: test this alternative to simplify call on client side
            // return result switch
            // {
            //     Result<T> r => base.Ok(Envelope.Ok(r.Value)),
            //     { } => base.Ok(Envelope.Ok(result))
            // };
        }

        protected IActionResult ValueOrError<T, E>(Result<T, E> result)
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