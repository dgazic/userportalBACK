using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Core.Responses;

namespace IN2.UserPortal.Core.Interfaces
{
    public interface IActivationAccountService
    {
        Task<ActivationAccountResponse> ActivationAccount(ActivationAccountDto request);
        Task<ActivationAccountResponse> IsActivationTokenValid(String activationToken);
    }
}
