using System.Net.Http;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class ProjectReadDataService : EntityReadDataService<ProjectDto>, IProjectReadDataService
    {

        public ProjectReadDataService(HttpClient httpClient) : base(httpClient, "projects")
        {
        }

    }
}