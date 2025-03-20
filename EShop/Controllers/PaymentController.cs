using EShop.Models;
using EShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public IActionResult GetPayments()
    {
        return Ok(_paymentService.GetPayments());
    }
    
    [HttpGet("{orderId}")]
    public IActionResult GetPaymentByOrderId(int orderId)
    {
        var payment = _paymentService.GetPaymentByOrderId(orderId);
        if (payment == null)
            return NotFound("Payment not found");
        return Ok(payment);
    }
}