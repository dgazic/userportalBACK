using IN2.UserPortal.Core.Responses;

namespace IN2.UserPortal.Core.Interfaces
{
    public interface ITicketQueryService
    {
        Task<TicketResponse> GetById(int id);
    }
}
