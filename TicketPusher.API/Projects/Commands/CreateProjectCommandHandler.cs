using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Projects;

namespace TicketPusher.API.Projects.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<ProjectDto>>
    {
        private readonly ITicketPusherRepository _repository;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Result<ProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Name);
            _repository.CreateProject(project);
            await _repository.SaveChangesAsync();

            var projectToReturn = _mapper.Map<ProjectDto>(project);
            return Result.SuccessIf(projectToReturn != null, projectToReturn, $"Error mapping project {project.Name}");
        }
    }
}