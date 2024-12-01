using Microsoft.EntityFrameworkCore;
using PaymentsAPI.Data;
using PaymentsAPI.DTOs;
using PaymentsAPI.Models;

namespace PaymentsAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> RegisterUser(LoginRequestDto request, bool isAdmin, int balance)
        {
            if(request is null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return 0;
            }
            var isExists = await GetUserByUsername(request.Username) != null ? true : false;
            if (isExists)
            {
                return -1;
            }
            User user = new User();
            user.Username = request.Username;
            user.PasswordHash = Utilities.Utilities.HashPassword(request.Password);
            user.Role = isAdmin ? "Admin" : "User";
            user.Balance = balance;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
        
        public async Task<User> Login(string username, string password)
        {
            // Retrieve the user by username
            var user = await GetUserByUsername(username);
            if (user == null)
                return null;
            if (!Utilities.Utilities.VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
