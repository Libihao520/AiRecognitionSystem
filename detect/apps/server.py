from fastapi import FastAPI, Request, Form, File, UploadFile
from fastapi.templating import Jinja2Templates
from pydantic import BaseModel
from typing import List, Optional

import cv2
import numpy as np
from PIL import ImageFont, ImageDraw, Image
import requests
import torch
import base64
import random
import json
import shutil
import os

app = FastAPI()
templates = Jinja2Templates(directory='templates')
model_selection_options = ['皮卡丘','光缆运输', '木头运输', '疫木识别']
# set up model cache
model_dict = {model_name: None for model_name in model_selection_options}

colors = [tuple([random.randint(0, 255) for _ in range(3)])
          for _ in range(100)]  # for bbox plotting

##############################################
# -------------GET Request Routes--------------
##############################################


@app.get("/")
def home(request: Request):
    ''' Returns html jinja2 template render for home page form
    '''
    return templates.TemplateResponse('home.html', {
        "request": request,
        "model_selection_options": model_selection_options,
    })


@app.get("/drag_and_drop_detect")
def drag_and_drop_detect(request: Request):
    return templates.TemplateResponse('drag_and_drop_detect.html',
                                      {"request": request,
                                       "model_selection_options": model_selection_options,
                                       })


##############################################
# ------------POST Request Routes--------------
##############################################
@app.post("/")
def detect_with_server_side_rendering(request: Request,
                                      file_list: List[UploadFile] = File(...),
                                      model_name: str = Form(...),
                                      img_size: int = Form(640)):
    if model_dict[model_name] is None:
        # model_dict[model_name] = torch.hub.load('ultralytics/yolov5', model_name, pretrained=True)
        path = 'torch/hub/shlhc_bch_master'
        model_dict[model_name] = torch.hub.load(
            path, model_name, source='local')
    img_batch = [cv2.imdecode(np.fromstring(file.file.read(), np.uint8), cv2.IMREAD_COLOR)
                 for file in file_list]

    # create a copy that corrects for cv2.imdecode generating BGR images instead of RGB
    # using cvtColor instead of [...,::-1] to keep array contiguous in RAM
    img_batch_rgb = [cv2.cvtColor(img, cv2.COLOR_BGR2RGB) for img in img_batch]

    results = model_dict[model_name](img_batch_rgb, size=img_size)

    json_results = results_to_json(results, model_dict[model_name])

    img_str_list = []
    # plot bboxes on the image
    for img, bbox_list in zip(img_batch, json_results):
        for bbox in bbox_list:
            label = f'{bbox["class_name"]} {bbox["confidence"]:.2f}'
            plot_one_box_cv(bbox['bbox'], img, label=label,
                         color=colors[int(bbox['class'])], line_thickness=3)

        img_str_list.append(base64EncodeImage(img))

    encoded_json_results = str(json_results).replace(
        "'", r"\'").replace('"', r'\"')
    if encoded_json_results != '[[]]':
        # 对数组的图片格式进行编码
        success, encoded_image = cv2.imencode(".jpg", img)
        # 将数组转为bytes
        f = encoded_image.tobytes()
        files = {'file': f}
        url = "http://127.0.0.1:8000/getPlateNo"
        # 将图片post提交
        r = requests.post(url, files=files)

        results2_code = r.json()['code']
        results2_msg = r.json()['msg']
        results_class_name = json_results[0][0]['class_name']
        results_score = json_results[0][0]['confidence']
        results_score_end = round(results_score, 2)
        if r.json()['result'] != []:
            results2_plateNo = r.json()['result'][0]['plateNo']
            results2_score = r.json()['result'][0]['score']
            results2_score_end = round(results2_score, 2)
            Emp = {
                "code": results2_code,
                "success": 'true',
                "data": {
                    "cargos": [
                        {
                            "name": results_class_name,
                            "score": results_score_end
                        }
                    ],
                    "trucks": [
                        {
                            "plateNo": results2_plateNo,
                            "score": results2_score_end
                        }
                    ]
                }
            }
        if r.json()['result'] == []:
            results2_plateNo = None
            results2_score_end = None
            Emp = {
                "code": results2_code,
                "success": 'true',
                "data": {
                    "cargos": [
                        {
                            "name": results_class_name,
                            "score": results_score_end
                        }
                    ],
                    "trucks": [
                    ]
                }
            }
    if encoded_json_results == '[[]]':
        Emp = {
            "code": 0,
            "success": 'false',
            "data": {
                "cargos": [
                ],
                "trucks": [
                ]
            }
        }
    Emp_json = json.dumps(Emp)
    encoded_json_results_end = str(Emp_json).replace(
        "'", r"\'").replace('"', r'\"')
    return templates.TemplateResponse('show_results.html', {
        'request': request,
        'bbox_image_data_zipped': zip(img_str_list, json_results),
        'bbox_data_str': encoded_json_results_end,
    })


