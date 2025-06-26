using EShop.Domain.Models;
using EShop.Domain.Repositories;
using EShop.Domain.Enums;

namespace EShop.Application.Service;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IOrderRepository _orderRepository;
    private int _nextInvoiceNumber = 1000;

    public InvoiceService(IInvoiceRepository invoiceRepository, IOrderRepository orderRepository)
    {
        _invoiceRepository = invoiceRepository;
        _orderRepository = orderRepository;
    }

    public List<Invoice> GetInvoices() => _invoiceRepository.GetInvoices();

    public Invoice? GetInvoiceById(int id) => _invoiceRepository.GetInvoiceById(id);

    public Invoice CreateInvoice(int orderId)
    {
        var order = _orderRepository.GetOrderById(orderId);
        if (order == null)
            throw new Exception("Order not found");

        var invoice = new Invoice
        {
            OrderId = orderId,
            InvoiceNumber = GenerateInvoiceNumber(),
            TotalAmount = order.TotalAmount,
            IssueDate = DateTime.UtcNow
        };

        return _invoiceRepository.CreateInvoice(invoice);
    }

    public Invoice? GetInvoiceByOrderId(int orderId)
    {
        return _invoiceRepository.GetInvoiceByOrderId(orderId);
    }

    private string GenerateInvoiceNumber()
    {
        var year = DateTime.UtcNow.Year;
        var month = DateTime.UtcNow.Month.ToString("D2");
        var number = _nextInvoiceNumber++.ToString("D4");
        return $"INV-{year}{month}-{number}";
    }
}