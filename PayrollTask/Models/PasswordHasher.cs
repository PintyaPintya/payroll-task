using System.Security.Cryptography;
using System.Text;

namespace PayrollTask.Models;

public static class PasswordHasher
{
    public static (byte[], byte[]) CreatePasswordHash(string password)
    {
        using(var hmac = new HMACSHA512())
        {
            var passwordSalt = hmac.Key;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHash = hmac.ComputeHash(passwordBytes);

            return (passwordHash, passwordSalt);
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using(var hmac = new HMACSHA512(storedSalt))
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] computedHash = hmac.ComputeHash(passwordBytes);

            return computedHash.SequenceEqual(storedHash);
        }
    }
}
