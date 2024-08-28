﻿using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class TokenHistory : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public bool IsValid { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual LoginHistory LoginHistory { get; set; }

        public bool IsExpired()
        {
            return DateTime.UtcNow > ExpiryDate;
        }
    }
}
