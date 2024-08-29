using Domain.Model;
using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Infrastructure
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext(DbContextOptions<SchedulerContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<WorkScheduleConfig> WorkScheduleConfigs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Appointment.Configure(modelBuilder);
            AppointmentHistory.Configure(modelBuilder);
            Calendar.Configure(modelBuilder);
            Store.Configure(modelBuilder);
            User.Configure(modelBuilder);
            WorkScheduleConfig.Configure(modelBuilder);
            Employee.Configure(modelBuilder);
            LoginHistory.Configure(modelBuilder);
            Notification.Configure(modelBuilder);
            SystemSetting.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
