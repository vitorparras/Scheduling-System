using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Calendar : BaseEntity
    {
        [Required]
        public Guid StoreId { get; set; }

        public virtual Store Store { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calendar>()
                .HasOne(c => c.Store)
                .WithOne(s => s.Calendar)
                .HasForeignKey<Calendar>(c => c.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
