using mvc_rpg.Entities;
using System.Drawing;

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

            var defaultImage = ImageToByteArray(System.Drawing.Image.FromFile("wwwroot\\Default\\img_defaultPicture.jpg"));

            var players = new Player[]
            {
                new Player{Name="Kl3XY", isAlive=true }, 
                new Player{Name="Yooo", isAlive=true}, 
                new Player{Name="Doomer", isAlive=true}, 
                new Player{Name="Ner", isAlive=true}, 
                new Player{Name="Bro", isAlive=true}
            };
            foreach (Player s in players)
            {
                s.ProfilePicture = defaultImage;
                context.Players.Add(s);
            }
            context.SaveChanges();



            var itemType = new ItemType[]
            {
                new ItemType{Name="Collectible"},
                new ItemType{Name="Gold"},
                new ItemType{Name="Furniture"},
                new ItemType{Name="Trash"}
            };
            foreach (var s in itemType)
            {
                context.ItemTypes.Add(s);
            }
            context.SaveChanges();


            var items = new Item[]
            {
                new Item{Name="Old Boots", ItemTypeID = 4},
                new Item{Name="Rusted Armor", ItemTypeID = 4},
                new Item{Name="Rusted Weapon", ItemTypeID = 4},
                new Item{Name="Caps", ItemTypeID = 4},
                new Item{Name="Old Collectible Relic", ItemTypeID = 1},
                new Item{Name="Table", ItemTypeID = 3},
                new Item{Name="Chair", ItemTypeID = 3},
                new Item{Name="Candles", ItemTypeID = 3},
            };

            foreach (Item s in items)
            {
                context.Items.Add(s);
            }
            context.SaveChanges();

            var enemyType = new EnemyType[]
            {
                new EnemyType{Name="Small Foe"},
                new EnemyType{Name="Large Foe"},
                new EnemyType{Name="Boss"},
                new EnemyType{Name="Large Boss"},
            };
            foreach (var s in enemyType)
            {
                context.EnemyTypes.Add(s);
            }
            context.SaveChanges();


            var enemy = new Enemy[]
            {
                new Enemy{Name="Goblin", EnemyTypeID=1, isAlive=true},
                new Enemy{Name="Golem", EnemyTypeID=3, isAlive=true},
                new Enemy{Name="Rogue", EnemyTypeID=1, isAlive=true},
                new Enemy{Name="Thief", EnemyTypeID=1, isAlive=true},
                new Enemy{Name="Monster", EnemyTypeID=2, isAlive=true}
            };
            foreach (var s in enemy)
            {
                context.Enemies.Add(s);
            }
            context.SaveChanges();


        }
    }
}
