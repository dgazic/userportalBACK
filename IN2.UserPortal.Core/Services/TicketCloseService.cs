using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Responses;
using IN2.UserPortal.Persistance.Interfaces;

namespace IN2.UserPortal.Core.Services
{
    public class TicketCloseService : ITicketCloseService
    {
        private readonly ITicketRepository _ticketPersistance;

        public TicketCloseService(ITicketRepository ticketRepository)
        {
            _ticketPersistance = ticketRepository;
        }

        public async Task<TicketCloseRespose> TicketClose(int id)
        {
            var response = new TicketCloseRespose();
            try
            {
                await _ticketPersistance.TicketClose(id);
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }
    }
}
