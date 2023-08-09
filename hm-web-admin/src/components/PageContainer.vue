<script setup>
defineProps({
  title: {
    required: true,
    type: String
  }
})
</script>
<template>
  <el-card class="page-container">
    <template #header>
      <div class="header">
        <!-- 标题父传子 -->
        <span>{{ title }}</span>
        <!-- 具名插槽定制按钮 -->
        <div class="extra"><slot name="extra"></slot></div>
      </div>
    </template>
    <!-- 默认插槽 -->
    <slot></slot>
  </el-card>
</template>
<style lang="scss" scoped>
.page-container {
  min-height: 100%;
  box-sizing: border-box;
  .header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
}
</style>
