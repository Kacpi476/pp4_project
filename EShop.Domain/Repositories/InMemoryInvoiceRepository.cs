using EShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Domain.Repositories
{
    public class InMemoryInvoiceRepository : IInvoiceRepository
    {
        private readonly List<Invoice> _invoices = new();
        private int _nextId = 1;

        public InMemoryInvoiceRepository()
        {
            // Sample invoices with only the required fields
            var sampleInvoices = new List<Invoice>
            {
                new Invoice
                {
                    Id = _nextId++,
                    OrderId = 1,
                    InvoiceNumber = "INV-202412-1000",
                    TotalAmount = 1999.98m,
                    IssueDate = DateTime.UtcNow.AddDays(-5)
                },
                new Invoice
                {
                    Id = _nextId++,
                    OrderId = 2,
                    InvoiceNumber = "INV-202412-1001",
                    TotalAmount = 149.99m,
                    IssueDate = DateTime.UtcNow.AddDays(-3)
                }
            };

            foreach (var invoice in sampleInvoices)
            {
                _invoices.Add(invoice);
            }
        }

        public List<Invoice> GetInvoices() => _invoices;

        public Invoice? GetInvoiceById(int id) => _invoices.FirstOrDefault(i => i.Id == id);

        public Invoice? GetInvoiceByOrderId(int orderId) => _invoices.FirstOrDefault(i => i.OrderId == orderId);

        public Invoice CreateInvoice(Invoice invoice)
        {
            invoice.Id = _nextId++;
            _invoices.Add(invoice);
            return invoice;
        }

        public Invoice UpdateInvoice(Invoice invoice)
        {
            var existingInvoice = _invoices.FirstOrDefault(i => i.Id == invoice.Id);
            if (existingInvoice != null)
            {
                // Only update fields that exist in the simplified model
                existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
                existingInvoice.TotalAmount = invoice.TotalAmount;
                existingInvoice.IssueDate = invoice.IssueDate;
                return existingInvoice;
            }
            return invoice;
        }
    }
} 