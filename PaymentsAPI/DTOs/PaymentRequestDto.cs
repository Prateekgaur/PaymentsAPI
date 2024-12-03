namespace PaymentsAPI.DTOs
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int RecipientId { get; set; }
    }
}
