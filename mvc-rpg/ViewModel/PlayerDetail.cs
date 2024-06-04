using mvc_rpg.Entities;

namespace mvc_rpg.ViewModel
{
    public class PlayerDetail
    {
        public Player Player {  get; set; }
        public List<Grave> Graves {  get; set; }
    }
}
