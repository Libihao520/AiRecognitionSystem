import { createApp } from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import './assets/css/global.scss'
import router from './router/index'
import stotre from './vuex/index'

createApp(App)
    .use(ElementPlus)
    .use(router)
    .use(stotre)
    .mount('#app')
