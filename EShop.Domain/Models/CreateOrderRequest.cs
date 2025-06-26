using System.ComponentModel.DataAnnotations;
using EShop.Domain.Enums;

namespace EShop.Domain.Models;

public class CreateOrderRequest
{
    [Required]
    public int ClientId { get; set; }
    
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
}

public class OrderItemRequest
{
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}