﻿
using Microsoft.EntityFrameworkCore;
using PaymentsAPI.Data;
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

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.SingleOrDefaultAsync(p => p.Id == paymentId);
        }
        public async Task<(bool Success, string Message, int PaymentId)> InitiatePaymentAsync(Payment payment)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var payer = await _context.Users.FindAsync(payment.UserId);
                var recipient = await _context.Users.FindAsync(payment.RecipientId);
                if (payer is null || recipient is null)
                {
                    return (false, "Invalid payer or recipient.", 0);
                }
                if (payer.Balance < payment.Amount)
                {
                    return (false, "Insufficient funds.", 0);
                }

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, "Payment initiated successfully.", payment.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Payment initiation failed: {ex.Message}", 0);
            }
        }

        public async Task<(bool Success, string Message)> CompletePaymentAsync(int paymentId, bool retriedPayment = false)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var payment = await _context.Payments.FindAsync(paymentId);
                if (payment == null)
                    return (false, "Payment not found.");

                if (payment.Status == PaymentStatus.Succeed)
                    return (false, "Payment already succeeded.");
                if (retriedPayment && payment.Status == PaymentStatus.Pending && payment.Tries > 5)
                {
                    return (false, "Only failed payment can be retried");
                }
                var payer = await _context.Users.FindAsync(payment.UserId);
                var recipient = await _context.Users.FindAsync(payment.RecipientId);

                if (payer is null || recipient is null)
                {
                    return (false, "Invalid payer or recipient details in database.");
                }

                payer.Balance -= payment.Amount;
                recipient.Balance += payment.Amount;

                payment.Tries += 1;
                payment.Status = PaymentStatus.Succeed;
                payment.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, "Payment completed successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Payment processing failed: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> CancelPaymentAsync(int paymentId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var payment = await _context.Payments.FindAsync(paymentId);
                if (payment == null)
                    return (false, "Payment not found.");

                if (payment.Status == PaymentStatus.Succeed)
                    return (false, "Payment already succeeded.");
                if (payment.Status == PaymentStatus.Failed)
                    return (false, "Payment already failed.");

                var payer = await _context.Users.FindAsync(payment.UserId);
                var recipient = await _context.Users.FindAsync(payment.RecipientId);

                if (payer is null || recipient is null)
                {
                    return (false, "Invalid payer or recipient details in database.");
                }

                payment.Tries = 1;
                payment.Status = PaymentStatus.Failed;
                payment.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, "Payment cancelled successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Payment cancellation failed: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> RetryPaymentAsync(int paymentId)
        {
            return await CompletePaymentAsync(paymentId, true);
        }
    }
}
