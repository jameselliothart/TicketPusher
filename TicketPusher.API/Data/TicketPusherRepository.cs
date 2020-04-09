using System;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Data
{
    public class TicketPusherRepository : IDisposable
    {
        private readonly TicketPusherContext _context;

        public TicketPusherRepository(TicketPusherContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreateTicket(Ticket ticket)
        {
            if (ticket.Id == Guid.Empty) 
                throw new ArgumentException($"Ticket Id {ticket.Id} cannot be empty");
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            _context.Tickets.Add(ticket);
        }

        public void RemoteTicket(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}