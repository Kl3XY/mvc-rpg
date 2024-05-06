using mvc_rpg.Models;

namespace mvc_rpg.Data
{
    public static class DbInit
    {
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        public static void seed(RPGContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
            {
                return;
            }

            var defaultImage = ImageToByteArray(System.Drawing.Image.FromFile("Default\\img_defaultPicture.jpg"));

            var players = new Player[]
            {
                new Player{Name="Kl3XY", EyeType=eyeType.Blue, SkinType=skinType.Black, HairType=hairType.Long, Health=100 }, 
                new Player{Name="Yooo", EyeType=eyeType.Blue, SkinType=skinType.Black, HairType=hairType.Long, Health=100 }, 
                new Player{Name="Doomer", EyeType=eyeType.Blue, SkinType=skinType.Black, HairType=hairType.Long, Health=100 }, 
                new Player{Name="Ner", EyeType=eyeType.Blue, SkinType=skinType.Black, HairType=hairType.Long, Health=100 }, 
                new Player{Name="Bro", EyeType=eyeType.Blue, SkinType=skinType.Black, HairType=hairType.Long, Health=100 }
            };
            foreach (Player s in players)
            {
                s.ProfilePicture = defaultImage;
                context.Players.Add(s);
            }
            context.SaveChanges();
        }
    }
}
