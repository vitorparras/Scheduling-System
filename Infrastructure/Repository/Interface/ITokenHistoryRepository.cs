using Domain.DTO;
using Domain.Model;

namespace Infrastructure.Repository.Interface
{
    public interface ITokenHistoryRepository : IGenericRepository<LoginHistory>
    {
        Task<GenericResponse<bool>> IsTokenValidAsync(string token);
    }
}
