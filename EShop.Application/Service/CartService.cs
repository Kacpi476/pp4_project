using EShop.Domain.Models;
using EShop.Domain.Repositories;
using System.Threading.Tasks;

namespace EShop.Application.Service;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    
    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task AddToCart(int clientId, int productId, int quantity)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        
        var cartItem = new CartItem
        {
            ProductId = productId,
            Product = product,
            Quantity = quantity,
            UnitPrice = product.Price,
            TotalPrice = product.Price * quantity
        };
        
        _cartRepository.AddToCart(clientId, cartItem);
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