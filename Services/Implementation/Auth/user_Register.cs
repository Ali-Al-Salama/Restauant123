using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Persistence.Entity;
using Restaurant.Services.Interfaces;
using Restaurant.DTO;
using Restaurant.utils;
using Microsoft.AspNetCore.Identity;
using static restaurant.DTO.User.user;
using Restaurant.Persistence;

namespace Restaurant.Services.Implementation
{
    public class user_Register : IUserRegisterServices
    {
        private readonly AppDBContext _appDbContext;
        private readonly Encrypt encrypt;
        private readonly Token token;
        private readonly RandomString randomString;
        private readonly Role role;
        private readonly RegisterErrorResponse registerErrorResponse;
        private readonly WrongPassword wrongPassword;
        public user_Register(
        AppDBContext appDbContext,
        Encrypt encrypt1,
        Token token1,
        RandomString randomString1,
        Role role1,
        RegisterErrorResponse registerErrorResponse1,
        WrongPassword wrongPassword1
        )
        {
            _appDbContext = appDbContext;
            encrypt = encrypt1;
            token = token1;
            randomString = randomString1;
            role = role1;
            registerErrorResponse = registerErrorResponse1;
            wrongPassword = wrongPassword1;
        }
        public user_Response create_User(user_Rigester user)
        {
            long request_Vercod = user.verification_Code;
            pendingUser user1 = (from users in _appDbContext.pendingUser where users.Email == user.Email select users).FirstOrDefault();
            if (user1 is not null && request_Vercod == user1.VerificationCode)
            {
                if(user.Email != user1.Email)
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
                else{
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
                string passwordHash=encrypt.ConvertToEncrypt(user.Password,passwordSalt);

                string access = randomString.Generate(token.LengthTokenKey()) + user.Email.ToString() + ':' + DateTime.Now.AddHours(token.AccessTokenHoursPeriod());
                string accessToken = encrypt.ConvertToEncrypt(access,token.AccessTokenKey());
                string refresh = randomString.Generate(token.LengthTokenKey()) + user.Email.ToString() + ':' + DateTime.Now.AddDays(token.RefreshTokenDaysPeriod());
                string refreshToken = encrypt.ConvertToEncrypt(refresh,token.RefreshTokenKey());
                User newUser = new User(
                        user.Email,
                        user.Name,
                        user.Phone,
                        role.Customer(),
                        user.Address,
                        passwordHash,
                        passwordSalt,
                        refreshToken
                        );
                _appDbContext.User.Add(newUser);
                _appDbContext.pendingUser.Remove(user1);
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
            }
            else
                return new user_Response(
                    registerErrorResponse.VerificationErrorMessage(),
                    0,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                );
        }
        public user_Response delete_User(long id,string Password)
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
        public List<User> get_User(long id)
        {
            List<User> usersList = (from users in _appDbContext.User where users.Id == id select users).ToList();
            return usersList;
        }
        public user_Response update_User(UserUpdateRequest user)
        {
            var existededUser = _appDbContext.User.Find(user.Id);
            if (existededUser is null)
            {
                return (user_Response)Results.NotFound("this user not found ");
            }
            existededUser.Name = user.Name;
            existededUser.Phone = user.Phone;
            existededUser.Address = user.Address;
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