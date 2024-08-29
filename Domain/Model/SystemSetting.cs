using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class SystemSetting : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemSetting>()
                .HasIndex(ss => ss.Key)
                .IsUnique();
        }
    }
}
