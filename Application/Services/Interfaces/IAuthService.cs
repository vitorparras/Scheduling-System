using Domain.DTO;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<GenericResponse<string>> LoginAsync(LoginDTO login, string ip);

        Task<GenericResponse<string>> LogoutAsync(string token);
    }
}
