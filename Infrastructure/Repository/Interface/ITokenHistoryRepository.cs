using Domain.Model;

namespace Infrastructure.Repository.Interface
{
    public interface ITokenHistoryRepository: IGenericRepository<TokenHistory>
    {
        Task<TokenHistory> GetTokenHistoryAsync(string token);
        Task InvalidateTokenAsync(TokenHistory tokenHistory);
        Task<bool> IsTokenValidAsync(string token);
    }
}
