using System.Text.Json.Serialization;

namespace EShop.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentMethod
{
    CreditCard,
    PayPal,
    CashOnDelivery,
    AccountBalance
}