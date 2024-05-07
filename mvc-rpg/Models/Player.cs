using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace mvc_rpg.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isAlive { get; set; }
        [NotMapped]
        public IFormFile ProfilePictureRaw { get; set; }
        public byte[] ProfilePicture { get; set; }
        public List<Item> Inventory { get; set; }
       
    }
}
