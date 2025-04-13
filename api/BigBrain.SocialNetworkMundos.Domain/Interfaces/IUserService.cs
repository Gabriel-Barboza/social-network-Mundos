using BigBrain.SocialNetworkMundos.Domain.Entities;
using BigBrain.SocialNetworkMundos.Domain.Models.Requests;

namespace BigBrain.SocialNetworkMundos.Domain.Interfaces
{
    public interface IUserService
    {
        public Task<User> CreateUserAsync(CreateUserRequest request);
    }
}
