namespace PaymentsAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Store hashed passwords, not plain text
        public string Role { get; set; } // For role-based authorization (e.g., Admin, User)
        public decimal Balance { get; set; } // To simulate available balance for payment
    }
}
