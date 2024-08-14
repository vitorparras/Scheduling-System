using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Calendar: BaseEntity
    {
        /// <summary>
        /// Identifier of the store associated with the calendar.
        /// </summary>
        [ForeignKey("Store")]
        public Guid StoreID { get; set; }
        public Store Store { get; set; }
    }
}
