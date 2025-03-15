using EShop.Models;

namespace EShop.Services;

public interface IInvoiceService
{
    List<Invoice> GetInvoices();
    Invoice? GetInvoiceById(int id);
}