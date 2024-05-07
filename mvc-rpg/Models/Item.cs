namespace mvc_rpg.Models
{
    public enum itemType
    {
        Collectible,
        Gold,
        Furniture
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public itemType ItemType { get; set; }
        public int Strength { get; set; }
    }
}
