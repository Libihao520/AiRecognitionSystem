<script setup>
const goList = () => {
  this.$router.push('/list')
}
</script>
<template>
  <div>wsdiv</div>
  <el-button @click="$router.push('/home')">跳首页</el-button>
  <button @click="goList">跳列表</button>
</template>

<style scoped></style>
