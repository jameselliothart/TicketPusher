using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TicketPusher.Domain.CompletedTickets;
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
        public DbSet<Project> Projects { get; set; }
        public DbSet<CompletedTicket> CompletedTickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var propNameProjectFK = "ProjectFK";

            modelBuilder.Entity<Ticket>().Property<Guid>(propNameProjectFK).HasColumnName("project_id");

            modelBuilder.Entity<Ticket>(x =>
            {
                x.ToTable("ticket").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("ticket_id");
                x.Property(p => p.Owner).HasColumnName("owner").IsRequired();
                x.OwnsOne(p => p.TicketDetails, p =>
                {
                    p.Property(pp => pp.Description).HasColumnName("description").IsRequired();
                    p.Property(pp => pp.SubmitDate).HasColumnName("submit_date").IsRequired();
                    p.Property(pp => pp.DueDate).HasColumnName("due_date").IsRequired();
                });
                x.HasOne(p => p.Project).WithMany().HasForeignKey(propNameProjectFK).IsRequired();
            });

            modelBuilder.Entity<CompletedTicket>().Property<Guid>(propNameProjectFK).HasColumnName("project_id");

            modelBuilder.Entity<CompletedTicket>(x =>
            {
                x.ToTable("completed_ticket").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("completed_ticket_id");
                x.Property(p => p.Owner).HasColumnName("owner").IsRequired();
                x.OwnsOne(p => p.TicketDetails, p =>
                {
                    p.Property(pp => pp.Description).HasColumnName("description").IsRequired();
                    p.Property(pp => pp.SubmitDate).HasColumnName("submit_date").IsRequired();
                    p.Property(pp => pp.DueDate).HasColumnName("due_date").IsRequired();
                });
                x.HasOne(p => p.Project).WithMany().HasForeignKey(propNameProjectFK).IsRequired();
                x.OwnsOne(p => p.CompletedDetails, p =>
                {
                    p.Property(pp => pp.CompletionDate).HasColumnName("completion_date").IsRequired();
                    p.Property(pp => pp.Resolution).HasColumnName("resolution").IsRequired();
                });
            });

            modelBuilder.Entity<Project>().Property<Guid?>(propNameProjectFK).HasColumnName("parent_project_id").IsRequired(false);

            modelBuilder.Entity<Project>(x =>
            {
                x.ToTable("project").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("project_id");
                x.Property(p => p.Name).HasColumnName("name").IsRequired();
                x.HasOne(p => p.ParentProject).WithMany().HasForeignKey(propNameProjectFK).IsRequired(false);
            });

            modelBuilder.Entity<Project>().HasData(Project.None);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var referenceData = ChangeTracker.Entries()
                .Where(x => x.Entity is Project && (Project)x.Entity == Project.None);
            foreach (var referenceDatum in referenceData)
            {
                referenceDatum.State = EntityState.Unchanged;
            }

            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}