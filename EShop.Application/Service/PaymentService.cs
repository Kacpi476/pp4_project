using EShop.Domain.Enums;
using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Application.Service;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public Payment? GetPaymentByOrderId(int orderId) => _paymentRepository.GetPaymentByOrderId(orderId);
    public void UpdatePaymentStatus(int orderId, PaymentStatus newStatus) => _paymentRepository.UpdatePaymentStatus(orderId, newStatus);
    public Payment AddPayment(Payment payment) => _paymentRepository.AddPayment(payment);
}