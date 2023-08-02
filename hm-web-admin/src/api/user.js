import request from '@/utils/request'

//注册接口
export const userRegisterService = ({ username, password, repassword }) => {
  return request.post('/api/reg', { username, password, repassword })
}
//get请求方式，自己的c#api
// export const userLoginService = ({ username, password }) => {
//   return request.get(
//     `/api/Login/GetToken?name=${username}&password=${password}`
//   )
// }

//登录接口
export const userLoginService = ({ username, password }) =>
  request.post('/api/login', { username, password })

  //获取用户基本信息
  export const userGetInfoService =() => request.get('/my/userinfo')
