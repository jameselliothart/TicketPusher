using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Common;
using TicketPusher.API.Projects.Commands;
using TicketPusher.API.Projects.Queries;

namespace TicketPusher.API.Projects
{
    [Route("api/projects")]
    public class ProjectsController : ApiController
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectList()
        {
            var query = new GetProjectListQuery();
            var result = await _mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var query = new GetProjectQuery(id);
            var result = await _mediator.Send(query);

            return FromResultWithValue(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto project)
        {
            var command = new CreateProjectCommand(project.Name, project.ParentProject);
            var result = await _mediator.Send(command);

            return Ok(result.Value);
        }
    }
}