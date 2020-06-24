using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketPusher.Domain.CompletedTickets;
using TicketPusher.Domain.Projects;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Data
{

    public class TicketPusherRepository : ITicketPusherRepository, IDisposable
    {
        private readonly TicketPusherContext _context;

        public TicketPusherRepository(TicketPusherContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));
            if (ticket.Id == Guid.Empty)
                throw new ArgumentException($"Ticket Id {ticket.Id} cannot be empty");

            _context.Tickets.Add(ticket);
        }

        public async Task<Ticket> GetTicketAsync(Guid ticketId)
        {
            return await _context.Tickets.Where(t => t.Id == ticketId).Include(t => t.Project).FirstOrDefaultAsync();
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.Include(t => t.Project).ToListAsync();
        }

        public void RemoveTicket(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }

        public void CreateProject(Project project)
        {
            _context.Projects.Add(project);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Project> GetProjectAsync(Guid projectId)
        {
            return await _context.Projects.Where(p => p.Id == projectId).Include(p => p.ParentProject).FirstOrDefaultAsync();
        }

        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
        }

        public void CreateCompletedTicket(CompletedTicket completedTicket)
        {
            _context.CompletedTickets.Add(completedTicket);
        }

        public CompletedTicket CloseTicket(Ticket ticket, string resolution)
        {
            var completedTicket = ticket.Close(resolution);
            RemoveTicket(ticket);
            CreateCompletedTicket(completedTicket);
            return completedTicket;
        }

        public async Task<List<CompletedTicket>> GetCompletedTicketsAsync()
        {
            return await _context.CompletedTickets.Include(t => t.Project).ToListAsync();
        }

        public async Task<List<Project>> GetProjectsListAsync()
        {
            return await _context.Projects.Include(p => p.ParentProject).ToListAsync();
        }
    }
}