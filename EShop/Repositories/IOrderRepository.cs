using EShop.Models;

namespace EShop.Repositories;

public interface IOrderRepository
{
    List<Order> GetOrders();
    Order? GetOrderById(int id);
    Order CreateOrder(Order order);
}