using System;
using System.Collections.Generic;
using TicketPusher.API.Projects;

namespace TicketPusher.Server.Utils
{
    public static class GuidExtensions
    {
        public static string ToShortGuid(this Guid guid) => guid.ToString().Split("-")[0];

        public static string ToProjectName(this Guid guid, List<ProjectDto> projects) =>
            projects.Find(p => p.Id == guid).Name;
    }
}