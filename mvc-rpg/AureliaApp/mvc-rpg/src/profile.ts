import { IHttpClient, json } from '@aurelia/fetch-client';
import { newInstanceOf } from '@aurelia/kernel';
import { inject, resolve, ICustomElementViewModel } from 'aurelia';
import { IRouter } from '@aurelia/router';
import { Item, Player } from './classes'


export function choose(array: any[]): any {
    return array[Math.floor(Math.random() * array.length)]
}

@inject(newInstanceOf(IHttpClient))
export class Profile implements ICustomElementViewModel {
    public player: Player;
    public id = 0;
    readonly router: IRouter = resolve(IRouter);
    constructor(private http: IHttpClient) {
    } 


    public async deleteItem(itemid: number) {
        console.log("Destroyed item id" + itemid.toString());

        await this.http.fetch("https://localhost:7122/PlayerItems/" + this.player.id.toString() + "?itemid=" + itemid.toString(), {
            method: 'Delete'
        })

        
    }

    async exampleFill() {
        let newItem = new Item();

        console.log(newItem);

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

        this.player.items.push(newItem);
    }


    load(parameters) {
        console.log(parameters)
        this.id = parameters.id;

        this.player = null;

        console.log("API Init")
        this.http.fetch("https://localhost:7122/PlayerItems/" + parameters.id.toString())
            .then(rsp => rsp.json())
            .then(json => { this.player = (json as Player); this.player.equippedItem = this.player.items.find(m => m.id == this.player.equippedItemID); })

    }
}