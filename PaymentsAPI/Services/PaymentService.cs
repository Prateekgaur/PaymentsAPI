
using Microsoft.EntityFrameworkCore;
using PaymentsAPI.Data;
using PaymentsAPI.DTOs;
using PaymentsAPI.Models;

namespace PaymentsAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));

            // Ensure payer and recipient exist
            var payer = await _context.Users.FindAsync(payment.UserId);
            var recipient = await _context.Users.FindAsync(payment.RecipientId);

            if (payer == null || recipient == null)
                throw new InvalidOperationException("Payer or recipient does not exist.");

            // Ensure payer has enough balance
            if (payer.Balance < payment.Amount)
                throw new InvalidOperationException("Insufficient balance for payment.");

            // Deduct balance from payer and create the payment
            payer.Balance -= payment.Amount;
            payment.Status = PaymentStatus.Pending;

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        /// <inheritdoc/>
        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == paymentId);

            if (payment == null)
                return null;

            // Fetch payer details using UserId
            var payer = await _context.Users.FirstOrDefaultAsync(u => u.Id == payment.UserId);
            var recipient = await _context.Users.FirstOrDefaultAsync(u => u.Id == payment.RecipientId);

            // You can map this data into a DTO or extend the Payment model if needed.
            return new Payment
            {
                Id = payment.Id,
                UserId = payment.UserId,
                RecipientId = payment.RecipientId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                CreatedAt = payment.CreatedAt,
                UpdatedAt = payment.UpdatedAt,
                // Optionally, map additional details about the payer and recipient if needed
            };
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId)
        {
            return await _context.Payments
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Payment>> GetPaymentsByRecipientIdAsync(int recipientId)
        {
            return await _context.Payments
                .Where(p => p.RecipientId == recipientId)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePaymentStatusAsync(int paymentId, PaymentStatus status)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                return false;
            }

            payment.Status = status;
            payment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> CancelPaymentAsync(int paymentId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
            if (payment == null || payment.Status != PaymentStatus.Pending)
            {
                // Only payments with "Pending" status can be canceled
                return false;
            }

            // Refund the amount to the payer
            var payer = await _context.Users.FindAsync(payment.UserId);
            if (payer != null)
            {
                payer.Balance += payment.Amount;
            }

            payment.Status = PaymentStatus.Failed;
            payment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public Task<(bool Success, string Message, int PaymentId)> InitiatePaymentAsync(PaymentRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
