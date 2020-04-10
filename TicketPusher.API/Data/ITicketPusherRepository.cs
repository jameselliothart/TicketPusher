using System;
using System.Collections.Generic;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Data
{
    public interface ITicketPusherRepository
    {
        void CreateTicket(Ticket ticket);
        List<Ticket> GetAllTickets();
        Ticket GetTicket(Guid ticketId);
        void RemoveTicket(Ticket ticket);
        int SaveChanges();
    }
}