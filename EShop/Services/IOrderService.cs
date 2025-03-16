using EShop.Models;

namespace EShop.Services;

public interface IOrderService
{
    List<Order> GetOrders();
    Order? GetOrderById(int id);
    Order? CreateOrder(int clientId);
}