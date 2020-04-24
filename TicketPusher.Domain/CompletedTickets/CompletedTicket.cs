using System;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.Projects;

namespace TicketPusher.Domain.CompletedTickets
{
    public class CompletedTicket : Entity
    {
        public string Owner { get; private set; }
        public Project Project { get; private set; }
        public TicketDetails TicketDetails { get; private set; }
        public CompletedDetails CompletedDetails { get; private set; }

        private CompletedTicket() {}

        public CompletedTicket(Guid id, string owner, Project project, TicketDetails ticketDetails, CompletedDetails completedDetails)
            : base(id)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Project = project ?? throw new ArgumentNullException(nameof(project));
            TicketDetails = ticketDetails ?? throw new ArgumentNullException(nameof(ticketDetails));
            CompletedDetails = completedDetails ?? throw new ArgumentNullException(nameof(completedDetails));
        }

    }
}