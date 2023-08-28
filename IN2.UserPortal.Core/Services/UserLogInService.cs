using AutoMapper;
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
    public class UserLogInService : IUserLogInService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public string ErrorMessage { get; set; }

        public UserLogInService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<UserLogInResponse> UserLogin(UserLoginDto request)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                var userModel = await _userRepository.GetUserByUsername(request.Username);

                var userLoginHistoryModel = new UserLoginHistoryModel
                {
                    SessionUuid = request.SessionUuid,
                    UserId = userModel.Id,
                    ApplicationType = request.DeviceModel.ApplicationType,
                    DevicePlatform = request.DeviceModel.DevicePlatform,
                    DeviceVersion = request.DeviceModel.DeviceVersion,
                    DeviceBrand = request.DeviceModel.DeviceBrand,
                    DeviceModel = request.DeviceModel.DeviceModel,
                    Browser = request.DeviceModel.Browser,
                    BrowserVersion = request.DeviceModel.BrowserVersion
                };
                await _userRepository.LogUserLogIn(userLoginHistoryModel);
                response.Token = TokenGenerator.CreateToken(userModel, _configuration);
            }
            return response;
        }

        private async Task<UserLogInResponse> Validate(UserLoginDto request) 
        {
            var response = new UserLogInResponse();
            var validator = new UserLoginValidator(_userRepository);
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

