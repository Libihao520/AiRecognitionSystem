<script setup>
import { ref } from 'vue'
import ChannelSelect from './ChannelSelect.vue'

const visibleDrawer = ref(false)
//默认数据
const defaultForm = {
  title: '', //标题
  cate_id: '', //分类id
  cover_img: '', //封面图片
  content: '', //string内容
  state: '' //状态
}
//准备数据
const formModel = ref({
  ...defaultForm
})

const open = (row) => {
  visibleDrawer.value = true
  console.log(row)
  if (row.id) {
    console.log('回显')
  } else {
    console.log('添加')
    //重置from数据
    formModel.value = {
      ...defaultForm
    }
  }
}
defineExpose({ open })
</script>
import ChannelSelect from './ChannelSelect.vue'

<template>
  <el-drawer
    v-model="visibleDrawer"
    :title="formModel.id ? '编辑文章' : '添加文章'"
    direction="rtl"
    size="50%"
  >
    <!-- 发表文章表单 -->
    <el-form :model="formModel" ref="formRef" label-width="100px">
      <el-form-item label="文章标题" prop="title">
        <el-input v-model="formModel.title" placeholder="请输入标题"></el-input>
      </el-form-item>
      <el-form-item label="文章分类" prop="cate_id">
        <channel-select
          v-model="formModel.cate_id"
          width="100%"
        ></channel-select>
      </el-form-item>
      <el-form-item label="文章封面" prop="cover_img"> 文件上传 </el-form-item>
      <el-form-item label="文章内容" prop="content">
        <div class="editor">富文本编辑器</div>
      </el-form-item>
      <el-form-item>
        <el-button type="primary">发布</el-button>
        <el-button type="info">草稿</el-button>
      </el-form-item>
    </el-form>
  </el-drawer>
</template>
