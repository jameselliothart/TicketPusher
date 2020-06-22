using System;
using CSharpFunctionalExtensions;
using TicketPusher.Domain.Common;

namespace TicketPusher.Domain.Projects
{
    public class Project : Entity
    {
        public string Name { get; private set; }
        public Project ParentProject { get; private set; }

        public static readonly Project None = new Project(Guid.Parse("11111111-1111-1111-1111-111111111111"), string.Empty);

        private Project() {}

        private Project(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public Project(string name) : this(name, None)
        {
        }

        public Project(string name, Project parentProject) : base()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            SetParentProject(parentProject);
        }

        public void SetParentProject(Maybe<Project> project)
        {
            if (project == this) throw new InvalidOperationException($"Cannot set a project as its own parent: {Id}|{Name}");
            ParentProject = project.Unwrap(None);
        }

    }
}