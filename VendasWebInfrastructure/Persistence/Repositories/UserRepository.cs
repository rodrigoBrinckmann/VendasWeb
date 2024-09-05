using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VendasWebDbContext _dbContext;
        private const int PAGE_SIZE = 5;

        public UserRepository(VendasWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        public async Task CadastrarUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await SaveChangesASync();
        }

        public Task EditarUserAsync(User user)
        {
            throw new NotImplementedException();
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

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesASync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _dbContext.Users.
                SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
        }
    }
}
