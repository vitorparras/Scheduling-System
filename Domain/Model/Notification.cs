using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Notification : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(2000)]
        public string Message { get; set; }

        [Required]
        public bool IsRead { get; set; }

        public virtual User User { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
