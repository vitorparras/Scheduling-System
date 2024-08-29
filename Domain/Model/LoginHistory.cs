using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Domain.Model
{
    public class LoginHistory : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public string Token { get; set; }

        [StringLength(45)]
        public string IPAddress { get; set; }

        [Required]
        public bool IsValid { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public virtual User User { get; set; }
        
        public bool IsExpired()
        {
            return DateTime.UtcNow > ExpiryDate;
        }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginHistory>()
                .HasOne(th => th.User)
                .WithMany(u => u.LoginHistories)
                .HasForeignKey(th => th.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<LoginHistory>()
                .HasIndex(th => th.Token);
        }
    }
}
