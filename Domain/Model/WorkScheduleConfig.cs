using Domain.Enum;
using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class WorkScheduleConfig : BaseEntity
    {
        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public DayWeekEnum DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public TimeSpan? LunchStartTime { get; set; }

        public TimeSpan? LunchEndTime { get; set; }

        [Required]
        public TimeSpan AppointmentDuration { get; set; }

        [Required]
        public TimeSpan CleaningTime { get; set; }

        public virtual Employee Employee { get; set; }

        // Método para configurações da model
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkScheduleConfig>()
                .HasOne(wsc => wsc.Employee)
                .WithOne(e => e.WorkScheduleConfig)
                .HasForeignKey<WorkScheduleConfig>(wsc => wsc.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
