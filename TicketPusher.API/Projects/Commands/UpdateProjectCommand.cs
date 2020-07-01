using System;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;

namespace TicketPusher.API.Projects.Commands
{
    public class UpdateProjectCommand : IRequest<Result<ProjectDto, Error>>
    {
        public UpdateProjectCommand(Guid projectId, string name, Guid parentProjectId)
        {
            ProjectId = projectId;
            Name = name;
            ParentProjectId = parentProjectId == Guid.Empty ? Project.None.Id : parentProjectId;
        }

        public Guid ProjectId { get; }
        public string Name { get; }
        public Guid ParentProjectId { get; }
    }
}