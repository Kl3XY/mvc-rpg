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

            var defaultImage = ImageToByteArray(System.Drawing.Image.FromFile("..\\mvc-rpg\\wwwroot\\Default\\img_defaultPicture.jpg"));

            var players = new Player[]
            {
                new Player{Name="Kl3XY", Health = 100, isAlive=true, skinType = skinType.white, hairType = hairType.Afro }, 
                new Player{Name="Yooo", Health = 100, isAlive=true, skinType = skinType.white, hairType = hairType.Afro}, 
                new Player{Name="Doomer", Health = 100, isAlive=true, skinType = skinType.white, hairType = hairType.Afro}, 
                new Player{Name="Ner", Health = 100, isAlive=true, skinType = skinType.white, hairType = hairType.Afro}, 
                new Player{Name="Bro", Health = 100, isAlive=true, skinType = skinType.white, hairType = hairType.Afro},
                new Player{Name="New Bro", Health = 100, isAlive=true, skinType = skinType.white, hairType = hairType.Afro},
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
                new ItemType{Name="Trash"},
                new ItemType{Name="Weapon"},
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
                new Item{Name="Sharpened Axe of the Blood Emperor", ItemTypeID = 5, Attack = 403, rarity = rarity.legendary, effectID = 1},
                new Item{Name="Frozen Kings Blade", ItemTypeID = 5, Attack = 320, rarity = rarity.legendary, effectID = 2},
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
                new Enemy{Name="Goblin", EnemyTypeID=1, isAlive=true, DroppableItemID = 8},
                new Enemy{Name="Golem", EnemyTypeID=3, isAlive=true, DroppableItemID = 8},
                new Enemy{Name="Rogue", EnemyTypeID=1, isAlive=true, DroppableItemID = 8},
                new Enemy{Name="Thief", EnemyTypeID=1, isAlive=true, DroppableItemID = 8},
                new Enemy{Name="Monster", EnemyTypeID=2, isAlive=true, DroppableItemID = 8}
            };
            foreach (var s in enemy)
            {
                context.Enemies.Add(s);
            }
            context.SaveChanges();


        }
    }
}
