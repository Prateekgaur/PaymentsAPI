using PaymentsAPI.Models;

namespace PaymentsAPI.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User GetUserById(int userId);
        User GetUserByUsername(string username);
    }
}
