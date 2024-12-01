using Microsoft.IdentityModel.Tokens;
using PaymentsAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PaymentsAPI.Utilities
{
    public static class Utilities
    {
        public static string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            string hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string storedPassword)
        {
            return HashPassword(password) == storedPassword;
        }
        public static string GenerateJwtToken(User user, IConfiguration _configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("Role", user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

