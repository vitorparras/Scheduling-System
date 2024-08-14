namespace Domain.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UserAddDTO: UserDTO
    {
        public string Tel { get; set; }
        public string Cep { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
    }
}