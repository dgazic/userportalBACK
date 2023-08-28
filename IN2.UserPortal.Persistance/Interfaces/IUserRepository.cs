using IN2.UserPortal.Persistance.Models;

namespace IN2.UserPortal.Persistance.Interfaces
{
    public interface IUserRepository : IAsyncRepository<UserModel>
    {
        public Task<UserModel> GetUserLoginData(string username);

        public Task<UserModel> GetUserByUsername(string username);

        public Task<UserModel> GetUserByActivationToken(string activationToken);

        public Task<UserModel> UpdateUserActivationToken(UserModel model);

        public Task<UserModel> GetUserByEmail(string email);
        public Task<UserModel> InsertResetPasswordToken(UserModel userModel);

        public Task<IEnumerable<HospitalModel>> GetHospitals();

        public Task ActivateDeactivateUser(UserModel userModel);

        public Task<UserModel> Update(UserModel model);

        public Task Delete(UserModel model);

        public Task<IEnumerable<UserModel>> GetAllUsersHospital(string userHospital, int userRoleId);

        public Task LogUserLogIn(UserLoginHistoryModel model);
    }
}
