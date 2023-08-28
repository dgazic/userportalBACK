using AutoMapper;
using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;
using IN2.UserPortal.Core.Validators;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Models;
using Microsoft.Extensions.Configuration;

namespace IN2.UserPortal.Core.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository _userPersistance;
        
        private readonly IMapper _mapper;

        public string ErrorMessage { get; set; }

        public UserAccountService(IUserRepository userPersistance, IMapper mapper)
        {
            _userPersistance = userPersistance;
            _mapper = mapper;
        }
        public async Task<UserDetailDto> GetUserById(int id)
        {
            var userDetail = await _userPersistance.GetById(id);

            return _mapper.Map<UserDetailDto>(userDetail);
        }

        public async Task<UserAccountResponse> UpdateUser(UserDto request)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                var userModel = _mapper.Map<UserModel>(request);
                await _userPersistance.Update(userModel);
            }
            return response;
        }

        public async Task<UserAccountResponse> ActivateDeactivateUser(UserDto request)
        {
            var userModel = _mapper.Map<UserModel>(request);
            await _userPersistance.ActivateDeactivateUser(userModel);
            return new UserAccountResponse { Success = true };
        }

        public async Task<UserAccountResponse> DeleteUser(UserDto request)
        {
            var userModel = _mapper.Map<UserModel>(request);
            await _userPersistance.Delete(userModel);
            return new UserAccountResponse { Success = true };
        }

        private async Task<UserAccountResponse> Validate(UserDto request)
        {
            var response = new UserAccountResponse();
            var validator = new UserAccountValidator(_userPersistance);
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
