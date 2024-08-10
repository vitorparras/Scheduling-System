using Domain.Model;

namespace Infrastructure.Repository.Interface
{
    public interface ITokenHistoryRepository
    {
        Task AddTokenHistoryAsync(TokenHistory tokenHistory);
        Task<TokenHistory> GetTokenHistoryAsync(string token);
        Task InvalidateTokenAsync(TokenHistory tokenHistory);
        Task<bool> IsTokenValidAsync(string token);
    }
}
