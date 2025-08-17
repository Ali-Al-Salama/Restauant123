using Microsoft.EntityFrameworkCore;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
using Restaurant.utils;

namespace Restaurant.Services
{
    public class ResetPasswordService
    {
        private readonly AppDBContext appDBContext;
        private readonly Encrypt encryptPassword;
        private readonly ConvertString2Long convertString2Long;
        private readonly SendConfirmCode sendConfirmCode;
        public ResetPasswordService(AppDBContext appDbContext1,Encrypt encryptPassword1,ConvertString2Long convertString2Long1,SendConfirmCode sendConfirmCode1)
        {
            appDBContext = appDbContext1;
            encryptPassword = encryptPassword1;
            convertString2Long = convertString2Long1;
            sendConfirmCode = sendConfirmCode1;
        }
        public bool ResetPassword(long code,string Id,string NewPassword)
        {
            long UserId = convertString2Long.ConvertId2Long(Id);
            User? user = (from u in appDBContext.User where u.Id == UserId select u).FirstOrDefault();
            if(user == null)
                return false;
            ResetPassword? resetPassword = appDBContext.ResetPasswords.Find(user.Email);
            if(resetPassword==null || resetPassword.verificationCode!=code)
                return false;
            user.PasswordHash = NewPassword;
            appDBContext.User.Entry(user).State = EntityState.Modified;
            appDBContext.ResetPasswords.Remove(resetPassword);
            appDBContext.SaveChanges();
            return true;
        }
        public bool SendCode2User(string Id)
        {
            long userId = convertString2Long.ConvertId2Long(Id);
            User? user = (from u in appDBContext.User where u.Id == userId select u).FirstOrDefault();
            long code = 999999;
            appDBContext.ResetPasswords.Add(
               new ResetPassword(
                    user.Email,
                    code
            ));
            bool ok = sendConfirmCode.SendEmail(user.Email,code);
            if(ok)
            {
                return true;
            }
            return false;
        }
    }
}
