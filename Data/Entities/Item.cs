namespace mvc_rpg.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ItemTypeID { get; set; }
        public ItemType ItemType { get; set; }
        public List<Player> Players { get; } = [];
    }
}
