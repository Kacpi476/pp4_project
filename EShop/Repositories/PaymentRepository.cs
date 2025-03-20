using EShop.Models;
namespace EShop.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly List<Payment> _paymentList = new();
        public List<Payment> GetPayments() => _paymentList;
        
        public Payment? GetPaymentByOrderId(int id) => _paymentList.FirstOrDefault(p =>p.OrderId == id );

        public Payment PayForOrder(int orderId, string newStatus)
        {
            var payment = GetPaymentByOrderId(orderId);
            if (payment == null)
            {
                throw new Exception("Order not found");
            }
            payment.paymentStatus = newStatus;
            return payment;
        }
        public Payment CreatePayment(Payment payment)
        {
            payment.PaymentId = Convert.ToInt32("9900"+_paymentList.Count+1);
            _paymentList.Add(payment);
            return payment;
        }
    }
}
