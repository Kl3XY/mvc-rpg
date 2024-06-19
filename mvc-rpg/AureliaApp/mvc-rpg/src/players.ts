import { IHttpClient, json } from '@aurelia/fetch-client';
import { newInstanceOf } from '@aurelia/kernel';
import { inject, IEventAggregator, resolve } from 'aurelia';
import { Player } from './classes'


@inject(newInstanceOf(IHttpClient))
export class Players {
    public name = 'Player';
    public player: Player[] = [];
    readonly ea: IEventAggregator = resolve(IEventAggregator)

    constructor(private http: IHttpClient) {

    }
    

    search() {
        this.player = [];
        console.log("API Init")
        let search: string;
        if (this.name != '') {
            search = "https://localhost:7122/Players/Search/" + this.name;
        } else {
            search = "https://localhost:7122/Players";
        }
        console.log(search);
        this.http.fetch(search)
            .then(rsp => rsp.json())
            .then(json => { this.player = (json as Player[]); console.log(this.player) })



        console.log("API Done!")
    }

    load() {
        console.log("API Init")
        this.http.fetch("https://localhost:7122/Players")
            .then(rsp => rsp.json())
            .then(json => { this.player = (json as Player[]) })

        
        console.log("API Done!")
    }
}