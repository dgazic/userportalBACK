using FluentValidation;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Interfaces;

namespace IN2.UserPortal.Core.Validators
{
    public class ActivationAccountValidator : AbstractValidator<ActivationAccountDto>
    {

        private readonly IUserRepository _userPersistance;

        public ActivationAccountValidator(IUserRepository userPersistance)
        {
            _userPersistance = userPersistance;

            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required!");
            RuleFor(e => e).MustAsync(VerifyActivation).WithMessage("Token nije važeći!");
        }

        private async Task<bool> VerifyActivation(ActivationAccountDto activationAccount, CancellationToken token)
        {
            var user = await _userPersistance.GetUserByActivationToken(activationAccount.ActivationToken);

            if(user == null)
            {
                return false;
            }

            return true;
        }

    }
}
