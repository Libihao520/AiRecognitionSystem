<script setup>
import { ref } from 'vue'
const dialogVisible = ref(false)
const formRef = ref()
const formModel = ref({
  yf: '',
  fz: ''
})
const rules = {
  yf: [
    {
      required: true,
      message: '请输入分类名称',
      trigger: 'blur'
    }
  ],
  fz: [
    {
      required: true,
      message: '请输入分类别名',
      trigger: 'blur'
    }
  ]
}
const onSubmit = async () => {
  await formRef.value.validate()
  const isEdit = formModel.value.id
  console.log(isEdit)
  if (isEdit) {
    await artEditChannelService(formModel.value)
    ElMessage.success('编辑成功')
  } else {
    await artAddChannelService(formModel.value)
    ElMessage.success('添加成功')
  }
  dialogVisible.value=false
}

//组件对外暴露方法
//({}) => 无需渲染，是添加
//有数据是编辑，需要回显
const open = (row) => {
  console.log(row)
  dialogVisible.value = true
  formModel.value = {...row}
}
defineExpose({
  open
})
</script>

<template>
  <el-dialog v-model="dialogVisible" title="添加弹层" width="30%">
    <el-form
      ref="formRef"
      :model="formModel"
      :rules="rules"
      label-width="100px"
      style="padding-right: 30px"
    >
      <el-form-item label="月份:" prop="yf">
        <el-input
          v-model="formModel.yf"
          placeholder="请输入月份"
        ></el-input>
      </el-form-item>
      <el-form-item label="房租:" prop="fz">
        <el-input
          v-model="formModel.fz"
          placeholder="请输入房租"
        ></el-input>
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