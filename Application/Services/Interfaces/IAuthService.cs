using Domain.DTO;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<GenericResponse<string>> LoginAsync(string email, string password);
        Task<GenericResponse<string>> LogoutAsync(string token);
    }
}
