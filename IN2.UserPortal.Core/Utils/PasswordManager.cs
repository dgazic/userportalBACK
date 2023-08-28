using System.Security.Cryptography;
using System.Text;

namespace IN2.UserPortal.Core.Utils
{
    public static class PasswordManager
    {
        public static byte[] GeneratePasswordHash(string password, byte[] salt)
        {
            var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        }

        public static byte[] GenerateSaltHash()
        {
            var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
        }

        public static byte[] GenerateActivationToken()
        {
            var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
        }
    }
}
