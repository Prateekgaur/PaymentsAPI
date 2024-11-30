using PaymentsAPI.Data;
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

        /// <summary>
        /// Authenticate a user based on username and password.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's plain-text password.</param>
        /// <returns>The authenticated user or null if authentication fails.</returns>
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            // Retrieve the user by username
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return null;

            string hashedPassword = Utilities.Utilities.HashPassword(password);

            // Verify password (you should use hashed password verification in production)
            if (user.PasswordHash != hashedPassword) // Replace with proper password hashing
                return null;

            return user;
        }

        /// <summary>
        /// Retrieve a user by their unique ID.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <returns>The user object or null if not found.</returns>
        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        /// <summary>
        /// Retrieve a user by their username.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <returns>The user object or null if not found.</returns>
        public User GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username);
        }
    }
}
