using FluentValidation;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Interfaces;

namespace IN2.UserPortal.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        private readonly IUserRepository _userPersistance;

        public UserRegisterValidator(IUserRepository userPersistance)
        {
            _userPersistance = userPersistance;

            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("Username is required!");
            

        }

    }
}
