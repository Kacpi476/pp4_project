using EShop.Models;

namespace EShop.Services;

public interface ICartService
{
    void AddToCart(int clientId, int productId, int quantity);
    void RemoveFromCart(int clientId, int productId);
    Cart GetCart(int clientId);
    void ClearCart(int clientId);
}