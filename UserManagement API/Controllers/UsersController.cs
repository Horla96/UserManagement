using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagement_API.Models;
using UserManagement_API.Services;

namespace UserManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "endpoint to get all users ")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetUsersAsync());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "endpoint to get a user ")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost("SignUp")]
        [SwaggerOperation(Summary = "endpoint to create a new user ")]
        public async Task<IActionResult> Create(User user)
        {
            await _service.CreateUserAsync(user);
            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "endpoint to update an existing user")]
        public async Task<IActionResult> Update(int id, User user)
        {
            var existingUser = await _service.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(); 
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            await _service.UpdateUserAsync(id, existingUser);

            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "endpoint to delete an existing user")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _service.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _service.DeleteUserAsync(id);

            return Ok();
        }
    }
}
