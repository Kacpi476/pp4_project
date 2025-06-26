using EShop.Domain.Models;

namespace EShop.Domain.Repositories;

public interface ICartRepository
{
    Cart GetCart(int clientId);
    void AddToCart(int clientId, CartItem cartItem);
    void RemoveFromCart(int clientId, int productId);
    void ClearCart(int clientId);
    
}