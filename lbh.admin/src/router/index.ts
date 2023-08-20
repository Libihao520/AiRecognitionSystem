import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '../view/index/LoginPage.vue'
import DeskTop from '../view/index/DeskTop.vue'
import RootPage from '../view/index/RootPage.vue'
import MenuManager from '../view/admin/menu/MenuManager.vue'
import MenuManager2 from '../view/admin/menu/MenuManager2.vue'
import UpdatePhoto  from '../view/index/UpdatePhoto.vue'
import Tool from '../global'
import { UserInfo } from '../../src/view/index/class/UserInfo'
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
                    },
                    {
                        name: "", path: "/UpdatePhoto", component: UpdatePhoto
                    }
                ]
        }
    ]
})
const toolObj = new Tool()
router.beforeEach((to, form) => {
    if (localStorage["nickname"] != undefined) {
        const user: UserInfo = JSON.parse(new Tool().FormatToken(localStorage["token"]))
        const expDate = toolObj.FormatDate(user.exp)
        const currDate = toolObj.GetDate()
        if (to.path == "/login") {
            if (expDate >= currDate) {
                return { path: "/desktop" }
            } else {
                toolObj.ClearLocalStorage()
            }
        } else {
            if (expDate < currDate) {
                toolObj.ClearLocalStorage()
                return { path: "/login" }
            }
        }
    } else {
        //避免无限重定向，因此要做个判断
        if (to.path !== "/login") {
            return { path: "/login" }
        }
    }
})
export default router;
