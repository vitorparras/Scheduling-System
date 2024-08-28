using Domain.Model;
using Infrastructure.Repository.Interface;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SchedulerContext dbContext) : base(dbContext)
        {
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
