using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Models;
using ConstructionManagementSystem.DTOs;
using ConstructionManagementSystem.Services;
using Construction_Management_System.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto.Username, dto.Password, dto.Email, dto.Role);
        return Ok(new { message = result });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto.Username, dto.Password);
        if (token == null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new { token });
    }

}
