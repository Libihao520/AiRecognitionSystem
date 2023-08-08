<script setup>
import { ref } from 'vue'
import { artGetChannelsService } from '../../api/article'
const channelList = ref([])
const getChannelList = async () => {
  const res = await artGetChannelsService()
  channelList.value = res.data.data
  console.log(channelList.value)
}
getChannelList()
</script>
<template>
  <page-container title="文章管理">
    <template #extra><el-button>添加文章</el-button></template
    >主体部分，表格+分页
  </page-container>
</template>
<style lang="scss" scoped></style>
