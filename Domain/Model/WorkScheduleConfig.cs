using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class WorkScheduleConfig : BaseEntity
    {
        /// <summary>
        /// Identifier of the employee associated with the work schedule configuration.
        /// </summary>
        [ForeignKey("Employee")]
        public Guid EmployeeID { get; set; }
        public Employee Employee { get; set; }

        /// <summary>
        /// Day of the week (0 = Sunday, 1 = Monday, etc.).
        /// </summary>
        [Required]
        public int DayOfWeek { get; set; }

        /// <summary>
        /// Start time of the work day.
        /// </summary>
        [Required]
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// End time of the work day.
        /// </summary>
        [Required]
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Start time of the lunch break.
        /// </summary>
        public TimeSpan? LunchStartTime { get; set; }

        /// <summary>
        /// End time of the lunch break.
        /// </summary>
        public TimeSpan? LunchEndTime { get; set; }

        /// <summary>
        /// Duration of each appointment.
        /// </summary>
        [Required]
        public TimeSpan AppointmentDuration { get; set; }

        /// <summary>
        /// Additional time for cleaning and preparation for the next appointment.
        /// </summary>
        [Required]
        public TimeSpan CleaningTime { get; set; }
    }
}
