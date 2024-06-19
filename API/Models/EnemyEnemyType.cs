using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace API.Models
{
    public class EnemyEnemyType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isAlive { get; set; }
        public int EnemyTypeID { get; set; }
        public EnemyType EnemyType { get; set; }
    }
}
