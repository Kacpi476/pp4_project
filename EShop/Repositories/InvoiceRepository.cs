using EShop.Models;
namespace EShop.Repositories
{
    public class InvoiceRepository: IInvoiceRepository
    {
        private readonly List<Invoice>_invoice=new();
        public InvoiceRepository() 
        {
            _invoice.Add(new Invoice{Id=1,OrderId=1});
            _invoice.Add(new Invoice{ Id=2,OrderId=2});
        }
        public List<Invoice> GetInvoices() => _invoice;
        public Invoice? GetInvoiceById(int id) => _invoice.FirstOrDefault(i => i.Id == id);
    }
}
