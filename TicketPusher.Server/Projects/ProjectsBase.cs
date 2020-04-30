using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class ProjectsBase : EntityBase<ProjectDto, IProjectDataService>
    {
        [Inject]
        private IModalService _modal { get; set; }

        protected override async Task<List<ProjectDto>> RefreshData()
        {
            var data = await EntityDataService.GetProjectListAsync();
            return data.Result;
        }

        public void AddProject()
        {
            _modal.Show<EditProject>("Add Project");
        }

    }
}