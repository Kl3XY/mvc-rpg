﻿using mvc_rpg.Models;

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

            var items = new Item[]
            {
                new Item{Name="Old Boots", ItemType = itemType.Trash},
                new Item{Name="Rusted Armor", ItemType = itemType.Trash},
                new Item{Name="Rusted Weapon", ItemType = itemType.Trash},
                new Item{Name="Caps", ItemType = itemType.Trash},
                new Item{Name="Old Collectible Relic", ItemType = itemType.Collectible},
                new Item{Name="Table", ItemType = itemType.Furniture},
                new Item{Name="Chair", ItemType = itemType.Furniture},
                new Item{Name="Candles", ItemType = itemType.Furniture},
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
