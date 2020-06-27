using System;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;

namespace TicketPusher.API.Projects.Commands
{
    public class CreateProjectCommand : IRequest<Result<ProjectDto, Error>>
    {
        public CreateProjectCommand(string name, Guid parentProjectId)
        {
            Name = name;
            ParentProjectId = parentProjectId == Guid.Empty ? Project.None.Id : parentProjectId;
        }

        public string Name { get; }
        public Guid ParentProjectId { get; }
    }
}