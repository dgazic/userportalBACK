using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;

namespace IN2.UserPortal.Core.Interfaces
{
    public interface IResetPasswordService
    {
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordDto request);
    }
}
