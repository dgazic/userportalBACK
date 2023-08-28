using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Models;

namespace IN2.UserPortal.Core.Services
{
    public class TicketChangePriorityService : ITicketChangePriorityService
    {
        private readonly ITicketRepository _ticketPersistance;

        public TicketChangePriorityService(ITicketRepository ticketRepository)
        {
            _ticketPersistance = ticketRepository;
        }

        public async Task<TicketChangePriorityResponse> TicketChangePriority(TicketChangePriorityDto request)
        {
            var response = new TicketChangePriorityResponse();
            try
            {
                var ticketModel = new TicketModel
                {
                    Id = request.Id,
                    Priority = request.Priority,
                };
                await _ticketPersistance.TicketChangePriority(ticketModel);
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }
    }
}
