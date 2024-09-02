using Domain.DTO;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<GenericResponse<string>> LoginAsync(string email, string password, string ip);
        Task<GenericResponse<string>> LogoutAsync(string token);
    }
}
