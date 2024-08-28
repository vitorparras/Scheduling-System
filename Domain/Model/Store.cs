using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Store : BaseEntity
    {
        public Guid AdminId { get; set; }
        public Guid CalendarId { get; set; }

        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string GoogleAddress { get; set; }

        [Required]
        [StringLength(255)]
        public string Link { get; set; }

        

        [ForeignKey(nameof(AdminId))]
        public virtual User Admin { get; set; }

        [ForeignKey(nameof(CalendarId))]
        public virtual Calendar Calendar { get; set; }


        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

