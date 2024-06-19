import { nameGen } from './functions'
export class Item {
    name: string
    id: number = 0
    itemTypeID: number
    itemType: ItemType
    attack: number
    rarity: number;
    effectID: number;

    constructor() {
        this.effectID = 0;
        this.rarity = 0;
        this.itemTypeID = Math.floor(Math.random() * 5) + 1;
        while (Math.random() * 100 < 40) {
            this.rarity++;
            if (this.rarity == 4) {
                break;
            }
        }

        this.attack = Math.floor((Math.random() + 1) * 24) * (this.rarity + 1);
        this.name = nameGen(this);

        if (this.rarity == 4) { this.itemTypeID = 5; }
    }
}
export interface ISkinType {
    id: number;
    name: string;
}

export class ItemType {
    name: string
    id: number = 0
}

export class Player {
    name: string
    id: number = 0
    health: number
    isAlive: boolean
    ProfilePicture: string;
    skintype: number;
    hairtype: number;
    equippedItemID: number;
    equippedItem: Item;
    items: Item[] = [];

}