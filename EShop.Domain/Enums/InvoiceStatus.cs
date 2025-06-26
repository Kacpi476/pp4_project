using System.Text.Json.Serialization;

namespace EShop.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InvoiceStatus
    {
        Draft,
        Issued,
        Sent,
        Paid,
        Overdue,
        Cancelled
    }
} 