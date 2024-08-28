using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enum;

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

        
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }
    }
}
