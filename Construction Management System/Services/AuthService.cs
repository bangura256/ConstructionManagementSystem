using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConstructionManagementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            // Validate password (replace with secure hash verification)
            if (!VerifyPassword(password, user.PasswordHash))
                return null;

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> RegisterAsync(string username, string password, string email, string role)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username || u.Email == email))
                return false;

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Email = email,
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Dummy hash, replace with real secure hashing (e.g., BCrypt)
        private string HashPassword(string password) => Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

        private bool VerifyPassword(string password, string hash) =>
            HashPassword(password) == hash;
    }
}
