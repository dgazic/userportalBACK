using FluentValidation;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Interfaces;

namespace IN2.UserPortal.Core.Validators
{
    public class UserAccountValidator : AbstractValidator<UserDto>
    {
        private readonly IUserRepository _userPersitance;

        public UserAccountValidator(IUserRepository userPersistance)
        {
            _userPersitance = userPersistance;

            //RuleFor(x => x.Activated).NotNull().NotEmpty().WithMessage("Activated is required!");
            //RuleFor(x => x.UserRoleId).NotNull().NotEmpty().WithMessage("UserRoleId is required!");
            //RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required!");
            //RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required!");
        }
    }
}
