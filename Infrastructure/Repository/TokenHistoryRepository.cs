using Domain.Model;
using Infrastructure.Repository.Interface;

namespace Infrastructure.Repository
{
    public class TokenHistoryRepository : GenericRepository<TokenHistory>, ITokenHistoryRepository
    {
        public TokenHistoryRepository(DxContext dbContext) : base(dbContext)
        {
        }

        public async Task<TokenHistory?> GetTokenHistoryAsync(string token) =>
            await FirstOrDefaultAsync(t => t.Token.Equals(token));

        public Task InvalidateTokenAsync(TokenHistory tokenHistory)
        {
            tokenHistory.IsValid = false;
            return UpdateAsync(tokenHistory);
        }

        public async Task<bool> IsTokenValidAsync(string token)
        {
            var exist = await GetTokenHistoryAsync(token);
            return (exist != null && exist.IsValid);
        }
    }
}
