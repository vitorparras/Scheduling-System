using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Store : BaseEntity
    {
        /// <summary>
        /// Identifier of the store's admin.
        /// </summary>
        [ForeignKey("Admin")]
        public Guid AdminID { get; set; }
        public User Admin { get; set; }

        /// <summary>
        /// Name of the store.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Address of the store.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        
        /// <summary>
        /// Link Google Address of the store.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string GoogleAddress { get; set; }

        /// <summary>
        /// Unique link for the store.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Link { get; set; }

        /// <summary>
        /// Identifier of the store's calendar.
        /// </summary>
        [ForeignKey("Calendar")]
        public Guid CalendarID { get; set; }
        public Calendar Calendar { get; set; }

        // Relationships
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}

