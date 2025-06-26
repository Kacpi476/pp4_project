using EShop.Domain.Enums;
using EShop.Domain.Models;
using System.Threading.Tasks;

namespace EShop.Application.Service;

public interface IOrderService
{
    List<Order> GetOrders();
    Order? GetOrderById(int id);
    Task<Order> CreateOrder(int clientId, PaymentMethod paymentMethod);
}