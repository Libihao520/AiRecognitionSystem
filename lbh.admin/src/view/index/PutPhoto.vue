<script setup>
import { ref } from "vue";
import { Plus, Upload } from "@element-plus/icons-vue";
import { PutPhotoService } from "../../http";
const imgUrl = ref("");
const uploadRef = ref();
const onSelectFile = (uploadFile) => {
  // 基于 FileReader 读取图片做预览
  const reader = new FileReader();
  reader.readAsDataURL(uploadFile.raw);
  reader.onload = () => {
  imgUrl.value = reader.result;
  };
};
const name = ref("皮卡丘")
//发送请求
const onUpdateAvatar = async () => {
  // 上传图片
  const res =await PutPhotoService(imgUrl.value,name.value);
  console.log(res.data)
  imgUrl.value = res.data;
  // 提示用户
  ElMessage.success("识别成功");
};
</script>
<template>
  <page-container title="AI识别">
    <el-form inline>
      <el-form-item label="选择模型：">
        <el-select v-model="name" >
          <el-option label="皮卡丘" value="皮卡丘"></el-option>
          <el-option label="木头运输" value="木头运输"></el-option>
        </el-select>
      </el-form-item>
    </el-form>
    <el-upload
      ref="uploadRef"
      :auto-upload="false"
      class="avatar-uploader"
      :show-file-list="false"
      :on-change="onSelectFile"
    >
      <img v-if="imgUrl" :src="imgUrl" class="avatar" />
      <el-icon v-else class="avatar-uploader-icon"><Plus /></el-icon>
    </el-upload>

    <br />
    <el-button
      @click="uploadRef.$el.querySelector('input').click()"
      type="primary"
      :icon="Plus"
      size="large"
      >选择图片</el-button
    >
    <el-button
      @click="onUpdateAvatar"
      type="success"
      :icon="Upload"
      size="large"
      >上传图片</el-button
    >
  </page-container>
</template>
<style lang="scss" scoped>
.avatar-uploader {
  :deep() {
    .avatar {
      width: 278px;
      height: 278px;
      display: block;
    }
    .el-upload {
      border: 1px dashed var(--el-border-color);
      border-radius: 6px;
      cursor: pointer;
      position: relative;
      overflow: hidden;
      transition: var(--el-transition-duration-fast);
    }
    .el-upload:hover {
      border-color: var(--el-color-primary);
    }
    .el-icon.avatar-uploader-icon {
      font-size: 28px;
      color: #8c939d;
      width: 278px;
      height: 278px;
      text-align: center;
    }
  }
}
</style>