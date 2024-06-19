import { customElement, bindable, inject } from 'aurelia';
import template from './draw-item.html';
import { newInstanceOf } from '@aurelia/kernel';
import { IHttpClient } from '@aurelia/fetch-client';
import { Item, Player, ItemType } from './classes'

@customElement({
    name: 'draw-item',
    template
})
@inject(newInstanceOf(IHttpClient))
export class drawItem {
    @bindable item: Item;
    @bindable player: Player;
    @bindable width: number = 64;
    @bindable height: number = 64;

    constructor(private http: IHttpClient) {
        
    } 

    async deleteItem(itemid: number) {
        console.log("Destroyed item id" + itemid.toString());

        await this.http.fetch("https://localhost:7122/PlayerItems/" + this.player.id.toString() + "?itemid=" + itemid.toString(), {
            method: 'Delete'
        })

        window.location.reload();
    }

    async equipItem(itemid: number) {
        console.log("Destroyed item id" + itemid.toString());
        
        await this.http.fetch("https://localhost:7122/PlayerItems/" + this.player.id.toString() + "/Items/Equip/" + itemid.toString(), {
            method: 'Put'
        })

        window.location.reload();
    }
}