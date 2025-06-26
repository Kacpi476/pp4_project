using EShop.Domain.Models;
using System.Threading.Tasks;

namespace EShop.Application.Service;

public interface ICartService
{
    Task AddToCart(int clientId, int productId, int quantity);
    void RemoveFromCart(int clientId, int productId);
    Cart GetCart(int clientId);
    void ClearCart(int clientId);
}