using UserManagement_API.DTOs.Request;
using UserManagement_API.DTOs.Response;

namespace UserManagement_API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetUsersAsync();
        Task<UserResponseDto> GetUserByIdAsync(long id);
        Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto request);
        Task UpdateUserAsync(long id, UpdateUserRequestDto request);
        Task DeleteUserAsync(long id);
    }
}
