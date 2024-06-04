using mvc_rpg.Entities;

namespace mvc_rpg.ViewModel
{
    public class VersusScreen
    {
        public Enemy Enemy { get; set; }
        public Player Player { get; set; }
        public List<Grave> Graves { get; set; }
        public List<Grave> PlayerKilledEnemies { get; set; }
    }
}
