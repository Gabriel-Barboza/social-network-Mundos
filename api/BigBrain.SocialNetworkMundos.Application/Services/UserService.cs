using BigBrain.SocialNetworkMundos.Domain.Entities;
using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using BigBrain.SocialNetworkMundos.Domain.Models.Requests;
using BigBrain.SocialNetworkMundos.Domain.Models.Response;

namespace BigBrain.SocialNetworkMundos.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            var user = new User
            {

                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                Bio = request.Bio,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,

            };

            user = await _userRepository.CreateUserAsync(user);
            return user;
        }


        public async Task<UserResponse[]> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var response = users.Select(user => new UserResponse
            {
                Id = user.Id.ToString(), // Convert Guid to string
                Name = user.Name,
                Email = user.Email,
                Bio = user.Bio,
                Username = user.Username,
            }).ToArray();

            return response;
        }


        public async Task<List<UserResponse>> GetUsersByNameOrUsernameAsync(GetUsersRequest request)


        {
            var nameOrUsername = request.SearchTerm;
            var users = await _userRepository.GetUsersByNameOrUsernameAsync(nameOrUsername);

            var userResponses = users.Select(user => new UserResponse
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Bio = user.Bio,
                Username = user.Username,
            }).ToList();

            return userResponses;
        }

        public async Task<UserResponse> GetUsersByIdAsync(Guid Id)
        {
            var user = await _userRepository.GetUsersByIdAsync(Id);

            if (user == null)
            {
                return null; // Return an empty list if no user is found
            }

            var userResponses = new UserResponse
            {


                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Bio = user.Bio,
                Username = user.Username,

            };

            return userResponses;
        }

        public async Task<UserResponse?> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetUsersByIdAsync(id);
            if (user == null)
            {
                return null; // Return null if no user is found
            }

            if (request.Name is not null)
            {
                user.Name = request.Name;
            }

            if ( request.Username is not null)
            {
                user.Username = request.Username;
            }
            
            if (request.Bio is not null)
            {
                user.Bio = request.Bio;
            } 
            
            await _userRepository.UpdateUserAsync(user);
            var userResponse = new UserResponse
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Bio = user.Bio,
                Username = user.Username,
            };
            return userResponse;


        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUsersByIdAsync(id);
            if (user == null)
            {
                return false; 
            }

            await _userRepository.DeleteUserAsync(id); 
            return true; 
        }
    }
}

