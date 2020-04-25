using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketPusher.Domain.CompletedTickets;
using TicketPusher.Domain.Projects;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Data
{
    public interface ITicketPusherRepository
    {
        void CreateTicket(Ticket ticket);
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketAsync(Guid ticketId);
        void RemoveTicket(Ticket ticket);
        bool SaveChanges();
        Task<bool> SaveChangesAsync();
        Task<Project> GetProjectAsync(Guid projectId);
        void CreateProject(Project project);
        void CreateCompletedTicket(CompletedTicket completedTicket);
        CompletedTicket CloseTicket(Ticket ticket, string resolution);
        Task<List<CompletedTicket>> GetCompletedTicketsAsync();
        Task<List<Project>> GetProjectsListAsync();
    }
}