using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class TokenHistory : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public Guid LoginHistoryId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public bool IsValid { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public virtual User User { get; set; }
        public virtual LoginHistory LoginHistory { get; set; }

        public bool IsExpired()
        {
            return DateTime.UtcNow > ExpiryDate;
        }

        
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenHistory>()
                .HasOne(th => th.User)
                .WithMany(u => u.TokenHistories)
                .HasForeignKey(th => th.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<TokenHistory>()
                .HasOne(th => th.LoginHistory)
                .WithMany()
                .HasForeignKey(th => th.LoginHistoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TokenHistory>()
                .HasIndex(th => th.Token);
        }
    }
}
