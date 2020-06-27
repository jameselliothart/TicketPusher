namespace TicketPusher.Domain.Projects
{
    public static class HierarchyBuilder
    {
        public static string GetHierarchy(Project project, string accumulator = "")
        {
            if (project == Project.None)
                return string.IsNullOrEmpty(accumulator) ? "/" : accumulator;

            return GetHierarchy(project.ParentProject, $"/{project.Name}{accumulator}");
        }
    }
}