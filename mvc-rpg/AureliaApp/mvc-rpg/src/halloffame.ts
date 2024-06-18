import { IHttpClient, json } from '@aurelia/fetch-client';
import { newInstanceOf } from '@aurelia/kernel';
import { inject } from 'aurelia';
import * as _ from 'lodash';
import { Players } from './players';

class Grave {
    id: number
    enemyID: number
    playerID: number
    killedBy: number
    dateTime: Date
}

class Group {
    key: string
    player: Players
    values: Grave[]
}

@inject(newInstanceOf(IHttpClient))
export class halloffame {
    public grouped: Group[] = [];

    constructor(private http: IHttpClient) {

    }

    async group(array: Grave[]) {
        this.grouped = [];
        let list = _.groupBy(array, 'playerID')
        for (let inp in list) {
            const newGroup = new Group();
            newGroup.key = inp.toString();
            await this.http.fetch("https://localhost:7122/Players/" + inp.toString())
                .then(rsp => rsp.json())
                .then(json => { newGroup.player = json as Players; })
            newGroup.values = list[inp.toString()];
            this.grouped.push(newGroup);
        }

        console.log(this.grouped);
    }

    load() {
        console.log("API Init")
        this.http.fetch("https://localhost:7122/Graves/HallOfFame")
            .then(rsp => rsp.json())
            .then(json => { this.group(json as Grave[]); })
    }
}