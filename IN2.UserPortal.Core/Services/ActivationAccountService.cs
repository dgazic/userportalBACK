using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;
using IN2.UserPortal.Core.Utils;
using IN2.UserPortal.Core.Validators;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Models;
using Microsoft.Extensions.Configuration;

namespace IN2.UserPortal.Core.Services
{
    public class ActivationAccountService : IActivationAccountService
    {

        private readonly IUserRepository _userPersistance;
        private readonly IConfiguration _configuration;

        public string ErrorMessage { get; set; }

        public ActivationAccountService(IUserRepository userPersistance, IConfiguration configuration)
        {
            _userPersistance = userPersistance;
            _configuration = configuration;
        }

        public async Task<ActivationAccountResponse> ActivationAccount(ActivationAccountDto request)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                byte[] salt = PasswordManager.GenerateSaltHash();
                byte[] passwordHash = PasswordManager.GeneratePasswordHash(request.Password, salt);
                var user = await _userPersistance.GetUserByActivationToken(request.ActivationToken);

                var userModel = new UserModel {Password = passwordHash, SaltPassword = salt, Activated = 1, Id = user.Id };
                await _userPersistance.UpdateUserActivationToken(userModel);
            }
            return response;
        }

        private async Task<ActivationAccountResponse> Validate(ActivationAccountDto request)
        {
            var response = new ActivationAccountResponse();
            var validator = new ActivationAccountValidator(_userPersistance);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Count > 0)
            {
                response.Success = false;
                foreach (var error in validatorResult.Errors)
                    response.ValidationErrors.Add(error.ErrorMessage);
            }
            return response;
        }

        public async Task<ActivationAccountResponse> IsActivationTokenValid(String activationToken)
        {
            var user = await _userPersistance.GetUserByActivationToken(activationToken);
            var response = new ActivationAccountResponse();
            if (user == null)
                response.IsTokenValid = false;
            return response;
        }

    }
}
