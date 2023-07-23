import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '../view/index/LoginPage.vue'
import DeskTop from '../view/index/DeskTop.vue'
import RootPage from '../view/index/RootPage.vue'
const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: '/', redirect: '/login' },
        { path: '/login', component: LoginPage },
        { path: '/DeskTop', component: DeskTop },
        {path:'/RootPage',component:RootPage}
    ]
})
export default router;
