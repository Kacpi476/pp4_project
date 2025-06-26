using EShop.Domain.Enums;
using EShop.Domain.Models;
using EShop.Domain.Repositories;
using System.Threading.Tasks;

namespace EShop.Application.Service;

public class OrderService : IOrderService
{
    private readonly IClientRepository _clientRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentService _paymentService;
    private readonly IInvoiceService _invoiceService;

    public OrderService(
        IClientRepository clientRepository, 
        ICartRepository cartRepository, 
        IProductRepository productRepository, 
        IOrderRepository orderRepository, 
        IPaymentService paymentService,
        IInvoiceService invoiceService)
    {
        _clientRepository = clientRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _paymentService = paymentService;
        _invoiceService = invoiceService;
    }

    public async Task<Order> CreateOrder(int clientId, PaymentMethod paymentMethod)
    {
        var client = _clientRepository.GetClientById(clientId);
        if (client == null)
            throw new Exception("Klient nie istnieje.");

        var cart = _cartRepository.GetCart(clientId);
        if (cart == null || !cart.Items.Any())
            throw new Exception("Koszyk jest pusty. Aby złożyć zamówienie, musisz dodać produkty do koszyka.");

        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        foreach (var cartItem in cart.Items)
        {
            var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
            if (product == null)
                throw new Exception($"Produkt o ID {cartItem.ProductId} nie istnieje.");

            // Check stock availability
            if (product.StockQuantity < cartItem.Quantity)
                throw new Exception($"Niewystarczająca ilość produktu '{product.Name}' w magazynie. Dostępne: {product.StockQuantity}, Zamówione: {cartItem.Quantity}");

            var totalPrice = product.Price * cartItem.Quantity;
            totalAmount += totalPrice;

            orderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = cartItem.Quantity,
                TotalPrice = totalPrice
            });
        }

        var order = new Order
        {
            ClientId = clientId,
            OrderItems = orderItems,
            OrderDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            PaymentMethod = paymentMethod,
            Status = OrderStatus.Pending
        };

        // Create the order
        var createdOrder = _orderRepository.CreateOrder(order);

        // Create payment record
        var payment = new Payment
        {
            OrderId = createdOrder.Id,
            Amount = totalAmount,
            PaymentMethod = paymentMethod,
            Status = PaymentStatus.Pending,
            ProcessedAt = DateTime.UtcNow
        };
        _paymentService.AddPayment(payment);

        // Create invoice for the order
        var invoice = _invoiceService.CreateInvoice(createdOrder.Id);

        // Update product stock
        foreach (var cartItem in cart.Items)
        {
            await _productRepository.UpdateStockAsync(cartItem.ProductId, cartItem.Quantity);
        }

        // Clear the cart
        _cartRepository.ClearCart(clientId);

        return createdOrder;
    }

    public List<Order> GetOrders() => _orderRepository.GetOrders();
    public Order? GetOrderById(int orderId) => _orderRepository.GetOrderById(orderId);
}