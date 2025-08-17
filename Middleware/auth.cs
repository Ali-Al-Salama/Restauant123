
using System.Globalization;
using System.Net;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
using Restaurant.Services;
using Restaurant.utils;

namespace Restaurant.Middleware
{
    public class auth : IMiddleware
    {
        private readonly AppDBContext appDBContext;
        private readonly Decrypt decrypt;
        private readonly Token token;
        private readonly ConvertString2Long convertString2Long;
        public auth (AppDBContext appDBContext1,Decrypt decrypt1, Token token1, ConvertString2Long convertString2Long1)
        {
            appDBContext = appDBContext1;
            decrypt = decrypt1;
            token = token1;
            convertString2Long = convertString2Long1;
        }
        public async Task InvokeAsync(HttpContext httpContext ,RequestDelegate next){
            var requestUrl = httpContext.Request.Path;
            string securedPath="/api/user/";
            if(!requestUrl.ToString().Contains(securedPath)||requestUrl.ToString().Contains("api/user/reset-password"))/*||
                requestUrl.ToString().Contains("api/meal")||
                requestUrl.ToString().Contains("api/restaurant/menu")||
                requestUrl.ToString().Contains("api/weekly")||
                requestUrl.ToString().Contains("api/order")||
                requestUrl.ToString().Contains("api/payment")||
                requestUrl.ToString().Contains("api/manager")
                )*/
            {
                string? access = httpContext.Request.Headers["accesstoken"];
                if(access is null)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized; 
                }
                else
                {
                    string? DataString = decrypt.ConvertToDecrypt(access,token.AccessTokenKey());
                    DataString = DataString.Substring((int)token.LengthTokenKey());
                    string[] parts = DataString.Split(':');
                    string Email = parts[0];
                    string format = "MM/dd/yyyy HH";
                    DateTime Expire = DateTime.ParseExact(parts[1],format,CultureInfo.InvariantCulture);
                        //User user2 = appDBContext.User.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();
                    User? user1 = (from user in appDBContext.User where user.Email == Email select user).FirstOrDefault(); 
                    if(user1 is null || DateTime.Now > Expire)
                    {
                        if(DateTime.Now > Expire)
                            httpContext.Response.Headers["error-message"] = "Access Token is unactive";
                        else 
                            httpContext.Response.Headers["error-message"] = Email;
                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                    else
                    {
                        if(requestUrl.ToString().Contains("api/manager"))
                        {
                            if(user1.Role == "manager")
                            {
                                httpContext.Request.Headers["role"] = "manager";
                                await next(httpContext);
                            }
                            else
                            {
                                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            }
                        }
                        else
                        {
                            httpContext.Request.Headers["UserId"]=user1.Id.ToString();
                            await next(httpContext);
                        }
                    }
                }
            }
            else
            {
                await next(httpContext);
            }
        }
    }
}