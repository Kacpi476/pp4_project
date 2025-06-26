using EShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Domain.Repositories
{
    public class InMemoryCartRepository : ICartRepository
    {
        private readonly List<Cart> _carts = new();

        public Cart GetCart(int clientId)
        {
            var cart = _carts.FirstOrDefault(c => c.ClientId == clientId);
            if (cart == null)
            {
                cart = new Cart { ClientId = clientId };
                _carts.Add(cart);
            }
            return cart;
        }

        public void AddToCart(int clientId, CartItem cartItem)
        {
            var cart = GetCart(clientId);
            
            // Assign CartId to the cart item
            cartItem.CartId = cart.Id;
            
            // Generate a unique ID for the cart item
            cartItem.Id = cart.Items.Count > 0 ? cart.Items.Max(i => i.Id) + 1 : 1;
            
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == cartItem.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
                existingItem.TotalPrice = existingItem.UnitPrice * existingItem.Quantity;
            }
            else
            {
                cart.Items.Add(cartItem);
            }
            
            // Update cart total amount
            cart.TotalAmount = cart.Items.Sum(i => i.TotalPrice);
            cart.LastUpdated = DateTime.UtcNow;
        }

        public void RemoveFromCart(int clientId, int productId)
        {
            var cart = GetCart(clientId);
            var itemsToRemove = cart.Items.Where(i => i.ProductId == productId).ToList();
            foreach (var item in itemsToRemove)
            {
                cart.Items.Remove(item);
            }
        }

        public void ClearCart(int clientId)
        {
            var cart = GetCart(clientId);
            cart.Items.Clear();
        }
    }
} 