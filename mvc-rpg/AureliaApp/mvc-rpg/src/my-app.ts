import { WelcomePage } from './welcome-page'
import { Players } from './players'
import { Profile } from './profile'

export class MyApp {
    static routes = [
        {
            path: '',
            component: WelcomePage,
            title: 'Player Creation'
        },
        {
            path: '/welcome-page',
            component: WelcomePage,
            title: 'Player Creation'
        },
        {
            path: '/players',
            component: Players,
            title: 'All Players'
        },
        {
            path: '/profile/:id',
            component: Profile,
            title: 'Player'
        },
    ]
}
