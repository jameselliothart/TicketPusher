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
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<ProjectDto, Error>>
    {
        private readonly ITicketPusherRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ProjectDto, Error>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            // TODO: enable this
            // var name = string.IsNullOrWhiteSpace(request.Name) ? project.Name : request.Name;
            // project.UpdateName(name);

            return await _repository.EntityOrNotFound(request.ProjectId, async id => await _repository.GetProjectAsync(id))
                .Bind(async p => await UpdateProject(request, p))
                ;
        }

        private async Task<Result<ProjectDto, Error>> UpdateProject(UpdateProjectCommand request, Project project)
        {
            if (request.ParentProjectId == project.ParentProject.Id)
            {
                ProjectDto projectToReturn = _mapper.Map<ProjectDto>(project);
                return Result.SuccessIf(projectToReturn != null, projectToReturn, Errors.General.NotFound());
            }

            return await
                (await _repository.EntityOrNotFound(request.ParentProjectId, async id => await _repository.GetProjectAsync(id)))
                .Map(async parent =>
                {
                    project.SetParentProject(parent);
                    _repository.UpdateProject(project);
                    await _repository.SaveChangesAsync();
                    return _mapper.Map<ProjectDto>(project);
                })
            ;
        }
    }
}