namespace Restaurant.utils
{
    public class LoginMessageResponse : TokenMessageResponse
    {
        public string WrongPassword()
        {
            return "Password is wrong";
        }
    }
}