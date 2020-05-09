using System;
using TicketPusher.Domain.Common;

namespace TicketPusher.Domain.Projects
{
    public class Project : Entity
    {
        public string Name { get; private set; }

        private Project() {}

        public Project(string name) : base()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

    }
}