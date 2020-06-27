using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.Projects;
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
            return await
                (await _repository.EntityOrNotFound(request.ParentProjectId, async id => await _repository.GetProjectAsync(id)))
                .Map(async p => await CreateProject(request.Name, p))
            ;
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