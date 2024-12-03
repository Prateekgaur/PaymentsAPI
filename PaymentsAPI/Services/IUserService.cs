using PaymentsAPI.DTOs;
using PaymentsAPI.Models;

namespace PaymentsAPI.Services
{
    public interface IUserService
    {
        Task<User> Login(string username, string password);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserById(int userId);
        Task<int> RegisterUser(LoginRequestDto request, bool isAdmin, int balance);
    }
}
