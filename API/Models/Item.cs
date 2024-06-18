using mvc_rpg.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ItemTypeID { get; set; }
        [NotMapped]
        public ItemType? ItemType { get; set; }
        public int Attack { get; set; }
        public rarity rarity { get; set; }
        public int effectID { get; set; }
    }
}
