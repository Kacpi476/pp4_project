namespace EShop.Models;

public class Payment
{
    DateTime Date { get; set; }= DateTime.Now;
    public int amount { get; set; }
    public Client client { get; set; }
    public Order order { get; set; }
}