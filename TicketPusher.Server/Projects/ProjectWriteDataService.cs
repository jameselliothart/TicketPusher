using System.Net.Http;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class ProjectWriteDataService : EntityWriteDataService<ProjectDto, CreateProjectDto>, IProjectWriteDataService
    {

        public ProjectWriteDataService(HttpClient httpClient) : base(httpClient, "projects")
        {
        }

    }
}