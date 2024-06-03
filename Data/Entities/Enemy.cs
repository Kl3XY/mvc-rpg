using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_rpg.Models
{
    public class Enemy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isAlive { get; set; }
        public int EnemyTypeID { get; set; }
        public EnemyType EnemyType { get; set; }
    }
}
