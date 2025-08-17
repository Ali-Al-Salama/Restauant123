using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Restaurant.DTO;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
using Restaurant.utils;
namespace Restaurant.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppDBContext appDBContext;
        private readonly Encrypt encrypt;
        private readonly Decrypt decrypt;
        private readonly Token token;
        private readonly LoginMessageResponse loginMessageResponse;
        private readonly RandomString randomString;
        public LoginService(
            AppDBContext appDBContext1,
            Encrypt encrypt1,
            Decrypt decrypt1,
            LoginMessageResponse loginMessageResponse1,
            RandomString randomString1,
            Token token1
        )
        {
            appDBContext = appDBContext1;
            encrypt = encrypt1;
            decrypt = decrypt1;
            loginMessageResponse = loginMessageResponse1;
            randomString = randomString1;
            token = token1;
        }
        public LoginResponse UserLogin(LoginRequest loginRequest)
        {
            User? user = (from U in appDBContext.User
                         where U.Email == loginRequest.Email
                          select U).FirstOrDefault();
            if (user == null)
                return null;
            if(user.PasswordHash!=encrypt.ConvertToEncrypt(loginRequest.PasswordHash,user.PasswordSalt))
                return new LoginResponse(
                    loginMessageResponse.WrongPassword(),
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
            );
            string refresh = decrypt.ConvertToDecrypt(user.refreshToken,token.RefreshTokenKey());
            string[] parts = refresh.Split(':');
            string Email = parts[0];
            string format = "MM/dd/yyyy HH";
            DateTime Expire = DateTime.ParseExact(parts[1],format,CultureInfo.InvariantCulture);
            if(DateTime.Now>Expire)
            {
                string Refresh = randomString.Generate(token.LengthTokenKey());
                Refresh += user.Email + ':' + DateTime.Now.AddDays(token.RefreshTokenDaysPeriod());
                string refreshToken = encrypt.ConvertToEncrypt(Refresh,token.RefreshTokenKey());
                user.refreshToken = refreshToken;
                appDBContext.User.Entry(user).State = EntityState.Modified;
                appDBContext.SaveChanges();
            }
            string access1 = randomString.Generate(token.LengthTokenKey()) + user.Email.ToString() + ':' + DateTime.Now.AddHours(token.AccessTokenHoursPeriod());
            string accessToken1 = encrypt.ConvertToEncrypt(access1,token.AccessTokenKey());
            return new LoginResponse(
                null,
                user.Email,
                user.Name,
                user.Phone,
                user.Address,
                accessToken1,
                user.refreshToken
            );
        }
    }
}