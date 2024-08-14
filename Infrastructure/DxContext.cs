using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure
{
    public class DxContext : DbContext
    {
        public DxContext(DbContextOptions<DxContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "teste@teste.com",
                    CreatedAt = DateTime.Now,
                    CPF = "123",
                    Name = "teste",
                    Password = "123456",
                });

            modelBuilder.Entity<Store>()
         .HasOne(s => s.Admin)
         .WithMany()
         .HasForeignKey(s => s.AdminID)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Store>()
                .HasOne(s => s.Calendar)
                .WithOne()
                .HasForeignKey<Store>(s => s.CalendarID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Store)
                .WithMany(s => s.Employees)
                .HasForeignKey(e => e.StoreID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Schedule)
                .WithOne()
                .HasForeignKey<Employee>(e => e.ScheduleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Store)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StoreID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Appointments)
                .HasForeignKey(a => a.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany()
                .HasForeignKey(a => a.ClientID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkScheduleConfig>()
                .HasOne(w => w.Employee)
                .WithOne(e => e.WorkScheduleConfig)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<TokenHistory> TokenHistories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Calendar> Calendars { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<WorkScheduleConfig> WorkScheduleConfigs { get; set; }

    }
}
