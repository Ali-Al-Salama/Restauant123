
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurant.Persistence.Entity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id {  get; set; }
        [Required]
        [Column]
        public long UserId{get;set;}
        [ForeignKey("UserId")]
        public User User{get;set;}
        [Required]
        [Column]
        public long ItemId{get;set;}
        [ForeignKey("ItemId")]
        public Item Item{get;set;}
        [Column]
        public long Quantity{get;set;}
        [Column]
        public DateTime RequestDate{get;set;}
        [Column]
        public DateTime ReceiptDate{get;set;}
        [Column]
        public  string Status{get;set;}
        [Column]
        public  float DeliveryCost{get;set;}
        public Order() { }
        public Order(
            long userid,
            long itemid,
            long quantity,
            DateTime requestdate,
            DateTime receiptdate,
            string status,
            float delivery
        )
        {
            UserId = userid;
            ItemId = itemid;
            Quantity = quantity;
            RequestDate = requestdate;
            ReceiptDate = receiptdate;
            Status = status;
            DeliveryCost = delivery;
        }
    }
}