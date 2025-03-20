using EShop.Models;
namespace EShop.Repositories
{
    public interface IPaymentRepository
    {
        List<Payment> GetPayments();
        
        Payment? GetPaymentByOrderId(int id);
        
        public Payment CreatePayment(Payment payment);
    }
}
