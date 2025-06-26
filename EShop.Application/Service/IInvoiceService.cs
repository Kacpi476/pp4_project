using EShop.Domain.Models;

namespace EShop.Application.Service;

public interface IInvoiceService
{
    List<Invoice> GetInvoices();
    Invoice? GetInvoiceById(int id);
    Invoice? GetInvoiceByOrderId(int orderId);
    Invoice CreateInvoice(int orderId);
}