using Domain.Enum;
using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// Name of the user.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Hash of the user's password.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Password { get; set; } 
        
        /// <summary>
        /// Hash of the user's password.
        /// </summary>
        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        /// <summary>
        /// Roles of the user in the system.
        /// Can be 'Client', 'Admin' or 'Employee'.
        /// </summary>
        public ICollection<UserRolesEnum> Roles { get; set; }
    }
}
