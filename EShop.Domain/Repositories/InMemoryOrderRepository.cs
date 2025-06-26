using EShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Domain.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();
        private int _nextOrderId = 1;
        private int _nextOrderItemId = 1;

        public List<Order> GetOrders() => _orders;

        public Order? GetOrderById(int id) => _orders.FirstOrDefault(o => o.Id == id);

        public Order CreateOrder(Order order)
        {
            order.Id = _nextOrderId++;
            
            // Assign IDs and OrderId to OrderItems
            foreach (var item in order.OrderItems)
            {
                item.Id = _nextOrderItemId++;
                item.OrderId = order.Id;
            }
            
            _orders.Add(order);
            return order;
        }
    }
} 