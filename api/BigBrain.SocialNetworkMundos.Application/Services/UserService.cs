using BigBrain.SocialNetworkMundos.Domain.Entities;
using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using BigBrain.SocialNetworkMundos.Domain.Models.Requests;

namespace BigBrain.SocialNetworkMundos.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }
        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            var user = new User
            {

                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,

            };
           
            user = await _userRepository.CreateUserAsync(user);
            return user;
        }
    }
}
