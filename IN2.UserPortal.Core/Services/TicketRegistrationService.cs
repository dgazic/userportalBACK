using AutoMapper;
using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;
using IN2.UserPortal.Core.Validators;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Models;
using Microsoft.Extensions.Configuration;

namespace IN2.UserPortal.Core.Services
{
    public class TicketRegistrationService : ITicketRegistrationService
    {
        private readonly ITicketRepository _ticketPersistance;
        private readonly IMapper _mapper;

        public TicketRegistrationService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketPersistance = ticketRepository;
            _mapper = mapper;
        }

        public async Task<TicketRegistrationResponse> TicketRegistration(TicketRegistrationDto request, int userId)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                var attachmentModels = new List<TicketAttachmentModel>();
                foreach (var attachment in request.Attachments)
                {
                    attachmentModels.Add(new TicketAttachmentModel
                    {
                        title = attachment.title,
                        documentExtension = attachment.documentExtension,
                        documentData = attachment.documentData,
                        size = attachment.size
                    }); 
                }

                var ticketModel = new TicketModel
                {
                    Abstract = request.Abstract,
                    Description = request.Description,
                    Type = request.Type,
                    Product = request.Product,
                    Domain = request.Domain,
                    Priority = request.Priority,
                    UserId = userId,
                    Attachments = attachmentModels
                };

                await _ticketPersistance.Insert(ticketModel);
            }
            return response;
        }

        private async Task<TicketRegistrationResponse> Validate(TicketRegistrationDto request)
        {
            var response = new TicketRegistrationResponse();
            var validator = new TicketRegistrationValidator(_ticketPersistance);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Count > 0)
            {
                response.Success = false;
                foreach (var error in validatorResult.Errors)
                    response.ValidationErrors.Add(error.ErrorMessage);
            }
            return response;
        }
    }


}
