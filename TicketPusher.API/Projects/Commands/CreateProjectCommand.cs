using System;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Projects.Commands
{
    public class CreateProjectCommand : IRequest<Result<ProjectDto, Error>>
    {
        public CreateProjectCommand(string name, Guid parentProject)
        {
            Name = name;
            ParentProject = parentProject;
        }

        public string Name { get; }
        public Guid ParentProject { get; }
    }
}