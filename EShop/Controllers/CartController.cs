using EShop.Models;
using EShop.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers;
[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{clientId}")]
    public IActionResult GetCart(int clientId)
    {
        var cart = _cartService.GetCart(clientId);
        if (cart.Items.Count == 0)
        {
            return NotFound("Cart is empty");
        }
        return Ok(cart);   
    }
    [HttpPost("add")]
    public IActionResult AddToCart(int clientId, int productId, int quantity)
    {
        _cartService.AddToCart(clientId, productId, quantity);
        return Ok("Produkt został dodany do koszyka.");
    }
    
    [HttpDelete("remove")]
    public IActionResult RemoveFromCart(int clientId, int productId)
    {
        _cartService.RemoveFromCart(clientId, productId);
        return Ok("Produkt został usunięty z koszyka.");
    }
    
    [HttpPost("clear")]
    public IActionResult ClearCart(int clientId)
    {
        _cartService.ClearCart(clientId);
        return Ok("Koszyk został wyczyszczony.");
    }
}