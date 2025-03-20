using EShop.enums;
using EShop.Models;
using EShop.Repositories;

namespace EShop.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;
    
    public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IClientRepository clientRepository)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
    }
    
    public List<Payment> GetPayments() => _paymentRepository.GetPayments();
    public Payment? GetPaymentByOrderId(int paymentId) => _paymentRepository.GetPaymentByOrderId(paymentId);

    public Payment CreatePayment(int orderId, decimal amount, PaymentMethod paymentMethod)
    {
        // Pobranie zamówienia
        var order = _orderRepository.GetOrderById(orderId);
        if (order == null)
        {
            throw new Exception("Zamówienie nie istnieje.");
        }
        
        // Pobrenie klienta
        var client = _clientRepository.GetClientById(order.ClientId);
        if (client == null)
        {
            throw new Exception("Klient nie istnieje.");
        }
        
        // Tworzenie płatności
        var payment = new Payment
        {
            OrderId = order.Id,
            ClientId = client.Id,
            Amount = order.TotalAmount,
            paymentMethod = paymentMethod.ToString(),
            paymentStatus = "Pending",
            PaymentDate = DateTime.Now
        };
        return _paymentRepository.CreatePayment(payment);
    }
}