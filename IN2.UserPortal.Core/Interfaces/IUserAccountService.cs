using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;

namespace IN2.UserPortal.Core.Interfaces
{
    public interface IUserAccountService
    {
        Task<UserDetailDto> GetUserById(int id);
        Task<UserAccountResponse> UpdateUser(UserDto request);
        Task<UserAccountResponse> ActivateDeactivateUser(UserDto request);
        Task<UserAccountResponse> DeleteUser(UserDto request);
    }
}
