using Azure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;

namespace mvc_rpg.Models
{
    [Index(nameof(Name))]
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isAlive { get; set; }
        [NotMapped]
        public IFormFile ProfilePictureRaw { get; set; }
        public byte[] ProfilePicture { get; set; }
        public List<Item> Items { get; } = [];

    }
}
