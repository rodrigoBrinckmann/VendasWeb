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
        Task ChangePasswordAsync(string email, string oldPassword, string newPassword);
        Task<User> EditarUserAsync(User user);
        Task<PaginationResult<User>> GetListOfUsers(string query, int page);
        Task<List<User>> GetUserByEmail(string email);
        Task SaveChangesASync();
    }
}
