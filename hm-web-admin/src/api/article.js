import request from '@/utils/request'
//获取文章分类
export const artGetChannelsService = () => request.get('/my/cate/list')
