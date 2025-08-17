using System.Globalization;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
using Restaurant.utils;
using static Restaurant.DTO.token;

namespace Restaurant.Services
{
    public class AccessTokenService
    {
        private readonly AppDBContext appDBContext;
        private readonly Decrypt decrypt;
        private readonly Token token;
        private readonly TokenMessageResponse tokenMessageResponse; 
        private readonly RandomString randomString;
        private readonly Encrypt encrypt;
        private readonly InternalError internalError;
        public AccessTokenService(
            AppDBContext appDBContext1,
            Decrypt decrypt1,
            Token token1,
            TokenMessageResponse tokenMessageResponse1,
            RandomString randomString1,
            Encrypt encrypt1,
            InternalError internalError1
        )
        {
            appDBContext = appDBContext1;
            decrypt = decrypt1;
            token = token1;
            tokenMessageResponse = tokenMessageResponse1;
            randomString = randomString1;
            encrypt = encrypt1;
            internalError = internalError1;
        }
        public AccessResponse GenerateAccessToken(string refreshtoken)
        {
            string refresh = decrypt.ConvertToDecrypt(refreshtoken,token.RefreshTokenKey());
            refresh = refresh.Substring((int)token.LengthTokenKey());
            string[] parts = refresh.Split(':');
            string Email = parts[0];
            string format = "MM/dd/yyyy HH";
            DateTime Expire = DateTime.ParseExact(parts[1],format,CultureInfo.InvariantCulture);
            User? user = (from user1 in appDBContext.User where user1.Email == Email select user1).FirstOrDefault();
            if(user is null)
            {
                return new AccessResponse(
                    tokenMessageResponse.AccessResponseNotFound(),
                    null
                );
            }
            if(refreshtoken != user.refreshToken)
            {
                return new AccessResponse(
                    tokenMessageResponse.WrongRefreshToken(),
                    null
                );
            }
            if(DateTime.Now>Expire)
            {
                return new AccessResponse(
                    tokenMessageResponse.RefreshTokenUnActive(),
                    null
                );
            }
            string access = randomString.Generate(token.LengthTokenKey()) + user.Email.ToString() + ':' + DateTime.Now.AddHours(token.AccessTokenHoursPeriod());
            string accessToken = encrypt.ConvertToEncrypt(access,token.AccessTokenKey());
            try
            {
                appDBContext.User.Entry(user).State = EntityState.Modified;
                appDBContext.SaveChanges();
                return new AccessResponse(
                    null,
                    accessToken
                );
            }
            catch(Exception ex)
            {
                return new AccessResponse(
                    internalError.InternalServerErrorResponse(),
                    null
                );
            }
            
        }
    }
}