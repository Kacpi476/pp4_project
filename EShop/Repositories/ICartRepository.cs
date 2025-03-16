using EShop.Models;

namespace EShop.Repositories;

public interface ICartRepository
{
    Cart GetCart(int clientId);
    void AddToCart(int clientId, OrderItem orderItem);
    void RemoveFromCart(int clientId, int productId);
    void ClearCart(int clientId);
    
}