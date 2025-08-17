using System.Text;

namespace Restaurant.utils
{
    public class RandomString
    {
        public String Generate(long Length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Characters to choose from
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }
            return stringBuilder.ToString();
        }
    }
}