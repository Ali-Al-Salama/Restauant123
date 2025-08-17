using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Persistence.Entity;
using Restaurant.Services.Interfaces;
using Restaurant.DTO;
using Restaurant.utils;
using static restaurant.DTO.User.user;
using Restaurant.Persistence;

namespace Restaurant.Services.Implementation
{
    public class user_Pending : IPendingUserServices
    {
         private readonly AppDBContext appDBContext;
          private readonly SendConfirmCode sendConfirmCode;

         public user_Pending(AppDBContext appDBContext1){

                 appDBContext = appDBContext1;
         }
        public pending_User_Responce create_User(pending_User_Request user)
        {
            Random r=new Random();
            long verCode=r.Next();
            
            Console.WriteLine("verCode");
           bool sucess = true;
           //bool sucess222=sendConfirmCode.SendEmail(user.Email,verCode);
           if(sucess){
             pendingUser newUser=new pendingUser(
                user.Email,
                verCode
            );
            try
            {
            appDBContext.pendingUser.Add(newUser);
            appDBContext.SaveChanges();
            return new pending_User_Responce(
                null,
                newUser.Id,
                newUser.Email,
                newUser.VerificationCode
            );
            }
            catch (Exception ex)
            {
                return new pending_User_Responce(
                ex.Message,
                0,
                null,
                0
            );
            }


           }
           else return (pending_User_Responce)Results.NotFound("this user not found ");
           
           
        }

   /*     public List<PendingUser> getPendingUser(long id)
        {
            List<PendingUser> pendingUserList = (from users in appDBContext.PendingUser where users.Id == id select users).ToList();
            return pendingUserList;
        }*/
    }
}