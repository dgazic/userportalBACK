using FluentValidation;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Interfaces;

namespace IN2.UserPortal.Core.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
    {
        private readonly IUserRepository _userPersistance;

        public ResetPasswordValidator(IUserRepository userPersistance)
        {
            _userPersistance = userPersistance;

            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email polje je obavezno!");
            RuleFor(y => y).MustAsync(VerifyEnteredEmail).WithMessage("Email koji ste upisali nije ispravan!");
        }

        private async Task<bool> VerifyEnteredEmail(ResetPasswordDto resetPassword, CancellationToken token)
        {
            var email = await _userPersistance.GetUserByEmail(resetPassword.Email);

            if (email == null)
            {
                return false;
            }
            return true;
        }
    }
}
