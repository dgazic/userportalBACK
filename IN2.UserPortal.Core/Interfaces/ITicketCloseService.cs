using IN2.UserPortal.Core.Responses;

namespace IN2.UserPortal.Core.Interfaces
{
    public interface ITicketCloseService
    {
        Task<TicketCloseRespose> TicketClose(int id);
    }
}
