using Domain.DTO;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<GenericResponse<UserDTO>> GetByEmailAsync(string email);
        Task<GenericResponse<bool>> VerifyPasswordAsync(UserDTO user, string password);
        Task<GenericResponse<IEnumerable<UserDTO>>> GetAllAsync();
        Task<GenericResponse<UserDTO>> GetByIdAsync(Guid id);
        Task<GenericResponse<UserDTO>> AddAsync(UserAddDTO user);
        Task<GenericResponse<UserDTO>> UpdateAsync(UserDTO user);
        Task<GenericResponse<string>> DeleteAsync(Guid id);
    }
}
