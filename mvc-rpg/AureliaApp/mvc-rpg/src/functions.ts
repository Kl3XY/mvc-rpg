import { Item } from './classes'
import { choose } from './profile'
export function nameGen(Item: Item): string {
    let finalName = "";
    switch (Item.rarity) {
        case 0:
            finalName += choose(["", "", "Broken ", "Busted ", "Useless ", "Bare-minimum ", "Dull "])
            switch (Item.itemTypeID) {
                case 1:
                    finalName += choose(["Old Trophy ", "\"Collectible\"", "Boulder "])
                    break;
                case 2:
                    finalName += choose(["Fake Gold", "Rose Gold"])
                    break;
                case 3:
                    finalName += choose(["Chair", "Table", "Desk"])
                    break;
                case 4:
                    finalName += choose(["Bag", "Cloth", "???"])
                    break;
                case 5:
                    finalName += choose(["Wooden ", "Steel ", "Pig Iron "])
                    finalName += choose(["Sword", "Spear", "Hammer", "Trident"])
                    break;
            }
            break;
        case 1:
            finalName += choose(["", "", "Working ", "Functional ", "Okay "])
            switch (Item.itemTypeID) {
                case 1:
                    finalName += choose(["Trophy ", "Trading Cards ", "Video Game Cassette"])
                    break;
                case 2:
                    finalName += choose(["Cat Gold ", "Brass "])
                    break;
                case 3:
                    finalName += choose(["Metal Chair", "Ceramic Plate", "Prof. Desk"])
                    break;
                case 4:
                    finalName += choose(["Bag", "Cloth", "???"])
                    break;
                case 5:
                    finalName += choose(["Golden ", "Fine Steel ", "Cast Iron "])
                    finalName += choose(["Sword", "Spear", "Hammer", "Trident"])
                    break;
            }
            break;
        case 2:
            finalName += choose(["", "", "Good ", "Near Perfect ", "Collectible ", "Rare "])
            switch (Item.itemTypeID) {
                case 1:
                    finalName += choose(["Rough Diamond "])
                    break;
                case 2:
                    finalName += choose(["Impure Gold ", "Golden Tooth "])
                    break;
                case 3:
                    finalName += choose(["Used Throne", "Silverware"])
                    break;
                case 4:
                    finalName += choose(["Bag", "Cloth", "???"])
                    break;
                case 5:
                    finalName += choose(["Titanium ", "Adamantite ", "Diadochos "])
                    finalName += choose(["Sword", "Spear", "Hammer", "Trident"])
                    break;
            }
            break;
        case 3:
            finalName += choose(["", "", "Expert ", "Demonic ", "Masterful "])
            switch (Item.itemTypeID) {
                case 1:
                    finalName += choose(["Chunk of Eridium "])
                    break;
                case 2:
                    finalName += choose(["Gold ", "Golden Watch "])
                    break;
                case 3:
                    finalName += choose(["Throne", " Double Bed"])
                    break;
                case 4:
                    finalName += choose(["Bag", "Cloth", "???"])
                    break;
                case 5:
                    finalName += choose(["Eridian ", "Masternium ", "Damascus "]);
                    finalName += choose(["Sword", "Spear", "Hammer", "Trident"]);
                    if (choose(["", "", "", "of"]) == "of") {
                        finalName += choose([" of Conviction", " of Fighting", " of Rightousness", " of Heroism"]);
                    }
                    break;
            }
            break;
        case 4:
            finalName += choose([
                "Sharpened Axe of the Blood Emperor",
                "Ice Kings Sword",
                "Demonic Possession",
                "Wrathful Blade",
                "Muramasa",
                "Vengeance",
                "Animus"
            ])
            break;
    }
    return finalName;
}