using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;

namespace TicketPusher.Server.Projects
{
    public class ProjectsBase : ComponentBase
    {
        [Inject]
        private IProjectDataService projectDataService { get; set; }

        private List<ProjectDto> _projects = new List<ProjectDto>();
        public IReadOnlyList<ProjectDto> projects => _projects.ToList();

        protected override async Task OnInitializedAsync()
        {
            var data = await projectDataService.GetProjectListAsync();
            _projects.AddRange(data.Result);
        }
    }
}