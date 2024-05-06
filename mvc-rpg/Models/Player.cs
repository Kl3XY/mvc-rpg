using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace mvc_rpg.Models
{
    public enum skinType
    {
        White,
        Black
    }
    public enum hairType
    {
        Short,
        Long,
        Buzzed
    }
    public enum eyeType
    {
        Blue,
        Brown,
        Green,
        Red
    }
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public skinType SkinType { get; set; }
        public hairType HairType { get; set; }
        public eyeType EyeType { get; set; }
        [NotMapped]
        public IFormFile ProfilePictureRaw { get; set; }
        [NotMapped]
        public Bitmap ProfilePictureRetrieve { get; set; }
        public byte[] ProfilePicture { get; set; }
        public List<Item> Inventory { get; set; }
       
    }
}
