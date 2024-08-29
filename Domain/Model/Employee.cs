using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Employee : BaseEntity
    {
        [Required]
        public Guid ScheduleId { get; set; }

        [Required]
        public Guid WorkScheduleConfigId { get; set; }

        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public virtual Appointment Schedule { get; set; }

        public virtual Store Store { get; set; }

        public virtual User User { get; set; }

        public virtual WorkScheduleConfig WorkScheduleConfig { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

      
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Schedule)
                .WithMany()
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Store)
                .WithMany(s => s.Employees)
                .HasForeignKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithMany(u => u.Employees)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.WorkScheduleConfig)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Appointments)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
