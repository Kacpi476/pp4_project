using EShop.enums;
using EShop.Models;

namespace EShop.Services;

public interface IPaymentService
{
    public List<Payment> GetPayments();
    public Payment? GetPaymentByOrderId(int paymentId);
    public Payment CreatePayment(int orderId, decimal amount, PaymentMethod paymentMethod);
}