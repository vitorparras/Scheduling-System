using Domain.Enum;
using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Appointment : BaseEntity
    {
        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public AppointmentStatus Status { get; set; }


        public Guid StoreId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ClientId { get; set; }


        [ForeignKey(nameof(ClientId))]
        public virtual User Client { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(StoreId))]
        public virtual Store Store { get; set; }


        [InverseProperty(nameof(AppointmentHistory.Appointment))]
        public virtual ICollection<AppointmentHistory> History { get; set; } = new List<AppointmentHistory>();

        public string DescriptionAppointment()
        {
            return $"{Date.ToShortDateString()} from {StartTime} to {EndTime} - Status: {Status}";
        }
    }
}
