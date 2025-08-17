using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Persistence.Entity
{
    [Table("resetPassword")]
    public class ResetPassword
    {
        [Key]
        [Column]
        public string email{get;set;}
        [Column]
        public long verificationCode{get;set;}
        public ResetPassword(){ }
        public ResetPassword(
            string email1,
            long verificationCode1
        )
        {
            email = email1;
            verificationCode = verificationCode1;
        }
    }
}