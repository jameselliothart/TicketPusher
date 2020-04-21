using Microsoft.EntityFrameworkCore;
using TicketPusher.Domain.Projects;
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
                x.ToTable("tickets").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("ticket_id");
                x.Property(p => p.Owner).HasColumnName("owner").IsRequired();
                x.OwnsOne(p => p.TicketDetails, p =>
                {
                    p.Property(pp => pp.Description).HasColumnName("description").IsRequired();
                    p.Property(pp => pp.SubmitDate).HasColumnName("submit_date").IsRequired();
                    p.Property(pp => pp.DueDate).HasColumnName("due_date").IsRequired();
                });
                x.HasOne(p => p.Project).WithMany();
            });

            modelBuilder.Entity<Project>(x =>
            {
                x.ToTable("projects").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("project_id");
                x.Property(p => p.Name).HasColumnName("name").IsRequired();
            });
        }
    }
}