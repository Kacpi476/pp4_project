namespace EShop.Models;

public class Payment
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public int ClientId { get; set; }
    public decimal Amount { get; set; }
    public string paymentStatus { get; set; }
    public string paymentMethod { get; set; }
    public DateTime PaymentDate { get; set; }
}