using Microsoft.EntityFrameworkCore;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Data
{
    public sealed class TicketPusherContext : DbContext
    {
        public TicketPusherContext(DbContextOptions<TicketPusherContext> options)
            : base(options)
        {
        }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(x =>
            {
                x.ToTable("Ticket").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("TicketId");
                x.Property(p => p.Owner).IsRequired();
                x.Property(p => p.Description).IsRequired();
            });
        }
    }
}