using EShop.Models;
namespace EShop.Repositories
{
    public interface IPaymentRepository
    {
        List<PaymentRepository> GetPayments();
    }
}
