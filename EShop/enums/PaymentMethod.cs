using System.Text.Json.Serialization;
namespace EShop.enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentMethod
{
    CreditCard,
    PayPal,
    CashOnDelivery,
    AccountBalance
}