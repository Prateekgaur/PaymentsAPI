using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.Models;
using PaymentsAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AuthService _authService;

    public AuthController(IUserService userService, AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _userService.Authenticate(request.Username, request.Password);
        if (user == null)
            return Unauthorized("Invalid username or password.");

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }
}
