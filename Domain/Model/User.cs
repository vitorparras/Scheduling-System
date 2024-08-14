using Domain.Model.Bases;

namespace Domain.Model
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Cep { get; set; }
        public string CPF { get; set; }
    }
}
