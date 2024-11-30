using System.Collections.Generic;
using PaymentsAPI.Models;
using PaymentsAPI.Models;

namespace PaymentsAPI.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        User GetUserByUsername(string username);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        void SaveChanges();
    }
}

