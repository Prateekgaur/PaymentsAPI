using PaymentsAPI.Models;

namespace PaymentsAPI.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
    }
}
