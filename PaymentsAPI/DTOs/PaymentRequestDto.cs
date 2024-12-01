using PaymentsAPI.Models;

namespace PaymentsAPI.DTOs
{
    public class PaymentRequestDto
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int RecipientId { get; set; }
    }
}
