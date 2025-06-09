using Microsoft.AspNetCore.Mvc;
using Paltrack.Application.Contracts;
using Paltrack.Application.Dtos;

namespace Paltrack.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IAuthService _authService;

    public UsersController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto registerUserDto)
    {
        if (registerUserDto == null)
        {
            return BadRequest("Invalid user data.");
        }

        var response = await _authService.RegisterUserAsync(registerUserDto);
        if (response.IsSuccess)
        {
            return Ok(response.Data);
        }

        return BadRequest(response.Message);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginDto loginDto)
    {
        if (loginDto == null)
        {
            return BadRequest("Invalid login data.");
        }

        var response = await _authService.LoginUserAsync(loginDto);
        if (response.IsSuccess)
        {
            return Ok(response.Data);
        }

        return Unauthorized(response.Message);
    }
}

