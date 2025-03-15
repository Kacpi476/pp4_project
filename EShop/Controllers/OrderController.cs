using EShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers;
[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromQuery] int clientId, [FromQuery] int productId)
    {
        var order = _orderService.CreateOrder(clientId, productId);

        if (order == null)
            return BadRequest("Nie znaleziono klienta lub produktu");

        return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(_orderService.GetOrders());
    }
}