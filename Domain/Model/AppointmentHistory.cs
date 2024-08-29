using Domain.Enum;
using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class AppointmentHistory : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid AppointmentId { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; }

        public virtual Appointment Appointment { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentHistory>()
                .HasOne(ah => ah.Appointment)
                .WithMany(a => a.History) 
                .HasForeignKey(ah => ah.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
