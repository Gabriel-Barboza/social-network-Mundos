using BigBrain.SocialNetworkMundos.Domain.Entities;

namespace BigBrain.SocialNetworkMundos.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User user);
    }
}
