<script setup>
import { ref } from 'vue'
const dialogVisible = ref(false)

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
    <span>内容</span>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="dialogVisible = false"> 确认 </el-button>
      </span>
    </template>
  </el-dialog>
</template>