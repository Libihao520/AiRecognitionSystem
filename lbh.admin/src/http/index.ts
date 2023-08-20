import axios from "axios";
//需要用拦截器的用instance 不用的用上面的axios
import instance from './filter'
const http = "http://localhost:5157/api"

//获取token方法
export const getToken = (name: string, password: string) => {
    return instance.get(http + "/Login/GetToken?name=" + name + "&Password=" + password);
}
export const getfzzjdesktop = () => {
    //当后端的controller加了权限验证要加上这句，请求带上token
    instance.defaults.headers.common['Authorization']="Bearer "+localStorage["token"];
    return instance.get(http + "/FzZj/GetDesktop");
}
export const getfzzjtable = () => {
    instance.defaults.headers.common['Authorization']="Bearer "+localStorage["token"];
    return instance.get(http + "/FzZj/GetTable");
}
export const addfzzjtable = (data) => {
    instance.defaults.headers.common['Authorization']="Bearer "+localStorage["token"];
    return instance.post(http + "/FzZj/AddTable",data);
}
export const putfzzjtable = (data) => {
    instance.defaults.headers.common['Authorization']="Bearer "+localStorage["token"];
    return instance.put(http + "/FzZj/putTable",data);
}
export const delfzzjtable = (id) => {
    instance.defaults.headers.common['Authorization']="Bearer "+localStorage["token"];
    return instance.delete(http + "/FzZj/delTable",{params:{id}});
}
//上传图片
export const PutPhotoService = (photo) =>
{
    instance.defaults.headers.common['Authorization']="Bearer "+localStorage["token"];
    return instance.put(http + "/Photo/PutPhoto",{photo});
}
