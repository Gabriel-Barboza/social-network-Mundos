using BigBrain.SocialNetworkMundos.Domain.Entities;
using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using BigBrain.SocialNetworkMundos.Domain.Models.Requests;

namespace BigBrain.SocialNetworkMundos.Application.Services
{
    public class UserService : IUserService
    {
        public Task<User> CreateUserAsync(CreateUserRequest request)
        {
            var user = new User
            {

                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,

            };
            return Task.FromResult(user);
        }
    }
}
