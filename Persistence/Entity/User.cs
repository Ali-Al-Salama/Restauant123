using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Persistence.Entity
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get;set;}
        [Column]
        public string Email {get;set;}
        [Column]
        public string Name {get;set;}
        [Column]
        public string Phone {get;set;}
        [Column]
        public string Role {get;set;}
        [Column]
        public string Address {get;set;}
        [Column]
        public string PasswordHash {get;set;}
        [Column]
        public string PasswordSalt {get;set;}        
        [Required]
        [Column]
        public string refreshToken { get; set;}

        public User(){}
        public User(
            string email,
            string name,
            string phone,
            string role,
            string address,
            string passwordHash,
            string passwordSalt
            ){
            Email=email;
            Name=name;
            Phone=phone;
            Role=role;
            Address=address;
            PasswordHash = passwordHash;
            PasswordSalt=passwordSalt;
        }
        public User(
            string email,
            string name,
            string phone,
            string role,
            string address,
            string passwordHash,
            string passwordSalt,
            string refreshToken1
            ){
            Email=email;
            Name=name;
            Phone=phone;
            Role=role;
            Address=address;
            PasswordHash = passwordHash;
            PasswordSalt=passwordSalt;
            refreshToken=refreshToken1;
        }
    }
}