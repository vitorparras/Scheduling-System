using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class LoginHistory : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        [StringLength(45)]
        public string IPAddress { get; set; }

        public Guid? TokenId { get; set; }

        public virtual User User { get; set; }

        public virtual TokenHistory TokenHistory { get; set; }


        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginHistory>()
                .HasOne(lh => lh.User)
                .WithMany(u => u.LoginHistories)
                .HasForeignKey(lh => lh.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<LoginHistory>()
                .HasOne(lh => lh.TokenHistory)
                .WithMany()
                .HasForeignKey(lh => lh.TokenId)
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