@app.post("/detect")
def detect_via_api(request: Request,
                   file_list: List[UploadFile] = File(...),
                   model_name: str = Form(...),
                   img_size: Optional[int] = Form(640),
                   download_image: Optional[bool] = Form(False)):
    if model_dict[model_name] is None:
        path = 'torch/hub/shlhc_bch_master'
        model_dict[model_name] = torch.hub.load(path, model_name, source='local')
    img_batch = [cv2.imdecode(np.fromstring(file.file.read(), np.uint8), cv2.IMREAD_COLOR)
                 for file in file_list]        
    img_batch_rgb = [cv2.cvtColor(img, cv2.COLOR_BGR2RGB) for img in img_batch]
    results = model_dict[model_name](img_batch_rgb, size=img_size)
    json_results = results_to_json(results, model_dict[model_name])
    if (model_name == '疫木识别') or (model_name == '皮卡丘') :
        # return json.loads(json_results)
        return_ymsbemp={'code':200,'success':True,'result':[],}
        if json_results[0] !=[]:
            return_ymsbemp['result']=json_results[0]
            if download_image:
                json_results = results_bbox_json(results, model_dict[model_name])
                imgbox = img_batch[0]
                for bbox in json_results[0]:
                    # if bbox["class_name"] =="1":
                    #     bbox["class_name"] ="病树"
                    # elif bbox["class_name"] =="4":
                    #     bbox["class_name"] ="死树"
                    label = f'{bbox["class_name"]} {bbox["confidence"]:.2f}'
                    # imgbox = plot_one_box(bbox['bbox'], imgbox, label=label, 
                    #         color=colors[int(bbox['class'])], line_thickness=3)   
                    plot_one_box_cv(bbox['bbox'], imgbox, label=label,
                         color=colors[int(bbox['class'])], line_thickness=3)        
                return_ymsbemp['image_base64']="data:image/jpeg;base64,"+base64EncodeImage(imgbox,".jpg")
            return return_ymsbemp
        return {'code':500,'success':False}
    # 判断存放照片的文件夹是否存在，不在创建
    if not os.path.exists("./tpimages/"):
        os.mkdir("./tpimages/")
    return_emp={'code':200,'success':True,'data':[]}
    return_emp_no={'code':500,'success':False,'data':[]}
    if json_results[0] !=[]:
        for j,screenshot_json in enumerate(json_results[0]):
            roi = screenshot(img_batch[0],screenshot_json)
            path = "./tpimages/"+str(j)+".png"
            if cv2.imwrite(path, roi) == False:
                return"保存图片失败"
            img = open(path,'rb')
            files = {'file': img}
            url = "http://plate:8000/getPlateNo"
            r = requests.request("POST",url, files=files)
            if r.ok != True:
                return "Call license plate recognition exception"
            emp = return_json(json_results[0][j],r)
            if emp != None:
                return_emp['data'].append(emp)
        img.close()
        shutil.rmtree("./tpimages/")
        
        if return_emp['data']==[]:
            return return_emp_no
        if download_image:
            json_results = results_bbox_json(results, model_dict[model_name])
            imgbox = img_batch[0]
            for bbox in json_results[0]:
                # if bbox["class_name"] =="smzp":
                #     bbox["class_name"] ="松木制品"
                # elif bbox["class_name"] =="ymys":
                #     bbox["class_name"] ="疫木运输"
                # elif bbox["class_name"] =="glys":
                #     bbox["class_name"] ="光缆运输"
                label = f'{bbox["class_name"]} {bbox["confidence"]:.2f}'
                # imgbox = plot_one_box(bbox['bbox'], imgbox, label=label, 
                #         color=colors[int(bbox['class'])], line_thickness=3)     
                plot_one_box_cv(bbox['bbox'], imgbox, label=label,
                         color=colors[int(bbox['class'])], line_thickness=3)                
            return_emp['image_base64']="data:image/jpeg;base64,"+base64EncodeImage(imgbox,".jpg")
        return return_emp
    return return_emp_no 
# ---------------DEF-------------------------   
def results_to_json(results, model):
    ''' Converts yolo model output to json (list of list of dicts)'''
    return [
        [
            {
                "class": int(pred[5]),
                "class_name": model.model.names[int(pred[5])],
                # convert bbox results to int from float
                "bbox": [float(x) for x in pred[:4].tolist()],
                "confidence": float(pred[4]),
            }
            for pred in result
        ]
        for result in results.xywhn
    ]

def results_bbox_json(results, model):
    return [
        [
            {
                "class": int(pred[5]),
                "class_name": model.model.names[int(pred[5])],
                "bbox": [float(x) for x in pred[:4].tolist()],
                "confidence": float(pred[4]),
            }
            for pred in result
        ]
        for result in results.xyxy
    ]

