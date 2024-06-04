namespace API.Models
{
    public enum killedBy
    {
        Player,
        Enemy
    }
    public class Grave
    {
        public int ID { get; set; }
        public int EnemyID { get; set; }
        public int PlayerID { get; set; }
        public killedBy KilledBy { get; set; }
        public DateTime DateTime { get; set; }
    }
}
