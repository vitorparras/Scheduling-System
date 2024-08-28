using Domain.Enum;
using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class AppointmentHistory : BaseEntity
    {
        public DateTime Date { get; set; }

        public Guid AppointmentId { get; set; }

        public AppointmentStatus Status { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        public virtual Appointment Appointment { get; set; }
    }
}
