using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;
using IN2.UserPortal.Core.Services.EmailService;
using IN2.UserPortal.Core.Utils;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Models;
using IN2.UserPortal.Validators;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace IN2.UserPortal.Core.Services
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;

        public string ErrorMessage { get; set; }
        public UserRegisterService(IUserRepository userRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        //Prije aktivacije - lozinka nije postavljena i račun nije aktiviran
        public async Task<UserRegisterResponse> UserRegister(UserRegisterDto request, int administratorId)
        {
            var response = await Validate(request);

            if (response.Success) { 
                byte[] activationToken = PasswordManager.GenerateActivationToken();
                
                string encodedToken = WebEncoders.Base64UrlEncode(activationToken);

                var link = $"<a href='https://userportal.in2.hr:1221/#/activationAccount?id={encodedToken}'>Aktiviraj račun!</a>";
                var userModel = new UserModel { Username = request.Username, Email = request.Email, LastName = request.LastName, FirstName = request.FirstName, UserRoleId = request.UserRoleId, Activated = 0, ActivationToken = activationToken, AdministratorId = administratorId, PhoneNumber = request.PhoneNumber, Hospital = request.HospitalName };
                await _userRepository.Insert(userModel);

                var message = new Message(new string[] { request.Email }, "IN2 - Korisnički portal - aktivacija računa", $"Poštovani,<br> {request.LastName + ' ' + request.FirstName} <br> Molimo Vas da kliknete na link ispod te tako aktivirate Vaš račun! <br> Link {link} ");
                _emailSender.SendEmail(message);
            }
            return response;
        }

        private async Task<UserRegisterResponse> Validate(UserRegisterDto request)
        {
            var response = new UserRegisterResponse();
            bool userExist = false;
            var userEmail = await _userRepository.GetUserByEmail(request.Email);
            if (userEmail != null) {
                userExist = true;
            }

            var validator = new UserRegisterValidator(_userRepository);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Count > 0)
            {
                response.Success = false;
                foreach (var error in validatorResult.Errors)
                    response.ValidationErrors.Add(error.ErrorMessage);
            }
            else if (userExist == true)
            {
                response.Success = false;
                response.ValidationErrors.Add("Korisnik s unesenom email adresom već postoji!");
            }
                
            return response;
        }
    }
}
