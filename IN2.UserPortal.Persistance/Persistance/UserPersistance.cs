using Dapper;
using IN2.UserPortal.Persistance.Context;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Data;

namespace IN2.UserPortal.Persistance
{
    public class UserPersistance : IUserRepository
    {
        private readonly DapperContext _context;

        public UserPersistance(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersHospital(string userHospital, int userRoleId)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new { 
                    UserHospital = userHospital,
                    UserRoleId = userRoleId
                };
                var users = await connection.QueryAsync<UserModel>("dbo.Users_Select",parameters,
                commandType: CommandType.StoredProcedure);
                return users;

            }
        }

        public async Task<UserModel> GetById(int id)
        {
            var query = "SELECT * FROM [User] WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UserModel>(query, new { id });
                return user;
            }
        }


        public async Task<UserModel> Insert(UserModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Username = model.Username,
                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    UserRoleId = model.UserRoleId,
                    Password = model.Password,
                    SaltPassword = model.SaltPassword,
                    Activated = model.Activated,
                    ActivationToken = model.ActivationToken,
                    AdministratorId = model.AdministratorId,
                    PhoneNumber = model.PhoneNumber,
                    HospitalName = model.Hospital
                };

                var userModels = await connection.QueryAsync<UserModel>("dbo.User_Insert", parameters,
                commandType: CommandType.StoredProcedure);
                return userModels.FirstOrDefault();
            }
        }

        public async Task<UserModel> GetUserLoginData(string username)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new { Username = username };

                var userModels = await connection.QueryAsync<UserModel>("dbo.LoginData_Select", parameters,
                commandType: CommandType.StoredProcedure);
                return userModels.FirstOrDefault();
            }
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            using(var connection = _context.CreateConnection())
            {
                var parameters = new { Email = email };
                
                var userModel = await connection.QueryAsync<UserModel>("dbo.GetUserByEmail_Select", parameters, commandType: CommandType.StoredProcedure);
                return userModel.FirstOrDefault();
            }
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new { Username = username };
                var userModel = await connection.QueryAsync<UserModel>("dbo.GetUserByUsername_Select",parameters, commandType: CommandType.StoredProcedure);
                return userModel.FirstOrDefault();
            }
        }

        public async Task<UserModel> GetUserByActivationToken(string activationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                byte[] decodedToken = WebEncoders.Base64UrlDecode(activationToken);
                var parameters = new { ActivationToken = decodedToken };
                var userModels = await connection.QueryAsync<UserModel>("dbo.ActivationTokenGetUser_Select", parameters,
                commandType: CommandType.StoredProcedure);
                return userModels.FirstOrDefault();
            }
        }

        public async Task<UserModel> UpdateUserActivationToken(UserModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Password = model.Password,
                    SaltPassword = model.SaltPassword,
                    Activated = model.Activated,
                    UserId = model.Id
                };

                var userModels = await connection.QueryAsync<UserModel>("dbo.PasswordReset_Update", parameters,
                commandType: CommandType.StoredProcedure);
                return userModels.FirstOrDefault();
            }
        }

        public async Task<UserModel> InsertResetPasswordToken(UserModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Email = model.Email,
                    ResetPasswordToken = model.ActivationToken
                };
                var userModel = await connection.QueryAsync<UserModel>("dbo.PasswordResetToken_Insert", parameters,
                    commandType: CommandType.StoredProcedure);
                return userModel.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<HospitalModel>> GetHospitals()
        {
            using (var connection = _context.CreateConnection())
            {
                var hospitalProducts = await connection.QueryAsync<HospitalModel>("dbo.GetHospitals_Select",
                    commandType: CommandType.StoredProcedure);
                return hospitalProducts;
            }
        }

        public async Task<UserModel> Update(UserModel model)
        {
            using(var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserRoleId = model.UserRoleId,
                    UserId = model.Id,
                    PhoneNumber = model.PhoneNumber
                };
                var userModel = await connection.QueryAsync<UserModel>("dbo.User_Update", parameters, commandType: CommandType.StoredProcedure);
                return userModel.FirstOrDefault();
            }
        }

        public async Task Delete(UserModel model)
        {
            using(var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    UserId = model.Id
                };
                await connection.ExecuteAsync("dbo.User_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task LogUserLogIn(UserLoginHistoryModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    SessionUuid = model.SessionUuid,
                    UserId = model.UserId,
                    ApplicationType = model.ApplicationType,
                    DevicePlatform = model.DevicePlatform,
                    DeviceVersion = model.DeviceVersion,
                    DeviceBrand = model.DeviceBrand,
                    DeviceModel = model.DeviceModel,
                    Browser = model.Browser,
                    BrowserVersion = model.BrowserVersion
                };
                await connection.ExecuteAsync("dbo.UserLoginHistory_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ActivateDeactivateUser(UserModel model)
        {
            using(var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    UserId = model.Id
                };
                await connection.ExecuteAsync("dbo.ActivateDeactivateUser_Update", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public Task<IEnumerable<UserModel>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}




