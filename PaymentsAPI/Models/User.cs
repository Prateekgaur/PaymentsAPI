using System.ComponentModel.DataAnnotations;

namespace PaymentsAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Payment> Payments { get; set; }
    }
}
