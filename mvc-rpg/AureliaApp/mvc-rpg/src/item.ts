import { customElement, bindable } from 'aurelia';
import template from './item.html';
import { IHttpClient} from '@aurelia/fetch-client';

class Item {
    name: string
    id: number = 0
    itemTypeID: number
    itemType: ItemType
    attack: number
    rarity: number;
    effectID: number;
}

class ItemType {
    name: string
    id: number = 0
}

@customElement({
    name: 'item',
    template
})
export class item {
    @bindable item: Item;
    @bindable width: number = 64;
    @bindable height: number = 64;

}