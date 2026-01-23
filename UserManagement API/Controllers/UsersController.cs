using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagement_API.DTOs.Request;
using UserManagement_API.DTOs.Response;
using UserManagement_API.Services;

namespace UserManagement_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "endpoint to get all users ")]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id:long}")]
    [SwaggerOperation(Summary = "endpoint to get a user ")]
    public async Task<ActionResult<UserResponseDto>> GetById(long id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }
       
        return Ok(user);
    }

    [HttpPost("SignUp")]
    [SwaggerOperation(Summary = "endpoint to create a new user ")]
    public async Task<ActionResult<UserResponseDto>> Create(
        [FromBody] CreateUserRequestDto request)
    {
        var createdUser = await _userService.CreateUserAsync(request);

        return CreatedAtAction(
          nameof(GetById),
          new { id = createdUser.Id },
          createdUser
      );
    }

    [HttpPut("{id:long}")]
    [SwaggerOperation(Summary = "endpoint to update an existing user")]

    public async Task<IActionResult> Update(long id, [FromBody] UpdateUserRequestDto request)
    {
        await _userService.UpdateUserAsync(id, request);

        return Ok(new
        {
            message = "User updated successfully"
        });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "endpoint to delete an existing user")]
    public async Task<IActionResult> Delete(long id)
    {
        await _userService.DeleteUserAsync(id);

        return Ok(new
        {
            message = "User deleted successfully"
        });
    }

}
