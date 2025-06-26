using EShop.Application.Service;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult GetInvoices()
        {
            return Ok(_invoiceService.GetInvoices());
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoiceById(int id)
        {
            var invoice = _invoiceService.GetInvoiceById(id);
            if (invoice == null)
                return NotFound("Invoice nie znaleziony");
            return Ok(invoice);
        }

        [HttpGet("order/{orderId}")]
        public IActionResult GetInvoiceByOrderId(int orderId)
        {
            var invoice = _invoiceService.GetInvoiceByOrderId(orderId);
            if (invoice == null)
                return NotFound("Invoice not found for this order");
            return Ok(invoice);
        }

        [HttpPost("create/{orderId}")]
        public IActionResult CreateInvoice(int orderId)
        {
            try
            {
                var invoice = _invoiceService.CreateInvoice(orderId);
                return Ok(new { 
                    Message = "Invoice created successfully", 
                    InvoiceId = invoice.Id,
                    InvoiceNumber = invoice.InvoiceNumber,
                    TotalAmount = invoice.TotalAmount,
                    IssueDate = invoice.IssueDate
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating invoice: {ex.Message}");
            }
        }
    }
}