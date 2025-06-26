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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IInvoiceService _invoiceService;

        public PaymentController(IPaymentService paymentService, IOrderService orderService, IInvoiceService invoiceService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _invoiceService = invoiceService;
        }

        [HttpGet("order/{orderId}")]
        public IActionResult GetPaymentByOrderId(int orderId)
        {
            var payment = _paymentService.GetPaymentByOrderId(orderId);
            if (payment == null)
                return NotFound("Payment not found for this order");
            
            return Ok(payment);
        }

        [HttpPut("order/{orderId}/status")]
        public IActionResult UpdatePaymentStatus(int orderId, PaymentStatus newStatus)
        {
            try
            {
                _paymentService.UpdatePaymentStatus(orderId, newStatus);
                
                // Update order status based on payment status
                var order = _orderService.GetOrderById(orderId);
                if (order != null)
                {
                    switch (newStatus)
                    {
                        case PaymentStatus.Completed:
                            order.Status = OrderStatus.Confirmed;
                            break;
                        case PaymentStatus.Failed:
                            order.Status = OrderStatus.Cancelled;
                            break;
                        case PaymentStatus.Pending:
                            order.Status = OrderStatus.Pending;
                            break;
                    }
                }
                
                return Ok(new { Message = "Payment status updated successfully", NewStatus = newStatus });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating payment status: {ex.Message}");
            }
        }

        [HttpGet("statuses")]
        public IActionResult GetPaymentStatuses()
        {
            var statuses = Enum.GetNames(typeof(PaymentStatus));
            return Ok(statuses);
        }
    }
}