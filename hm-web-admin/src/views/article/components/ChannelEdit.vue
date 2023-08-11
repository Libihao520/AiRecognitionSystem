<script setup>
import { ref } from 'vue'
const dialogVisible = ref(false)
const formModel = ref({
  cate_name: '',
  cate_alias: ''
})
const rules = {
  cate_name: [
    {
      required: true,
      message: '请输入分类名称',
      trigger: 'blur'
    }
  ],
  cate_alias: [
    {
      required: true,
      message: '请输入分类别名',
      trigger: 'blur'
    }
  ]
}

//组件对外暴露方法
//({}) => 无需渲染，是添加
//有数据是编辑，需要回显
const open = (row) => {
  console.log(row)
  dialogVisible.value = true
}
defineExpose({
  open
})
</script>

<template>
  <el-dialog v-model="dialogVisible" title="添加弹层" width="30%">
    <el-form :model="formModel" :rules="rules" label-width="100px" style="padding-right: 30px">
      <el-form-item label="分类名称:" prop="cate_name">
        <el-input v-model="formModel.cate_name" placeholder="请输入分类名称"></el-input>
      </el-form-item>
      <el-form-item label="分类别名:" prop="cate_alias">
        <el-input v-model="formModel.cate_alias" placeholder="请输入分类别名"></el-input>
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="dialogVisible = false"> 确认 </el-button>
      </span>
    </template>
  </el-dialog>
</template>