using Domain.Model.Bases;

namespace Domain.Model
{
    public class TokenHistory : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public bool IsValid { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
