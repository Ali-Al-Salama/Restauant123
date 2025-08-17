

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurant.Persistence.Entity
{
    public class WeeklyMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {  get; set; }
        [Column]
        public long ItemId{get;set;}
        [ForeignKey("ItemId")]
        public Item Item{get;set;}
        [Column]
        public DateTime Date { get; set; }
        public WeeklyMenu(){ }
        public WeeklyMenu(
            long itemid,
            DateTime dateTime
        )
        {
            ItemId = itemid;
            Date = dateTime;
        }
    }
}