namespace Restaurant.utils
{
    public class Token
    {
        public string AccessTokenKey()
        {
            return "7w4gJ3GCEoWTV3Nx3Al6fycW+hY2TGu2mZRJL5Yv6LE=";
        }
        public string RefreshTokenKey()
        {
            return "i+96DLzz6M7LyQaMXCNcgD0kZFx/gRkrjI1cs6dm+bY=";
        }
        public long AccessTokenHoursPeriod()
        {
            return 3;
        }
        public long RefreshTokenDaysPeriod()
        {
            return 10;
        }
        public long LengthTokenKey()
        {
            return 10;
        }
    }
}