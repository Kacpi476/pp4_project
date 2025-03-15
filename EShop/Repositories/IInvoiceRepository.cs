using EShop.Models;
namespace EShop.Repositories
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetInvoices();
        Invoice? GetInvoiceById(int id);
    }
}
