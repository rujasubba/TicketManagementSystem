using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Persistent
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TicketLog> TicketLogs { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssigenedUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Priority)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Department)
            .WithMany(d => d.Tickets)
            .HasForeignKey(t => t.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(t => t.Ticket)
                .WithMany(c => c.Comments)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(u => u.CreatedByUser)
                .WithMany(c => c.Comments)
                .HasForeignKey(u => u.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketLog>()
               .HasOne(u => u.AssignedUser)
               .WithMany(t => t.TicketLogs)
               .HasForeignKey(u => u.AssignedUserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketLog>()
              .HasOne(t => t.Ticket)
              .WithMany(t => t.TicketLogs)
              .HasForeignKey(t => t.TicketId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attachment>()
              .HasOne(t => t.Ticket)
              .WithMany(a => a.Attachments)
              .HasForeignKey(t => t.TicketId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Priority>().HasData(
            new Priority { PriorityId = 1, Name = "Low" },
            new Priority { PriorityId = 2, Name = "Medium" },
            new Priority { PriorityId = 3, Name = "High" }

     );
        modelBuilder.Entity<Status>().HasData(
        new Status { StatusId = 1, Name = "Open" },
        new Status { StatusId = 2, Name = "Closed" },
        new Status { StatusId = 3, Name = "In Progress" },
        new Status { StatusId = 4, Name = "Declined" }

   );

            base.OnModelCreating(modelBuilder);
        }


    }
}
