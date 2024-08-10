using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
