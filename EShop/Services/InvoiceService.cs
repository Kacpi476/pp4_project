using EShop.Models;
using EShop.Repositories;

namespace EShop.Services;

public class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

    public List<Invoice> GetInvoices() => _invoiceRepository.GetInvoices();

    public Invoice? GetInvoiceById(int id) => _invoiceRepository.GetInvoiceById(id);
}