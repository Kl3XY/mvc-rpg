using Azure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;

namespace API.Models
{
    public class PlayerItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isAlive { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public int equippedItemID { get; set; }
        public hairType hairType { get; set; }
        public skinType skinType { get; set; }
        public int Health { get; set; }
        public List<API.Models.Item> Items { get; } = [];
    }
}
