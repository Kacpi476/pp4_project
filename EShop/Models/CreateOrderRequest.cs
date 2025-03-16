namespace EShop.Models;

public class CreateOrderRequest
{
    public int ClientId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}