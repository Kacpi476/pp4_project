using EShop.Models;

namespace EShop.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();
    private int _nextOrderId = 1;

    public List<Order> GetOrders() => _orders;
    
    public Order? GetOrderById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public Order AddOrder(Order order)
    {
        order.Id = _nextOrderId++;
        _orders.Add(order);
        return order;
    }

}
    