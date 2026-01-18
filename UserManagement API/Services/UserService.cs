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

        public async Task CreateUserAsync(User user)
        {
             await _repository.AddAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            await _repository.DeleteAsync(user);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            var existingUser = await _repository.GetByIdAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found");


            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            await _repository.UpdateAsync(existingUser);
        }
    }
}
