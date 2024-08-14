using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Appointment : BaseEntity
    {
        /// <summary>
        /// Identifier of the store associated with the appointment.
        /// </summary>
        [ForeignKey("Store")]
        public Guid StoreID { get; set; }
        public Store Store { get; set; }

        /// <summary>
        /// Identifier of the employee associated with the appointment.
        /// </summary>
        [ForeignKey("Employee")]
        public Guid EmployeeID { get; set; }
        public Employee Employee { get; set; }

        /// <summary>
        /// Identifier of the client associated with the appointment.
        /// </summary>
        [ForeignKey("Client")]
        public Guid ClientID { get; set; }
        public User Client { get; set; }

        /// <summary>
        /// Date of the appointment.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Start time of the appointment.
        /// </summary>
        [Required]
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// End time of the appointment.
        /// </summary>
        [Required]
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Status of the appointment.
        /// Can be 'Scheduled', 'Completed', or 'Cancelled'.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
