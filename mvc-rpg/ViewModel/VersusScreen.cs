using mvc_rpg.Models;

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
