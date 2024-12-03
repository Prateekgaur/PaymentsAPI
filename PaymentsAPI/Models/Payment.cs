
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsAPI.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to User
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int RecipientId { get; set; } // recipient's User Id
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending; // Default status
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Timestamp for payment creation
        public DateTime? UpdatedAt { get; set; } // Timestamp for the last status update
    }
    public enum PaymentStatus
    {
        Pending,
        Succeed,
        Failed
    }
}
