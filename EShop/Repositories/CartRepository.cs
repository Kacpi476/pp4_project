using EShop.Models;

namespace EShop.Repositories;

public class CartRepository : ICartRepository
{
    private readonly Dictionary<int, Cart> _cartStore = new();
    
    public Cart GetCart(int clientId)
    {
        return _cartStore.ContainsKey(clientId) ? _cartStore[clientId] : new Cart{ClientId = clientId};
    }

    public void AddToCart(int clientId, OrderItem orderItem)
    {
        if (!_cartStore.ContainsKey(clientId))
        {
            _cartStore[clientId] = new Cart{ClientId = clientId};
        }
        
        var cart = _cartStore[clientId];
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == orderItem.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += orderItem.Quantity;
        }
        else
        {
            cart.Items.Add(orderItem);
        }
    }

    public void RemoveFromCart(int clientId, int productId)
    {
        if (_cartStore.ContainsKey(clientId))
        {
            var cart = _cartStore[clientId];
            cart.Items.RemoveAll(i => i.ProductId == productId);
        }
    }

    public void ClearCart(int clientId)
    {
        if (_cartStore.ContainsKey(clientId))
        {
            _cartStore.Remove(clientId);
        }
    }
}