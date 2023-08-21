# 杭州林和草

# 启动文件
 python server.py

# 模型
训练好的模型位置在weight
ysgl.pt :光缆运输识别
ysmt.pt :木材运输识别

# 模型选择
server.py第15行  ->  52行   ->  torch\hub\shlhc_bch_master\hubconf.py 61行

# 车牌识别
server.py中的👇
url = "http://localhost:8001/getPlateNo"
r = requests.post(url, files=files)