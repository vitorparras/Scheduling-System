using Domain.Enum;
using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Appointment : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; }

        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        public virtual User Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Store Store { get; set; }

        [InverseProperty(nameof(AppointmentHistory.Appointment))]
        public virtual ICollection<AppointmentHistory> History { get; set; } = new List<AppointmentHistory>();

        public string DescriptionAppointment()
        {
            return $"{Date.ToShortDateString()} from {StartTime} to {EndTime} - Status: {Status}";
        }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments) 
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Appointments) 
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Store)
                .WithMany(s => s.Appointments)  
                .HasForeignKey(a => a.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasMany(a => a.History)
                .WithOne(ah => ah.Appointment)
                .HasForeignKey(ah => ah.AppointmentId);
        }
    }
}
