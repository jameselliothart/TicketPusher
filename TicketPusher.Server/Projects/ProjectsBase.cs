using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class ProjectsBase : EntityBase<ProjectDto, IProjectReadDataService>
    {
        public IEnumerable<ProjectDto> EditableProjects =>
            Entities?.Where(p => p.Id != Project.None.Id);

        protected override async Task RefreshData()
        {
            Entities = (await RetrieveMainEntities()).OrderBy(e => e.Hierarchy).ToList();
            StateHasChanged();
        }
    }
}