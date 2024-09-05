using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Models;

namespace VendasWebCore.Repositories
{
    public interface IUserRepository
    {
        Task CadastrarUserAsync(User user);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);

        Task EditarUserAsync(User user);
        Task<PaginationResult<User>> GetListOfUsers(string query, int page);
        
        Task SaveChangesASync();
    }
}
