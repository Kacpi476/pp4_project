using EShop.enums;

namespace EShop.Models;

public class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public PaymentMethod paymentMethod { get; set; }
}

public class OrderItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}