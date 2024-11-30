
namespace PaymentsAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public decimal Amount { get; set; }
        public string Currency { get; set; } // E.g., "USD", "INR"
        public string PaymentMethod { get; set; } 
        public string Recipient { get; set; } // E.g., recipient account or email
        public string Status { get; set; } = "Pending"; // Default status
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for payment creation
        public DateTime? UpdatedAt { get; set; } // Timestamp for the last status update
    }
}
