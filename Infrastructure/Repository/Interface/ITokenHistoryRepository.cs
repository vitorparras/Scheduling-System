using Domain.Model;

namespace Infrastructure.Repository.Interface
{
    public interface ITokenHistoryRepository: IGenericRepository<LoginHistory>
    {
        Task<LoginHistory> GetTokenHistoryAsync(string token);
        Task InvalidateTokenAsync(LoginHistory tokenHistory);
        Task<bool> IsTokenValidAsync(string token);
    }
}
