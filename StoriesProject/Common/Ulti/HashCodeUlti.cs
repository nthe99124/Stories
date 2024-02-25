using System.Text;
using System.Security.Cryptography;

namespace StoriesProject.Common.Ulti
{
    public class HashCodeUlti
    {
        /// <summary>
        /// Hàm encode password với md5
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="originalPassword"></param>
        /// <returns></returns>
        public static string EncodePassword(string originalPassword)
        {
            var md5 = new MD5CryptoServiceProvider();
            var originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            var encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
    }
}
