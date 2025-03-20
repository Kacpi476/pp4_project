using EShop.enums;
using EShop.Models;
using EShop.Repositories;

namespace EShop.Services;

public class OrderService : IOrderService
{
    private readonly IClientRepository _clientRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentService _paymentService;

    public OrderService(IClientRepository clientRepository, ICartRepository cartRepository, IProductRepository productRepository, IOrderRepository orderRepository, IPaymentService paymentService)
    {
        _clientRepository = clientRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _paymentService = paymentService;
    }

    public Order CreateOrder(int clientId, PaymentMethod paymentMethod)
    {
        // Pobranie klienta
        var client = _clientRepository.GetClientById(clientId);
        if (client == null)
        {
            throw new Exception("Klient nie istnieje.");
        }

        // Pobranie koszyka klienta
        var cart = _cartRepository.GetCart(clientId);
        if (cart == null || !cart.Items.Any())
        {
            throw new Exception("Koszyk jest pusty. Aby złożyć zamówienie, musisz dodać produkty do koszyka.");
        }

        // Tworzymy zamówienie
        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        // Obliczamy całkowitą kwotę zamówienia na podstawie produktów w koszyku
        foreach (var cartItem in cart.Items)
        {
            // Pobieramy pełne dane produktu
            var product = _productRepository.GetProductById(cartItem.ProductId);
            if (product == null)
            {
                throw new Exception("Produkt nie istnieje.");
            }

            // Obliczamy cenę całkowitą dla tego produktu
            var totalPrice = product.Price * cartItem.Quantity;
            totalAmount += totalPrice;

            // Tworzymy OrderItem
            orderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = cartItem.Quantity,
                TotalPrice = totalPrice
            });
        }

        // Tworzymy zamówienie
        var order = new Order
        {
            ClientId = clientId,  // Przypisujemy ClientId
            OrderItems = orderItems,
            OrderDate = DateTime.Now,
            TotalAmount = totalAmount,
            paymentMethod = paymentMethod
        };

        // Generujemy ID zamówienia
        order.Id = _orderRepository.GetOrders().Count + 1;

        // Dodajemy zamówienie do repozytorium
        _orderRepository.CreateOrder(order);
        
        _paymentService.CreatePayment(order.Id, order.TotalAmount, paymentMethod);
        
        // Resetujemy koszyk po złożeniu zamówienia
        _cartRepository.ClearCart(clientId);
        
        return order;
    }
    public List<Order> GetOrders() => _orderRepository.GetOrders();
    public Order? GetOrderById(int orderId) => _orderRepository.GetOrderById(orderId);
}