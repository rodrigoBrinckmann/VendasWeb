using Microsoft.EntityFrameworkCore;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VendasWebDbContext _dbContext;
        private const int PAGE_SIZE = 5;

        public UserRepository(VendasWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CadastrarUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await SaveChangesASync();
        }
        
        public async Task<User> EditarUserAsync(User user)
        {
            try
            {
                var userDB = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
                if (userDB is not null)
                {
                    userDB.UpdateUsuario(user);
                    await SaveChangesASync();
                    return userDB;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("Usuário não existente no banco de dados - Não atualizado", e);
            }

        }

        public async Task<PaginationResult<User>> GetListOfUsers(string query, int page)
        {
            IQueryable<User> users = _dbContext.Users;

            if (!string.IsNullOrWhiteSpace(query))
            {
                users = users
                    .Where(p =>
                    p.Email.Contains(query)
                    || p.FullName.Contains(query));
            }

            return await users.GetPaged<User>(page, PAGE_SIZE);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Active == true);
            //IQueryable<User> users = _dbContext.Users;
            //return users.Where(e => e.Email.Contains(email)).ToList();                
        }

        public async Task<User> GetUserByEmailAndRole(string email, string role)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Role == role && u.Active == true);
            //IQueryable<User> users = _dbContext.Users;
            //return users.Where(e => e.Email.Contains(email) &&
            //          e.Role == role && e.Active == true).ToList();
        }

        public async Task<User> GetUserByEmailAndPasswordAndRole(string email, string password, string role)
        {
            //IQueryable<User> users = _dbContext.Users;
            return await _dbContext.Users.SingleOrDefaultAsync(e => e.Email.Contains(email) &&
                      e.Password == password && e.Role == role && e.Active == true);
        }

        public async Task ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            try
            {
                //var userDB = await _dbContext.Users.SingleOrDefaultAsync(u => u.UserId == userId);
                if (user is not null)
                {
                    user.UpdatePassword(newPassword);
                    await SaveChangesASync();                    
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("Usuário inválido - Não atualizado", e);
            }
        }
        
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _dbContext.Users.
                SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
        }

        public async Task SaveChangesASync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
