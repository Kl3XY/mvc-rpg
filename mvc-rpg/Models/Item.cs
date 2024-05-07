namespace mvc_rpg.Models
{
    public enum itemType
    {
        Collectible,
        Gold,
        Furniture,
        Trash
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public itemType ItemType { get; set; }
        public List<Player> Players { get; } = [];
    }
}
