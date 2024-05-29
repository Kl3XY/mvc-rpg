using mvc_rpg.Models;

namespace mvc_rpg.ViewModel
{
    public class SearchPlayers
    {
        public string Search = "";
        public int page = 0;
        public List<Player> Players { get; set; }

        public int addToPage(int num)
        {
            return num += 1;
        }

        public int subtractFromPage(int num)
        {
            return num -= 1;
        }
    }
}
