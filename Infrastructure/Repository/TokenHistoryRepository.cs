using Domain.Model;
using Infrastructure.Repository.Interface;

namespace Infrastructure.Repository
{
    public class TokenHistoryRepository : GenericRepository<User>, ITokenHistoryRepository
    {
        public TokenHistoryRepository(DxContext dbContext) : base(dbContext)
        {
        }

        public Task AddTokenHistoryAsync(TokenHistory tokenHistory)
        {
            throw new NotImplementedException();
        }

        public Task<TokenHistory> GetTokenHistoryAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task InvalidateTokenAsync(TokenHistory tokenHistory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTokenValidAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
