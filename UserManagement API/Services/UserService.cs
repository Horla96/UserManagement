using UserManagement_API.DTOs.Request;
using UserManagement_API.DTOs.Response;
using UserManagement_API.Interfaces;
using UserManagement_API.Models;

namespace UserManagement_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
            };
        }

        public async Task DeleteUserAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            await _repository.DeleteAsync(user);
        }

        public async Task<UserResponseDto> GetUserByIdAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
        {
            var users = await _repository.GetAllAsync();

            return users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            });
        }

        public async Task UpdateUserAsync(long id, UpdateUserRequestDto request)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;

            await _repository.UpdateAsync(user);
        }
    }
}
