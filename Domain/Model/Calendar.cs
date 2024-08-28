using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Calendar: BaseEntity
    {
        public Guid StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public virtual Store Store { get; set; }
    }
}
