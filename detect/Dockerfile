

# FROM python:3.8.6-buster  AS build
# WORKDIR /apps
# COPY ./apps/requirements.txt ./
# RUN pip install -r requirements.txt -i https://pypi.tuna.tsinghua.edu.cn/simple --no-cache-dir 


FROM python:3.8.6-buster  AS build
WORKDIR /apps
COPY ./apps ./
RUN apt-get update && apt-get install -y \
    libgl1-mesa-glx
RUN pip install -r requirements.txt -i https://pypi.tuna.tsinghua.edu.cn/simple --no-cache-dir 