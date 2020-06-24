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
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<ProjectDto, Error>>
    {
        private readonly ITicketPusherRepository _repository;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Result<ProjectDto, Error>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var parentProject = request.ParentProject != Guid.Empty ?
                await _repository.GetProjectAsync(request.ParentProject) :
                Project.None;

            Result<ProjectDto, Error> result = await Result.SuccessIf(
                parentProject != null,
                parentProject,
                Errors.General.NotFound(nameof(Project), request.ParentProject))
                .Map(async p => await CreateProject(request.Name, parentProject))
            ;

            return result;
        }

        private async Task<ProjectDto> CreateProject(string name, Project parentProject)
        {
            var project = new Project(name, parentProject);
            _repository.CreateProject(project);
            await _repository.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }
    }
}