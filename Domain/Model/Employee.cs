using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Employee : BaseEntity
    {
        public Guid ScheduleId { get; set; }
        public Guid WorkScheduleConfigId { get; set; }
        public Guid StoreId { get; set; }
        public Guid UserId { get; set; }


        [ForeignKey(nameof(ScheduleId))]
        public virtual Appointment Schedule { get; set; }

        [ForeignKey(nameof(StoreId))]
        public virtual Store Store { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(WorkScheduleConfigId))]
        public virtual WorkScheduleConfig WorkScheduleConfig { get; set; }

        // Collection of appointments related to the employee
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
