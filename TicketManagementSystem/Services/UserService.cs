using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Services
{
    public class UserService(AppDbContext dbContext) : IUserService
    {
        public async Task AddUser(User user)
        {
            var users = new User()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive
            };

            dbContext.Users.Add(users);
            await dbContext.SaveChangesAsync();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