def plot_one_box(x, im, color=(128, 128, 128), label=None, line_thickness=3):
    assert im.data.contiguous, 'Image not contiguous. Apply np.ascontiguousarray(im) to plot_on_box() input image.'
    tl = line_thickness or round(
        0.002 * (im.shape[0] + im.shape[1]) / 2) + 1  # line/font thickness
    c1, c2 = (int(x[0]), int(x[1])), (int(x[2]), int(x[3]))
    cv2.rectangle(im, c1, c2, color, thickness=tl, lineType=cv2.LINE_AA)
    if label:
        tf = max(tl - 1, 1)  # font thickness
        t_size = cv2.getTextSize(label, 0, fontScale=tl / 3, thickness=tf)[0]
        font_size = t_size[1]
        font = ImageFont.truetype('./msyh.ttc', font_size)
        t_size = font.getsize(label)
        c2 = c1[0] + t_size[0], c1[1] - t_size[1]
        cv2.rectangle(im, c1, c2, color, -1, cv2.LINE_AA)  # filled
        img_PIL = Image.fromarray(cv2.cvtColor(im, cv2.COLOR_BGR2RGB))
        draw = ImageDraw.Draw(img_PIL)
        draw.text((c1[0], c2[1] - 2), label, fill=(255, 255, 255), font=font)
        cv2.cvtColor(np.array(img_PIL), cv2.COLOR_RGB2BGR) 
        return cv2.cvtColor(np.array(img_PIL), cv2.COLOR_RGB2BGR)

def plot_one_box_cv(x, im, color=(128, 128, 128), label=None, line_thickness=3):
    assert im.data.contiguous, 'Image not contiguous. Apply np.ascontiguousarray(im) to plot_on_box() input image.'
    tl = line_thickness or round(0.002 * (im.shape[0] + im.shape[1]) / 2) + 1  # line/font thickness
    c1, c2 = (int(x[0]), int(x[1])), (int(x[2]), int(x[3]))
    cv2.rectangle(im, c1, c2, color, thickness=tl, lineType=cv2.LINE_AA)
    if label:
        tf = max(tl - 1, 1)  
        t_size = cv2.getTextSize(label, 0, fontScale=tl / 3, thickness=tf)[0]
        c2 = c1[0] + t_size[0], c1[1] - t_size[1] - 3
        cv2.rectangle(im, c1, c2, color, -1, cv2.LINE_AA)  
        cv2.putText(im, label, (c1[0], c1[1] - 2), 0, tl / 3, [225, 255, 255], thickness=tf, lineType=cv2.LINE_AA)

def base64EncodeImage(img,type):
    ''' Takes an input image and returns a base64 encoded string representation of that image (jpg format)'''
    _, im_arr = cv2.imencode(type, img)
    im_b64 = base64.b64encode(im_arr.tobytes()).decode('utf-8')

    return im_b64

def screenshot(img,json_results):
    # 根据传进来的四个坐标，对传进来的原图进行截图
    w = img.shape[1] # 原始图片resize
    h = img.shape[0]
    img = cv2.resize(img,(w,h),interpolation = cv2.INTER_CUBIC) 
    x_center = w * float(json_results['bbox'][0])       # aa[1]左上点的x坐标  
    y_center = h * float(json_results['bbox'][1])       # aa[2]左上点的y坐标
    width = int(w*float(json_results['bbox'][2]))       # aa[3]图片width
    height = int(h*float(json_results['bbox'][3]))      # aa[4]图片height
    lefttopx = int(x_center-width/2.0)
    lefttopy = int(y_center-height/2.0)
    roi = img[lefttopy+1:lefttopy+height+3,lefttopx+1:lefttopx+width+1]
    return roi

def return_json(json_results,r):
    if r.json()['result'] != []:
        plateno = r.json()['result'][0]['plateNo']
        score = round(r.json()['result'][0]['score'],2)
    else:
        plateno = "未识别出车牌"
        score = "0"
    Emp={
            "cargos":
                {
                    "name": json_results['class_name'],
                    "score": round(json_results['confidence'], 2),
                    "bbox":json_results['bbox']
                },  
            "trucks":
                {
                    "plateNo": plateno,
                    "score": score
                }          
         }
    return Emp
if __name__ == '__main__':
    import uvicorn
    import argparse
    parser = argparse.ArgumentParser()
    parser.add_argument('--host', default='0.0.0.0')
    parser.add_argument('--port', default=8005)
    parser.add_argument('--precache-models', action='store_true',
                        help='Pre-cache all models in memory upon initialization, otherwise dynamically caches models')
    opt = parser.parse_args()

    if opt.precache_models:
        # model_dict = {model_name: torch.hub.load('ultralytics/yolov5', model_name, pretrained=True)
        path = 'torch/hub/shlhc_bch_master'
        model_dict = {torch.hub.load(path, model_name, source='local')
                      for model_name in model_selection_options}
    app_str = 'server:app'  # make the app string equal to whatever the name of this file is
    uvicorn.run(app_str, host=opt.host, port=opt.port, reload=True)
