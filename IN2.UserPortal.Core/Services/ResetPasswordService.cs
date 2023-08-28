using AutoMapper;
using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;
using IN2.UserPortal.Core.Services.EmailService;
using IN2.UserPortal.Core.Utils;
using IN2.UserPortal.Core.Validators;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace IN2.UserPortal.Core.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IUserRepository _userPersistance;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;


        public string ErrorMessage { get; set; }

        public ResetPasswordService(IUserRepository userPersistance, IEmailSender emailSender, IMapper mapper)
        {
            _userPersistance = userPersistance;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordDto request)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                byte[] resetPasswordToken = PasswordManager.GenerateActivationToken();

                var userModel = new UserModel { Email = request.Email, ActivationToken = resetPasswordToken };
                await _userPersistance.InsertResetPasswordToken(userModel);

                string encodedToken = WebEncoders.Base64UrlEncode(resetPasswordToken);
                var link = $"<a href='https://userportal.in2.hr:1221/#/activationAccount?id={encodedToken}'>Resetiraj lozinku!</a>";
                var message = new Message(new string[] { request.Email }, "IN2 - Korisnički portal - resetiranje lozinke", $"Poštovani,<br> Molimo Vas da kliknete na link ispod te tako resetirate Vašu lozinku<br> Link {link} ");
                _emailSender.SendEmail(message);
            }
            return response;
        }

        private async Task<ResetPasswordResponse> Validate(ResetPasswordDto request)
        {
            var response = new ResetPasswordResponse();
            var validator = new ResetPasswordValidator(_userPersistance);
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
