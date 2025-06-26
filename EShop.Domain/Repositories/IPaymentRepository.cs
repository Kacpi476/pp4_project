using EShop.Domain.Models;
using EShop.Domain.Enums;

namespace EShop.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Payment? GetPaymentByOrderId(int orderId);
        
        void UpdatePaymentStatus(int orderId, PaymentStatus newStatus);
        Payment AddPayment(Payment payment);
    }
}
