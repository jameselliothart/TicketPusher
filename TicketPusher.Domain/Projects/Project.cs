using System;
using CSharpFunctionalExtensions;
using TicketPusher.Domain.Common;

namespace TicketPusher.Domain.Projects
{
    public class Project : Entity
    {
        public string Name { get; private set; }
        public Maybe<Project> ParentProject { get; private set; }

        private Project() {}

        public Project(string name) : this(name, Maybe<Project>.None)
        {
        }

        public Project(string name, Maybe<Project> parentProject) : base()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ParentProject = parentProject;
        }

        public void UpdateParentProject(Maybe<Project> project)
        {
            if (project == this) throw new InvalidOperationException($"Cannot set a project as its own parent: {Id}|{Name}");
            ParentProject = project;
        }

    }
}