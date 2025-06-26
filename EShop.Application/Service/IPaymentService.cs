using EShop.Domain.Enums;
using EShop.Domain.Models;

namespace EShop.Application.Service;

public interface IPaymentService
{
    Payment? GetPaymentByOrderId(int orderId);
    void UpdatePaymentStatus(int orderId, PaymentStatus newStatus);
    Payment AddPayment(Payment payment);
}