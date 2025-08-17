
using System.Text;

namespace Restaurant.utils
{
    public class Encrypt
    {
        public string ConvertToEncrypt(string? Input, string? Salt)
        {
            Input += Salt;
            var InputBytes = Encoding.UTF8.GetBytes(Input);
            return Convert.ToBase64String(InputBytes);
        }
    }
}