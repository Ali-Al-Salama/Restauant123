using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Persistence.Entity
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column]
        public long orderId { get; set; }
        [Column]
        public long userId { get; set; }

        

        [Column]
        public String userAddress{get; set;}

        [Column]
        public string orderStatus { get; set; }

        [Column]
        public float paymentCost { get; set; }

        public Payment() { }
        public Payment(long orderid, long userid,string useraddress, string status, float paymentcost)
        {
            
            orderId = orderid;
            userId = userid;
           
            userAddress=useraddress;
            orderStatus = status;
            paymentCost = paymentcost;

        }


    }
}