using EShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers;
[ApiController]
[Route("api/invoice")]
public class InvoiceController :ControllerBase
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
}