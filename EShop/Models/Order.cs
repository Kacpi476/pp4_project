namespace EShop.Models;

public class Order
{
    public int Id { get; set; }
    public Client Client { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}