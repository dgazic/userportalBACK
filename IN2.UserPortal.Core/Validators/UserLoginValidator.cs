using FluentValidation;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Interfaces;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace IN2.UserPortal.Core.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        private readonly IUserRepository _userPersistance;

        public UserLoginValidator(IUserRepository userPersistance)
        {
            _userPersistance = userPersistance;

            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("Username is required!");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required!");
            RuleFor(e => e).MustAsync(VerifyLogin).WithMessage("Korisničko ime ili lozinka nisu ispravni!");
        }

        private async Task<bool> VerifyLogin(UserLoginDto userLogin, CancellationToken token)
        {
            var user = await _userPersistance.GetUserLoginData(userLogin.Username);

            if(user != null) { 
                var sha256 = SHA256.Create();

                byte[] password = (sha256.ComputeHash(Encoding.UTF8.GetBytes((userLogin.Password) + user.SaltPassword)));
                bool areEqual = StructuralComparisons.StructuralEqualityComparer.Equals(password, user.Password);
                if (areEqual)
                    return true;
            }
            else
                return false;

            return false;
        }

    }
}
