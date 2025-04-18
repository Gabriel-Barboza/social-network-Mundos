using BigBrain.SocialNetworkMundos.Domain.Entities;
using BigBrain.SocialNetworkMundos.Domain.Models.Response;

namespace BigBrain.SocialNetworkMundos.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User user);
        public Task<User[]> GetAllUsersAsync();
        public Task<List<User>> GetUsersByNameOrUsernameAsync(string nameOrUsername);
        public Task<User?> GetUsersByIdAsync(Guid Id);
        public Task<User> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(Guid id);
    }
}
