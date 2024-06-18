namespace mvc_rpg.Entities
{
    public enum rarity
    {
        common,
        uncommon,
        rare,
        epic,
        legendary
    }
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ItemTypeID { get; set; }
        public ItemType ItemType { get; set; }
        public int Attack { get; set; }
        public rarity rarity { get; set; }
        public int effectID { get; set; }
        public List<Player> Players { get; } = [];
    }
}
