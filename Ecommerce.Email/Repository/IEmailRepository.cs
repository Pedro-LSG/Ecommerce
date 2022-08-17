using Ecommerce.Email.Messages;

namespace Ecommerce.Email.Repository
{
    public interface IEmailRepository
    {
        Task LogEmail(UpdatePaymentResultMessage message);
    }
}
