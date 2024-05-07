using mvc_rpg.Models;

namespace mvc_rpg.ViewModel
{
    public class SearchPlayers
    {
        public string Search = "";
        public List<Player> Players { get; set; }
    }
}
