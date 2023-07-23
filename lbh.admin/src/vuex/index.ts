import { createStore } from 'vuex'
const store = createStore({
    //状态变量
    state() {
        return {
            Token: localStorage["token"],
            NickName: localStorage["nickname"]
        }
    },
    //方法
    mutations: {
        SettingNickName(state: any, NickName) {
            state.NickName = NickName
        },
        SettingToKen(state: any, Token) {
            state.Token = Token
        }
    }
})
export default store