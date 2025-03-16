namespace EShop.Models;

public class Cart
{
    public int ClientId { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}