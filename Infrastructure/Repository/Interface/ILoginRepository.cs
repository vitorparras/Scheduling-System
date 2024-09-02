using Domain.Model;

namespace Infrastructure.Repository.Interface
{
    public interface ILoginRepository : IGenericRepository<LoginHistory>
    {
        Task InvalidateTokenAsync(LoginHistory loginHistory);
        Task<LoginHistory?> GetLoginHistoryByTokenAsync(string token);
    }
}
