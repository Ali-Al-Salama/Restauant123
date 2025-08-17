using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurant.Persistence.Entity
{
    [Table("Item")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {  get; set; }
        [Required]
        [Column]
        public  string Name { get; set; }
        [Column]        
        public  string Category { get; set; }
        [Column]
        public  string URL {get;set;}
        [Column]
        public float Price {get;set;}
        [Column]
        public  string Description{get;set;}
        [Column]
        public bool IsAvailable {get;set;}

        public Item() { }
        public Item(
            long id,
            string name,
            string category,
            string url,
            float price,
            string description,
            bool isAvailable
        )
        { 
            Id = id;
            Name = name;
            Category = category;
            URL = url;
            Price = price;
            Description = description;
            IsAvailable = isAvailable;
        }
        public Item(
            string name,
            string category,
            string url,
            float price,
            string description,
            bool isAvailable
        )
        { 
            Name = name;
            Category = category;
            URL = url;
            Price = price;
            Description = description;
            IsAvailable = isAvailable;
        }
    }
}