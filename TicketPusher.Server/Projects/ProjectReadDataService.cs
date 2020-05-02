using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;
using TicketPusher.API.Utils;
using TicketPusher.Server.Shared;
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