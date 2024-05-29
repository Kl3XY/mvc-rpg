using mvc_rpg.Models;

namespace mvc_rpg.ViewModel
{
    public class HallOfFame
    {
        public string Search = "";
        public int page = 0;
        public List<System.Linq.IGrouping<mvc_rpg.Models.Player, mvc_rpg.Models.Grave>> Entries { get; set; }

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
