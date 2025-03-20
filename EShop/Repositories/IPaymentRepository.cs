using EShop.Models;
namespace EShop.Repositories
{
    public interface IPaymentRepository
    {
        List<Payment> GetPayments();
        
        Payment? GetPaymentByOrderId(int id);
        
        Payment PayForOrder(int orderId, string newStatus);
        public Payment CreatePayment(Payment payment);
    }
}
