using EShop.Models;
using EShop.Repositories;

namespace EShop.Services;

public class OrderService : IOrderService
{
    private readonly IClientRepository _clientRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private int _nextOrderId = 1;
    
    public OrderService(IClientRepository clientRepository, IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _clientRepository = clientRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }
    public Order CreateOrder(int clientId, int productId)
    {
        var client = _clientRepository.GetClientById(clientId);
        var product = _productRepository.GetProductById(productId);

        if (client == null || product == null)
            throw new Exception("ID klienta albo id produktu nie moze byc puste");

        var newOrder = new Order
        {
            Id = _nextOrderId++,
            Client = client,
            TotalAmount = product.Price,
            OrderDate = DateTime.Now
        };

        _orderRepository.AddOrder(newOrder);
        return newOrder;
    }
    public List<Order> GetOrders() => _orderRepository.GetOrders();
    public Order? GetOrderById(int orderId) => _orderRepository.GetOrderById(orderId);
}