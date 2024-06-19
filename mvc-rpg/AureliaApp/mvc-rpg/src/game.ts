import { IHttpClient, json } from '@aurelia/fetch-client';
import { newInstanceOf } from '@aurelia/kernel';
import { inject } from 'aurelia';
import { Player, ItemType } from './classes'
import { Item } from './classes'
import { choose } from './profile'

class Grave {
    id: number
    enemyID: number
    enemy: Enemy
    playerID: number
    player: Player
    killedBy: number
    dateTime: Date
}


class Enemy {
    name: string
    health: number
    attack: number
    id: number = 0
    isAlive: boolean
    EnemyTypeID: number

    constructor(difficulty: number) {
        let finalName = choose(["", "", "", "", "", "Great ", "Enourmous "])
        finalName += choose(["Goblin", "Skeleton", "Ogre", "Troll", "Undead", "Zombie"])
        finalName += choose(["", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", " of Kings Retreat", " of Ridora"])

        this.name = finalName;
        this.health = (Math.floor((Math.random() * 50) + 10) * difficulty);
        this.attack = 10;
    }
}


@inject(newInstanceOf(IHttpClient))
export class game {
    public selectedPlayer: Player
    public Healing = 3;
    public droppedItems: Item[] = []
    public selectedEnemy: Enemy
    public combatLog: Grave[] = []
    public difficulty = 1;

    async checkIfEnemyAlive() {
        if (this.selectedEnemy.health <= 0) {
            this.selectedEnemy.isAlive = false;

            //Mark in database as dead

            this.difficulty++;
            let i = 0;
            while (i < 3) {
                let newItem = new Item()
                this.droppedItems.push(newItem);

                await this.http.fetch("https://localhost:7122/Items", {
                    method: 'Post',
                    body: json(newItem),
                })
                    .then(rsp => rsp.json())
                    .then(json => { newItem = json as Item })

                await this.http.fetch("https://localhost:7122/PlayerItems/" + this.selectedPlayer.id.toString() + "?itemid=" + newItem.id.toString(), {
                    method: 'Put',
                    body: json(newItem),
                })

                i++;
            }
            this.selectedEnemy = new Enemy(this.difficulty);
        }
    }

    playerActionAttack() {
        if (this.selectedPlayer.equippedItem == undefined) {
            this.selectedEnemy.health -= 10;
        } else {
            this.selectedEnemy.health -= (this.selectedPlayer.equippedItem as Item).attack;
        }
        this.checkIfEnemyAlive();
        this.selectedPlayer.health -= Math.floor(this.selectedEnemy.attack * (Math.random() + 0.25));
        if (this.difficulty % 10 == 0) {
            this.Healing++;
        }
    }

    playerActionHeal() {
        this.selectedEnemy.health = 100;
    }

    constructor(private http: IHttpClient) {

    }

    async load() {
        this.selectedEnemy = new Enemy(this.difficulty);

        await this.http.fetch("https://localhost:7122/PlayerItems")
            .then(rsp => rsp.json())
            .then(json => { this.selectedPlayer = choose(json as Player[]); this.selectedPlayer.equippedItem = this.selectedPlayer.items.find(m => m.id == this.selectedPlayer.equippedItemID); })

    }
}