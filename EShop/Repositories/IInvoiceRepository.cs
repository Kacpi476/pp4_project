using EShop.Models;
namespace EShop.Repositories
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetInvoices();
        Invoice? GetInvoiceByid(int id);
        Invoice AddInvoice(Invoice invoice);
    }
}
