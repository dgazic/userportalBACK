using FluentValidation;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Interfaces;

namespace IN2.UserPortal.Core.Validators
{
    public class TicketRegistrationValidator : AbstractValidator<TicketRegistrationDto>
    {
        private readonly ITicketRepository _UserPersistance;

        public TicketRegistrationValidator(ITicketRepository userPersistance)
        {
            _UserPersistance = userPersistance;

            RuleFor(x => x.Abstract).NotNull().NotEmpty().WithMessage("Abstract is required");
            RuleFor(x => x.Type).NotNull().NotEmpty().WithMessage("Type is required");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is required");

        }

    }
}
