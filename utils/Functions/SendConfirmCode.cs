using System.Net;
using System.Net.Mail;
using Restaurant.Persistence.Entity;

namespace Restaurant.utils
{
    public class SendConfirmCode
    {
        //private readonly AppDBContext appDBContext;
        //private readonly EncryptServices encryptServices;
        //public SendConfirmCode(AppDBContext appDBContext1,EncryptServices encryptServices1)
        //{
          //  appDBContext = appDBContext1;
            //encryptServices = encryptServices1; 
        //}
        public bool SendEmail(string email,long code)
        {
        //    User? user = (from u in appDBContext.User where u.Id == UserId select u).FirstOrDefault();
            //long code = 999999;
          //  if (user == null)
            //    return false;
           // appDBContext.ResetPasswords.Add(
             ///   new ResetPassword(
                //    user.Email,
                  //  code
        //    ));
            var femail = "";
            var fpassword = "";

            var themsg = new MailMessage();
            themsg.From = new MailAddress(femail);
            themsg.Subject = "Confirm Code";
            themsg.To.Add(email);
            themsg.Body = code.ToString();

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(femail, fpassword),
                Port = 587
            };
            smtpClient.Send(themsg);
            return true;
        }
    }
}