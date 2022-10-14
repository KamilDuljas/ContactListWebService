using ContactListWebService.DbContexts;
using ContactListWebService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactListWebService.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly UserDbContext _context;

        public UserInfoRepository(UserDbContext ctx)
        {
            _context = ctx;
        }

        public Task<User?> GetUserAsync(int userId)
        {
            return _context.Users.Include(c => c.UserCategory).FirstOrDefaultAsync(user => user.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.Include(c => c.UserCategory).ToListAsync();
        }

        public async Task<bool> HasUserExistAsync(string userEmail)
        {
            return await _context.Users.AnyAsync(user => user.Email.ToLower() == userEmail.ToLower());
        }

        public async Task<int> AddUserAsync(User user)
        {
            var addedUser = await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveUser(User user)
        {
            _context.Remove(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
