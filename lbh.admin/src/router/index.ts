import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '../view/index/LoginPage.vue'
import DeskTop from '../view/index/DeskTop.vue'
import RootPage from '../view/index/RootPage.vue'
const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: '/', redirect: '/login' },
        { path: '/login', component: LoginPage },
        {
            path: '/RootPage', component: RootPage,
            children:
                [
                    {
                        name: "", path: "/DeskTop", component: DeskTop
                    }
                ]
        }
    ]
})
export default router;
