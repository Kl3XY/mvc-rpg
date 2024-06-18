import { IHttpClient, json } from '@aurelia/fetch-client';
import { newInstanceOf } from '@aurelia/kernel';
import { inject } from 'aurelia';
class Player {
    name: string
    id: number = 0
    isAlive: boolean
    ProfilePicture: string;
}

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
    id: number = 0
    isAlive: boolean
    EnemyTypeID: number
}


@inject(newInstanceOf(IHttpClient))
export class game {
    public selectedPlayer: Player
    public selectedEnemy: Enemy
    public combatLog: Grave[] = []

    constructor(private http: IHttpClient) {

    }

    async selectPlayer(): Promise<Player> {
        let players: Player[];
        let selPlayer: Player;

        await this.http.fetch("https://localhost:7122/Players/")
            .then(rsp => {
                if (rsp.status == 404) { return; } else { return rsp.json() }
            })
            .then(json => { players = json as Player[]; })

        while (players.length > 0 || selPlayer != undefined) {
            let ind = this.randomRange(0, players.length - 1)
            let val = players[ind]
            if (val.isAlive == true) {
                selPlayer = val;
                break;
            } else {
                const index = players.indexOf(val);
                if (index > -1) { // only splice array when item is found
                    players.splice(index, 1); // 2nd parameter means remove one item only
                }
            }
        }

        return selPlayer;
    }

    async killPlayer(player: Player){
        await this.http.fetch("https://localhost:7122/Players/" + player.id, {
            method: 'PUT',
            body: json(player)
        })
    }

    async killEnemies(enemy: Enemy) {
        await this.http.fetch("https://localhost:7122/Enemies/" + enemy.id, {
            method: 'PUT',
            body: json(enemy)
        })
    }

    async selectEnemy(): Promise<Enemy> {
        let enemies: Enemy[];
        let selEnemy: Enemy;

        await this.http.fetch("https://localhost:7122/Enemies/")
            .then(rsp => {
                if (rsp.status == 404) { return; } else { return rsp.json() }
            })
            .then(json => { enemies = json as Enemy[]; })
            
        while (enemies.length > 0 || selEnemy != undefined) {
            let ind = this.randomRange(0, enemies.length - 1)
            let val = enemies[ind]
            if (val.isAlive == true) {
                selEnemy = val;
                break;
            } else {
                const index = enemies.indexOf(val);
                if (index > -1) { // only splice array when item is found
                    enemies.splice(index, 1); // 2nd parameter means remove one item only
                }
            }
        }

        return selEnemy;
    }

    randomRange(min: number, max: number): number {
        return Math.floor(Math.random() * max + min);
    }

    createGrave(player: Player, enemy: Enemy, killedBy: number) {
        let newGrave = new Grave();
        newGrave.playerID = player.id;
        newGrave.enemyID = enemy.id;
        newGrave.killedBy = killedBy;
        newGrave.dateTime = new Date();

        this.http.fetch("https://localhost:7122/Graves", {
            method: 'POST',
            body: json(newGrave)
        })
    }


    async load() {
        this.selectedPlayer = await this.selectPlayer();
        this.selectedEnemy = await this.selectEnemy();
        await this.http.fetch("https://localhost:7122/Graves")
            .then(result => result.json())
            .then(json => this.combatLog = json as Grave[])

        for (let inp in this.combatLog) {
            await this.http.fetch("https://localhost:7122/Enemies/" + this.combatLog[inp].enemyID.toString())
                .then(result => result.json())
                .then(json => this.combatLog[inp].enemy = json as Enemy)
            await this.http.fetch("https://localhost:7122/Players/" + this.combatLog[inp].playerID.toString())
                .then(result => result.json())
                .then(json => this.combatLog[inp].player = json as Player)
        }

        this.combatLog = this.combatLog.reverse();
        let random = Math.floor(Math.random() * 2 + 1);
        console.log(random)
        if (this.selectedEnemy != undefined && this.selectedPlayer != undefined) {
            if (random == 2) {
                this.selectedPlayer.isAlive = false;
                this.killPlayer(this.selectedPlayer);
                this.createGrave(this.selectedPlayer, this.selectedEnemy, random - 1);
            } else {
                this.selectedEnemy.isAlive = false;
                this.killEnemies(this.selectedEnemy);
                this.createGrave(this.selectedPlayer, this.selectedEnemy, random - 1);
            }
        }
    }
}