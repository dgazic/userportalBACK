using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;

namespace IN2.UserPortal.Core.Interfaces
{
    public interface IUserRegisterService
    {
        Task<UserRegisterResponse> UserRegister(UserRegisterDto request, int administratorId);
    }
}
