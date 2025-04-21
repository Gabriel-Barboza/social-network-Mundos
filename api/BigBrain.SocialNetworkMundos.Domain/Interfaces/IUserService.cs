using BigBrain.SocialNetworkMundos.Domain.Entities;
using BigBrain.SocialNetworkMundos.Domain.Models.Requests;
using BigBrain.SocialNetworkMundos.Domain.Models.Response;

namespace BigBrain.SocialNetworkMundos.Domain.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponse> CreateUserAsync(CreateUserRequest request);
        public Task<UserResponse[]> GetAllUsersAsync();

        public Task<List<UserResponse>> GetUsersByNameOrUsernameAsync(GetUsersRequest request);
        public Task<UserResponse> GetUsersByIdAsync(Guid id);

        public Task<UserResponse?> UpdateUserAsync(Guid id, UpdateUserRequest request);

        public Task<bool>DeleteUserAsync(Guid id);

        public Task<string?> LoginAsync(string email, string password);
        Task<bool> UpdateProfilePictureAsync(Guid userId, string imageUrl);
    }
}
