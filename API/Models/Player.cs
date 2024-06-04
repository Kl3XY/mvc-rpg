using Azure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;

namespace API.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isAlive { get; set; }
    }
}
