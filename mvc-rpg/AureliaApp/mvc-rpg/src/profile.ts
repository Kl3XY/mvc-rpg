import { IHttpClient, json } from '@aurelia/fetch-client';
import { newInstanceOf } from '@aurelia/kernel';
import { inject } from 'aurelia';

export interface ISkinType {
    id: number;
    name: string;
}

class ItemType {
    name: string
    id: number = 0
}

class Item {
    name: string
    id: number = 0
    itemTypeID: number
    itemType: ItemType
    Attack: number
    rarity: number;
    effectID: number;
}
class Player {
    name: string
    id: number = 0
    health: number
    isAlive: boolean
    ProfilePicture: string;
    skintype: number;
    hairtype: number;
    items: Item[] = [];

}

@inject(newInstanceOf(IHttpClient))
export class Profile {
    public player: Player;
    constructor(private http: IHttpClient) {
    } 

    choose(array: any[]): any {
        return array[Math.floor(Math.random() * array.length)]
    }

    nameGen(Item: Item): string {
        let finalName = "";
        switch (Item.rarity) {
            case 0:
                finalName += this.choose(["", "", "Broken ", "Busted ", "Useless ", "Bare-minimum ", "Dull "])
                switch (Item.itemTypeID) {
                    case 1:
                        finalName += this.choose(["Old Trophy ", "\"Collectible\"", "Boulder "])
                        break;
                    case 2:
                        finalName += this.choose(["Fake Gold", "Rose Gold"])
                        break;
                    case 3:
                        finalName += this.choose(["Chair", "Table", "Desk"])
                        break;
                    case 4:
                        finalName += this.choose(["Bag", "Cloth", "???"])
                        break;
                    case 5:
                        finalName += this.choose(["Wooden ", "Steel ", "Pig Iron "])
                        finalName += this.choose(["Sword", "Spear", "Hammer", "Trident"])
                        break;
                }
                break;
            case 1:
                finalName += this.choose(["", "", "Working ", "Functional ", "Okay "])
                switch (Item.itemTypeID) {
                    case 1:
                        finalName += this.choose(["Trophy ", "Trading Cards ", "Video Game Cassette"])
                        break;
                    case 2:
                        finalName += this.choose(["Cat Gold ", "Brass "])
                        break;
                    case 3:
                        finalName += this.choose(["Metal Chair", "Ceramic Plate", "Prof. Desk"])
                        break;
                    case 4:
                        finalName += this.choose(["Bag", "Cloth", "???"])
                        break;
                    case 5:
                        finalName += this.choose(["Golden ", "Fine Steel ", "Cast Iron "])
                        finalName += this.choose(["Sword", "Spear", "Hammer", "Trident"])
                        break;
                }
                break;
            case 2:
                finalName += this.choose(["", "", "Good ", "Near Perfect ", "Collectible ", "Rare "])
                switch (Item.itemTypeID) {
                    case 1:
                        finalName += this.choose(["Rough Diamond "])
                        break;
                    case 2:
                        finalName += this.choose(["Impure Gold ", "Golden Tooth "])
                        break;
                    case 3:
                        finalName += this.choose(["Used Throne", "Silverware"])
                        break;
                    case 4:
                        finalName += this.choose(["Bag", "Cloth", "???"])
                        break;
                    case 5:
                        finalName += this.choose(["Titanium ", "Adamantite ", "Diadochos "])
                        finalName += this.choose(["Sword", "Spear", "Hammer", "Trident"])
                        break;
                }
                break;
            case 3:
                finalName += this.choose(["", "", "Expert ", "Demonic ", "Masterful "])
                switch (Item.itemTypeID) {
                    case 1:
                        finalName += this.choose(["Chunk of Eridium "])
                        break;
                    case 2:
                        finalName += this.choose(["Gold ", "Golden Watch "])
                        break;
                    case 3:
                        finalName += this.choose(["Throne", " Double Bed"])
                        break;
                    case 4:
                        finalName += this.choose(["Bag", "Cloth", "???"])
                        break;
                    case 5:
                        finalName += this.choose(["Eridian ", "Masternium ", "Damascus "]);
                        finalName += this.choose(["Sword", "Spear", "Hammer", "Trident"]);
                        if (this.choose(["", "", "", "of"]) == "of") {
                            finalName += this.choose([" of Conviction", " of Fighting", " of Rightousness", " of Heroism"]);
                        }
                        break;
                }
                break;
            case 4:
                finalName += this.choose([
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


    async exampleFill() {
        let newItem = new Item();
        newItem.effectID = 0;
        newItem.rarity = Math.floor(Math.random() * 4);
        newItem.itemTypeID = Math.floor(Math.random() * 5) + 1;
        newItem.Attack = Math.floor((Math.random()+1) * 24) * (newItem.rarity+1);

        newItem.name = this.nameGen(newItem);

        await this.http.fetch("https://localhost:7122/Items", {
            method: 'Post',
            body: json(newItem),
        })
            .then(rsp => rsp.json())
            .then(json => { newItem = json as Item })

        await this.http.fetch("https://localhost:7122/PlayerItems/" + this.player.id.toString() + "?itemid=" + newItem.id.toString(), {
            method: 'Put',
            body: json(newItem),
        })

        this.load();
    }

    load() {
        this.player = null;
        console.log("API Init")
        this.http.fetch("https://localhost:7122/PlayerItems/1")
            .then(rsp => rsp.json())
            .then(json => { this.player = (json as Player); console.log(json) })

        
        console.log("API Done!")
    }
}