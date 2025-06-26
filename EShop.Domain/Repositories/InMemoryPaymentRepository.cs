using EShop.Domain.Models;
using EShop.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Domain.Repositories
{
    public class InMemoryPaymentRepository : IPaymentRepository
    {
        private readonly List<Payment> _paymentList = new();

        public Payment? GetPaymentByOrderId(int orderId)
        {
            return _paymentList.FirstOrDefault(p => p.OrderId == orderId);
        }

        public void UpdatePaymentStatus(int orderId, PaymentStatus newStatus)
        {
            var payment = _paymentList.FirstOrDefault(p => p.OrderId == orderId);
            if (payment != null)
            {
                payment.Status = newStatus;
            }
        }

        public Payment AddPayment(Payment payment)
        {
            payment.Id = _paymentList.Count + 1;
            _paymentList.Add(payment);
            return payment;
        }
    }
} 