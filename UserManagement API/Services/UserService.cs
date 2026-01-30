using AutoMapper;
using UserManagement_API.DTOs.Request;
using UserManagement_API.DTOs.Response;
using UserManagement_API.Interfaces;
using UserManagement_API.Models;
//using Serilog;
using Microsoft.Extensions.Logging;

namespace UserManagement_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;


        public UserService(IUserRepository repository, IMapper mapper, ILogger<UserService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto request)
        {
            _logger.LogInformation("Creating user with email {Email}", request.Email);

            var user = _mapper.Map<User>(request);

            user.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(user);

            _logger.LogInformation("User created successfully with Id {UserId}", user.Id);

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task DeleteUserAsync(long id)
        {
            _logger.LogInformation("Deleting user with Id {UserId}", id);

            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            await _repository.DeleteAsync(user);

            _logger.LogInformation("User with Id {UserId} deleted successfully", id);
        }

        public async Task<UserResponseDto> GetUserByIdAsync(long id)
        {
            _logger.LogInformation("Fetching user with Id {UserId}", id);

            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User with Id {UserId} not found", id);

                return null;
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
        {
            _logger.LogInformation("Fetching all users");

            var users = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task UpdateUserAsync(long id, UpdateUserRequestDto request)
        {
            _logger.LogInformation("Updating user with Id {UserId}", id);

            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _mapper.Map(request, user);

            await _repository.UpdateAsync(user);

            _logger.LogInformation("User with Id {UserId} updated successfully", id);
        }
    }
}
