using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Projects.Commands
{
    public class CreateProjectCommand : IRequest<Result<ProjectDto>>
    {
        public CreateProjectCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}