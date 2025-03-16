using EShop.Models;
using EShop.Repositories;

namespace EShop.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    
    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public void AddToCart(int clientId, int productId, int quantity)
    {
        var product = _productRepository.GetProductById(productId);
        if (product == null)
        {
            throw new Exception("Produkt nie istnieje.");
        }
        // Pobierz aktualny koszyk
        var cart = _cartRepository.GetCart(clientId);
    
        // Sprawdź, czy produkt już istnieje w koszyku
        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            existingItem.TotalPrice = product.Price * existingItem.Quantity;
        }
        else
        { 
            var orderItem = new OrderItem
                   {
                       ProductId = productId,
                       ProductName = product.Name,
                       ProductPrice = product.Price,
                       Quantity = quantity,
                       TotalPrice = product.Price * quantity
                   }; 
            _cartRepository.AddToCart(clientId, orderItem);
        }
    }

    public void RemoveFromCart(int clientId, int productId)
    {
        _cartRepository.RemoveFromCart(clientId, productId);
    }
    
    public void ClearCart(int clientId)
    {
        _cartRepository.ClearCart(clientId);
    }
    public Cart GetCart(int clientId)
    {
        return _cartRepository.GetCart(clientId);
    }
}