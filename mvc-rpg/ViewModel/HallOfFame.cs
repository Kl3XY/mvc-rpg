using mvc_rpg.Entities;

namespace mvc_rpg.ViewModel
{
    public class HallOfFame
    {
        public string Search = "";
        public int page = 0;
        public List<System.Linq.IGrouping<mvc_rpg.Entities.Player, mvc_rpg.Entities.Grave>> Entries { get; set; }

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
