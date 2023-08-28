using AutoMapper;
using AutoMapper.Configuration;
using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Persistance;

namespace IN2.UserPortal.Core.Services
{
    public class TicketQueryService : ITicketQueryService
    {
        private ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketQueryService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<TicketResponse> GetById(int id)
        {
            var response = new TicketResponse();
            var ticket = await _ticketRepository.GetById(id);

           response.TicketDto = _mapper.Map<TicketDto>(ticket);
           response.Success = ticket != null ? true : false;
           return response;
        }
    }
}
