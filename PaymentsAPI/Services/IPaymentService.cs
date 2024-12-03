using PaymentsAPI.Models;

namespace PaymentsAPI.Services
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<(bool Success, string Message, int PaymentId)> InitiatePaymentAsync(Payment payment);
        Task<(bool Success, string Message)> CompletePaymentAsync(int paymentId, bool retriedPayment = false);
        Task<(bool Success, string Message)> CancelPaymentAsync(int paymentId);
        Task<(bool Success, string Message)> RetryPaymentAsync(int paymentId);

    }
}
