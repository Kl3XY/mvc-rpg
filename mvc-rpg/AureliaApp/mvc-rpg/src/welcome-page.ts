import { IHttpClient, json } from '@aurelia/fetch-client';
import { newInstanceOf } from '@aurelia/kernel';
import { inject } from 'aurelia';

export interface ISkinType {
    id: number;
    name: string;
}

class Player {
    name: string
    id: number = 0
    isAlive: boolean
    ProfilePicture: string;
    skintype: number;
    hairtype: number;

    testFunc(){
        console.log("Hello! from " + this.name);
    }
}

@inject(newInstanceOf(IHttpClient))
export class WelcomePage {
    public name = 'Player';
    public player: Player[] = [];
    skintypes: ISkinType[] = [
        { id: 0, name: "White" },
        { id: 1, name: "Light Skinned" },
        { id: 2, name: "Brown" },
    ]
    hairtypes: ISkinType[] = [
        { id: 0, name: "Red Long Hair" },
        { id: 1, name: "Brown Short Hair" },
        { id: 2, name: "Brown Afro" },
    ]
    skintype: number;
    hairtype: number;
    constructor(private http: IHttpClient) {
    }


    async create() {
        const form = document.querySelector('form');
        form.addEventListener('submit', this.create);

        const formData = new FormData(form)
        const pB = formData.get("file") as File

        console.log(pB)

        const buff = await pB.arrayBuffer();
        const base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(buff)));
        console.log(base64String)
        
        const newPlayer = new Player();
        newPlayer.name = this.name;
        newPlayer.isAlive = true;
        newPlayer.ProfilePicture = base64String;
        newPlayer.skintype = this.skintype;

        this.http.fetch("https://localhost:7122/Players", {
            method: 'Post',
            body: json(newPlayer),
        })
    }

 

    load() {
        console.log("API Init")
        this.http.fetch("https://localhost:7122/Players")
            .then(rsp => rsp.json())
            .then(json => { this.player = (json as Player[]) })

        
        console.log("API Done!")
    }
}