using System.Security.Cryptography;
using System.Text;

namespace FourtitudeAsiaTest.Helper
{
    public static class EncryptHelper
    {
        public static string ComputeSha256Base64(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert hash bytes to Base64
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
