
using BigBrain.SocialNetworkMundos.Domain.Entities;
using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using BigBrain.SocialNetworkMundos.Infra.Data;
using Microsoft.EntityFrameworkCore;
namespace BigBrain.SocialNetworkMundos.Infra.Repository

{

    public class UserRepository : IUserRepository

    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User[]> GetAllUsersAsync()
        {
            var users = await _context.Users.ToArrayAsync();

            return users;
        }
        public async Task<List<User>> GetUsersByNameOrUsernameAsync(string nameOrUsername)
        {
            var users = await _context.Users
            .Where(u =>
             u.Name.ToLower().Contains(nameOrUsername.ToLower()) ||
              u.Username.ToLower().Contains(nameOrUsername.ToLower()))
             .ToListAsync();
            return users;


        }

        public Task<User?> GetUsersByIdAsync(Guid Id)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }

        public Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            _context.SaveChangesAsync();
            return Task.FromResult(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
