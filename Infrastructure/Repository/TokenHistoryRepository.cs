using Domain.DTO;
using Domain.Model;
using Infrastructure.Repository.Interface;

namespace Infrastructure.Repository
{
    public class TokenHistoryRepository : GenericRepository<LoginHistory>, ITokenHistoryRepository
    {
        public TokenHistoryRepository(SchedulerContext dbContext) : base(dbContext)
        {
        }

        public async Task<LoginHistory?> GetTokenHistoryAsync(string token) =>
            await FirstOrDefaultAsync(t => t.Token.Equals(token));

        public Task InvalidateTokenAsync(LoginHistory tokenHistory)
        {
            tokenHistory.IsValid = false;
            return UpdateAsync(tokenHistory);
        }

        public async Task<GenericResponse<bool>> IsTokenValidAsync(string token)
        {
            var exist = await GetTokenHistoryAsync(token);
            return (exist is null) ?
                new GenericResponse<bool>("Token Not Found", false) :
                new GenericResponse<bool>(exist.IsValid);
        }
    }
}
