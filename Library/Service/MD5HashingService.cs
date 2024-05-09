using Application.IService;
using System.Security.Cryptography;
using System.Text;

namespace Library.Service
{
    public class MD5HashingService : IMD5HashingService
    {
        public MD5HashingService() { }

        public string getMD5Hash(string rawValue)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(rawValue);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    //Convert each byte to its hexadecimal representation
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
