using System.Collections.Generic;
using PaymentsAPI.Models;
using PaymentsAPI.Models;

namespace PaymentsAPI.Repositories
{
    public interface IPaymentRepository
    {
        Payment GetPaymentById(int paymentId);
        IEnumerable<Payment> GetPaymentsByUserId(int userId);
        IEnumerable<Payment> GetAllPayments();
        void AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int paymentId);
        void SaveChanges();
    }
}
