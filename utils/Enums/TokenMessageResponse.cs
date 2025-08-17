namespace Restaurant.utils
{
    public class TokenMessageResponse
    {
        public string AccessTokenUnActive()
        {
            return "Access Token in not active";
        }
        public string RefreshTokenUnActive()
        {
            return "Refresh Token in not active";
        }
        public string AccessResponseNotFound()
        {
            return "User Not Found";
        }
        public string WrongRefreshToken()
        {
            return "Refresh Token is wrong";
        }
    }
}