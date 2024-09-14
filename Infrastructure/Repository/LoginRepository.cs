using Domain.Model;
using Infrastructure.Repository.Interface;

namespace Infrastructure.Repository
{
    public class LoginRepository : GenericRepository<LoginHistory>, ILoginRepository
    {
        public LoginRepository(SchedulerContext dbContext) : base(dbContext)
        {
        }

        public async Task<LoginHistory?> GetLoginHistoryByTokenAsync(string token)
        {
            return await FirstOrDefaultAsync(t => t.Token.Equals(token));
        }

        public async Task InvalidateTokenAsync(LoginHistory loginHistory)
        {
            loginHistory.IsValid = false;
            await UpdateAsync(loginHistory);
       }
    }
}
