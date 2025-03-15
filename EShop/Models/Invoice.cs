namespace EShop.Models;

public class Invoice
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.Now;
}