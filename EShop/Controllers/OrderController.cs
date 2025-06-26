using EShop.Application.Service;
using EShop.Domain.Enums;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public async Task<IActionResult> CreateOrder(int clientId, PaymentMethod paymentMethod)
        {
            try
            {
                if (paymentMethod == null)
                {
                    return BadRequest("Nie podano metody płatności.");
                }
                
                // Pobierz koszyk klienta
                var cart = _cartService.GetCart(clientId);
                
                // Jeśli koszyk jest pusty, nie można złożyć zamówienia
                if (cart.Items.Count == 0)
                {
                    return BadRequest("Koszyk jest pusty. Dodaj produkty do koszyka, aby złożyć zamówienie.");
                }
                
                // Tworzymy zamówienie na podstawie koszyka
                var order = await _orderService.CreateOrder(clientId, paymentMethod);
                
                return Ok(new { 
                    Message = "Zamówienie zostało złożone.", 
                    OrderId = order.Id,
                    TotalAmount = order.TotalAmount,
                    PaymentMethod = order.PaymentMethod,
                    Status = order.Status
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas tworzenia zamówienia: {ex.Message}");
            }
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
}