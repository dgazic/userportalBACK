using IN2.UserPortal.Core.Services.EmailService;

namespace IN2.UserPortal.Core.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
