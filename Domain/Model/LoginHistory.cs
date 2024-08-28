using Domain.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class LoginHistory : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        public string IPAddress { get; set; }

        public Guid? TokenId { get; set; } // Nullable in case the login wasn't token-based

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(TokenId))]
        public virtual TokenHistory TokenHistory { get; set; } // Navigation property for token

        // Optional: Method to provide a string representation of the login history
        public override string ToString()
        {
            return $"Login ID: {Id}, User ID: {UserId}, Login Time: {LoginTime}, IP Address: {IPAddress}, Token ID: {TokenId}";
        }
    }
}
