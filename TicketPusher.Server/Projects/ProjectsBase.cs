using System.Threading.Tasks;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class ProjectsBase : EntityBase<ProjectDto, IProjectReadDataService>
    {

        protected override async Task RefreshData()
        {
            Entities = await RetrieveMainEntities();
            StateHasChanged();
        }
    }
}