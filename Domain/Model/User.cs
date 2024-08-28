using Domain.Enum;
using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class User : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        [Required, MaxLength(14)]
        public string CPF { get; set; }

        [Required]
        public UserRolesEnum Roles { get; set; } = UserRolesEnum.None;

        

        public virtual ICollection<TokenHistory> TokenHistories { get; set; } = new List<TokenHistory>();

        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public virtual ICollection<LoginHistory> LoginHistories { get; set; } = new List<LoginHistory>();


        public bool HasRole(UserRolesEnum role)
        {
            return (Roles & role) == role;
        }
    }
}
