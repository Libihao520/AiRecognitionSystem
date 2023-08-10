import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '../view/index/LoginPage.vue'
import DeskTop from '../view/index/DeskTop.vue'
import RootPage from '../view/index/RootPage.vue'
import MenuManager from '../view/admin/menu/MenuManager.vue'
import MenuManager2 from '../view/admin/menu/MenuManager2.vue'
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
                    },
                    {
                        name: "", path: "/MenuManager", component: MenuManager
                    },
                    {
                        name: "", path: "/MenuManager2", component: MenuManager2
                    }
                ]
        }
    ]
})
export default router;
