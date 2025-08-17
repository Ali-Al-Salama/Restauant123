using System.Text;

namespace Restaurant.utils
{
    public class Decrypt
    {
        public string ConvertToDecrypt(string EncodeInput,string Salt)
        {
            var base64EncodeBytes = Convert.FromBase64String(EncodeInput);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0,result.Length - Salt.Length);
            return result;
        }
    }
}