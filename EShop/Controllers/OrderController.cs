using EShop.enums;
using EShop.Models;
using EShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers;
[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;

    public OrderController(IOrderService orderService, ICartService cartService)
    {
        _orderService = orderService;
        _cartService = cartService;
        
    }

    [HttpPost("create/{clientId}")]
    public IActionResult CreateOrder(int clientId,[FromBody] PaymentMethod paymentMethod)
    {
        // Pobierz koszyk klienta
        var cart = _cartService.GetCart(clientId);
        
        // Jeśli koszyk jest pusty, nie można złożyć zamówienia
        if (cart.Items.Count == 0)
        {
            return BadRequest("Koszyk jest pusty. Dodaj produkty do koszyka, aby złożyć zamówienie.");
        }

        // Tworzymy zamówienie na podstawie koszyka
        var order = _orderService.CreateOrder(clientId,paymentMethod);
        
        // Czyszczenie koszyka po złożeniu zamówienia
        _cartService.ClearCart(clientId);

        return Ok(new { Message = "Zamówienie zostało złożone.", OrderId = order.Id });
    }
    
    // Pobranie dostępnych metod płatności
    [HttpGet("payment-methods")]
    public IActionResult GetPaymentMethods()
    {
        var methods = Enum.GetNames(typeof(PaymentMethod)); // Pobieramy metody jako stringi
        return Ok(methods);
    }
    
    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(_orderService.GetOrders());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetOrderById(int id)
    {
        var order = _orderService.GetOrderById(id);
        if (order == null)
            return NotFound("Order not found");
        return Ok(order);
    }
}