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
        Task<User> GetUserByEmailAndPasswordAndRole(string email, string password, string role);
        Task ChangePasswordAsync(User user, string oldPassword, string newPassword);
        Task<User> EditarUserAsync(User user);
        Task<PaginationResult<User>> GetListOfUsers(string query, int page);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByEmailAndRole(string email, string role);
        Task SaveChangesASync();
    }
}
