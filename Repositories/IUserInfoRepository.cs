using ContactListWebService.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactListWebService.Repositories
{
    public interface IUserInfoRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User?> GetUserAsync(int userId);

        Task<bool> HasUserExistAsync(string userEmail);

        Task<int> AddUserAsync(User user);

        Task<int> RemoveUser(User user);

        Task<bool> SaveChangesAsync();
    }
}
