import { customElement, bindable } from 'aurelia';
import template from './draw-profile.html';
class Player {
    name: string
    id: number = 0
    isAlive: boolean
    ProfilePicture: string;
}

@customElement({
    name: 'draw-profile',
    template
})
export class DrawProfile {
    @bindable profile: Player;
    @bindable width: number = 64;
    @bindable height: number = 64;

}