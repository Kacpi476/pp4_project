using EShop.Models;
namespace EShop.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly List<Payment> _paymentList = new();
        public PaymentRepository() 
        {
            _paymentList.Add(new Payment() { amount = 1});
            _paymentList.Add(new Payment() { amount = 2 });
        }
        public List<Payment> GetPayments()=>_paymentList;

        List<PaymentRepository> IPaymentRepository.GetPayments()
        {
            throw new NotImplementedException();
        }
    }
}
