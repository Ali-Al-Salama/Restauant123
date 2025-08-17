using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Persistence.Entity
{
     [Table("Pendinguser")]
    public class pendingUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get;set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Column]
        public long VerificationCode {get; set;}

        public pendingUser(){}
        public pendingUser(string email , long verificationcode){
            Email=email;
            VerificationCode=verificationcode;

        }


        
    }
}