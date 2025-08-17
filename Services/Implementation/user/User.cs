using Restaurant.Services.Interfaces;
using Restaurant.DTO;
using Restaurant.Persistence.Entity;
using static restaurant.DTO.User.user;
using Restaurant.Persistence;
using Restaurant.utils;

namespace Restaurant.Services.Implementation
{
    public class user_Service : IUserService
    {

        private readonly AppDBContext _appDbContext;
        private readonly Encrypt encrypt;
        private readonly Token token;
        private readonly RandomString randomString;
        private readonly RegisterErrorResponse registerErrorResponse;
        private readonly WrongPassword wrongPassword;
        public user_Service(
            AppDBContext appDbContext,
            Encrypt encrypt1,
            Token token1,
            RandomString randomString1,
            RegisterErrorResponse registerErrorResponse1,
            WrongPassword wrongPassword1
        )
        {
            _appDbContext = appDbContext;
            encrypt = encrypt1;
            token = token1;
            randomString = randomString1;
            registerErrorResponse = registerErrorResponse1;
            wrongPassword = wrongPassword1;
        }

        public user_Response CreateUser(ManagerUserRequest user_Request)
        {
            var user = (from U in _appDbContext.User where U.Email == user_Request.Email select U).FirstOrDefault();
            if (user != null)
            {
                return new user_Response(
                    registerErrorResponse.EmailErrorMessage(),
                    0,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                );
            }
            Random r=new Random();
                 int strLen=r.Next(5,5);
                 int randValue;
                 string str="";
                 char letter;
                 for(int i=0;i<strLen;i++){
                    randValue=r.Next(0,26);
                    letter=Convert.ToChar(randValue+65);
                    str=str+letter;
                }
            string passwordSalt=str;
            string access = randomString.Generate(token.LengthTokenKey()) + user_Request.Email + ':' + DateTime.Now.AddHours(token.AccessTokenHoursPeriod());
            string accessToken = encrypt.ConvertToEncrypt(access,token.AccessTokenKey());
            string refresh = randomString.Generate(token.LengthTokenKey()) + user_Request.Email + ':' + DateTime.Now.AddDays(token.RefreshTokenDaysPeriod());
            string refreshToken = encrypt.ConvertToEncrypt(refresh,token.RefreshTokenKey());
            User newUser = new User(
                user_Request.Email,
                user_Request.Name,
                user_Request.Phone,
                user_Request.Role,
                user_Request.Address,
                encrypt.ConvertToEncrypt(user_Request.Password,passwordSalt),
                passwordSalt,
                refreshToken
            );
            _appDbContext.User.Add(newUser);
            _appDbContext.SaveChanges();

            return new user_Response(
              null,
              newUser.Id,
              newUser.Email,
              newUser.Name,
              newUser.Phone,
              newUser.Address,
              accessToken,
              refreshToken
           );


        }

        public user_Response DeleteUser(long id,string Password)
        {
            
            var user = _appDbContext.User.Find(id);
            if(user.PasswordHash != encrypt.ConvertToEncrypt(Password,user.PasswordSalt))
            {
                return new user_Response(
                wrongPassword.WrongPasswordResponse(),
                0,
                null,
                null,
                null,
                null,
                null,
                null
             );
            }
            _appDbContext.User.Remove(user);
            _appDbContext.SaveChanges();
            return new user_Response(
                null,
                user.Id,
                user.Email,
                user.Name,
                user.Phone,
                user.Address,
                null,
                null
             );
        }
        public List<User> GetUsers()
        {
            List<User> usersList = (from users in _appDbContext.User select users).ToList();
            return usersList;
        }
        public List<User> GetUsers(long id)
        {
            List<User> usersList = (from users in _appDbContext.User where users.Id == id select users).ToList();
            return usersList;
        }
        public user_Response UpdateUser(ManagerUserUpdateRequest user)
        {
            var existededUser = _appDbContext.User.Find(user.Id);
            if (existededUser is null)
            {
                return (user_Response)Results.NotFound("this user not found ");
            }
            existededUser.Name = user.Name;
            existededUser.Phone = user.Phone;
            existededUser.Address = user.Address;
            existededUser.Role = user.Role;
            _appDbContext.User.Update(existededUser);
            _appDbContext.SaveChanges();

            return new user_Response(
                null,
                user.Id,
                existededUser.Email,
                user.Name,
                user.Phone,
                user.Address,
                null,
                existededUser.refreshToken
            );
        }
    }
}