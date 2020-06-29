using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Common;
using TicketPusher.API.Projects.Commands;
using TicketPusher.API.Projects.Queries;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.Projects;

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
            Result<IEnumerable<ProjectDto>> result = await _mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var query = new GetProjectQuery(id);
            Result<ProjectDto, Error> result = await _mediator.Send(query);

            return ValueOrError(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto project)
        {
            var command = new CreateProjectCommand(project.Name, project.ParentProjectId);
            Result<ProjectDto, Error> result = await _mediator.Send(command);

            return ValueOrError(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectDto project)
        {
            var command = new UpdateProjectCommand(id, project.Name, project.ParentProjectId);
            Result<ProjectDto, Error> result = await _mediator.Send(command);

            return ValueOrError(result);
        }
    }
}