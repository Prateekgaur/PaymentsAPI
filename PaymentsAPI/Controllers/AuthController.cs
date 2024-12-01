using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentsAPI.DTOs;
using PaymentsAPI.Models;
using PaymentsAPI.Services;
using PaymentsAPI.Utilities;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    /// <summary>
    /// Register user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequestDto request, bool isAdmin = false)
    {
        if (request is null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("username and password must be string");
        }
        var balance = 500;
        int userId = await _userService.RegisterUser(request, isAdmin, balance);
        if (userId > 0) 
        {
            return Ok(new { Message = "User registered successfully." });
        }
        else if (userId == -1)
        {
            return BadRequest("user already exists");
        }
        else
        {
            return StatusCode(500, new { Error = "Internal server error" });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        if (request is null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("username and password must be string");
        }
        var user = await _userService.Login(request.Username, request.Password);

        if (user == null)
            return Unauthorized("Invalid username or password.");

        var token = Utilities.GenerateJwtToken(user,_configuration);

        return Ok(new { Token = token });
       
    }
}
