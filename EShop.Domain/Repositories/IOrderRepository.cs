using EShop.Domain.Models;

namespace EShop.Domain.Repositories;

public interface IOrderRepository
{
    List<Order> GetOrders();
    Order? GetOrderById(int id);
    Order CreateOrder(Order order);
}