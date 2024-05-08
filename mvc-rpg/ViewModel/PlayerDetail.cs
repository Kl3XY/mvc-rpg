using mvc_rpg.Models;

namespace mvc_rpg.ViewModel
{
    public class PlayerDetail
    {
        public Player Player {  get; set; }
        public List<Grave> Graves {  get; set; }
    }
}
