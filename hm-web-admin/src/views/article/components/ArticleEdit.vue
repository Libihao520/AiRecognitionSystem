<script setup>
import { ref } from 'vue'
const visibleDrawer = ref(false)

const open = (row) => {
  visibleDrawer.value = true
  console.log(row)
}
defineExpose({ open })
</script>
<template>
  <el-drawer v-model="visibleDrawer" title="大标题" size="50%">
    <span>hi there</span></el-drawer
  >
</template>
