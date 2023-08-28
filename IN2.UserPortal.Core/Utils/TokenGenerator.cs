using IN2.UserPortal.Persistance.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IN2.UserPortal.Core.Utils
{
    public static class TokenGenerator
    {
        public static string CreateToken(UserModel user, IConfiguration configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("LastNameFirstName" , user.LastName + ' ' + user.FirstName),
                new Claim("UserRoleId", user.UserRoleId.ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserHospital", user.Hospital),
                new Claim("UserportalHospitalName", user.UserportalHospitalName),
                new Claim("HospitalId", user.HospitalId.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:SecretKey").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            configuration["AppSettings:Token"] = jwt;
            return jwt;
        }
    }
}
