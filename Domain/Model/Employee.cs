using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Identifier of the store where the employee works.
        /// </summary>
        [ForeignKey("Store")]
        public Guid StoreID { get; set; }
        public Store Store { get; set; }

        /// <summary>
        /// Identifier of the user corresponding to the employee.
        /// </summary>
        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Name of the employee.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Identifier of the employee's schedule.
        /// </summary>
        [ForeignKey("Schedule")]
        public Guid ScheduleID { get; set; }
        public Appointment Schedule { get; set; }

        /// <summary>
        /// Identifier of the the work schedule configuration
        /// </summary>
        [ForeignKey("Schedule")]
        public Guid WorkScheduleConfigId { get; set; }
        public WorkScheduleConfig WorkScheduleConfig { get; set; }

        // Relationships
        public ICollection<Appointment> Appointments { get; set; }

    }
}
