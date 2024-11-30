
using System.Collections.Generic;
using System.Linq;
using PaymentsAPI.Data;
using PaymentsAPI.Models;
using PaymentsAPI.Data;
using PaymentsAPI.Models;

namespace PaymentsAPI.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Payment GetPaymentById(int paymentId)
        {
            return _context.Payments.Find(paymentId);
        }

        public IEnumerable<Payment> GetPaymentsByUserId(int userId)
        {
            return _context.Payments.Where(p => p.UserId == userId).ToList();
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
        }

        public void DeletePayment(int paymentId)
        {
            var payment = GetPaymentById(paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
