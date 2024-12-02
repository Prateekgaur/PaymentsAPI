﻿using PaymentsAPI.DTOs;
using PaymentsAPI.Models;

namespace PaymentsAPI.Services
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId);
        Task<IEnumerable<Payment>> GetPaymentsByRecipientIdAsync(int recipientId);
        Task<bool> UpdatePaymentStatusAsync(int paymentId, PaymentStatus status);
        Task<bool> CancelPaymentAsync(int paymentId);
        Task<(bool Success, string Message, int PaymentId)> InitiatePaymentAsync(PaymentRequestDto request);

    }
}
