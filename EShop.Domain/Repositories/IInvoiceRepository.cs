using EShop.Domain.Models;

namespace EShop.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetInvoices();
        Invoice? GetInvoiceById(int id);
        Invoice? GetInvoiceByOrderId(int orderId);
        Invoice CreateInvoice(Invoice invoice);
        Invoice UpdateInvoice(Invoice invoice);
    }
}
