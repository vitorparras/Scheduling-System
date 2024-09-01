using Domain.DTO;

namespace Application.Services.Interfaces
{
    public interface IJwtService
    {
        Task<GenericResponse<bool>> TokenIsValid(string token);
        GenericResponse<string> GenerateJwtToken(UserDTO user);
    }
}
