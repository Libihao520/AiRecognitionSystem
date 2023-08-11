<script lang="ts" setup>
import { ref } from 'vue'
import { Edit, Delete } from '@element-plus/icons-vue'
import { getfzzjtable } from "../../../http";
import ChannelEdit from './components/ChannelEdit.vue';

const fz = ref([]);
const loading = ref(false)
const dialog = ref()
//请求获取数据
const getfzlist = async () => {
  loading.value = true
  const res = (await getfzzjtable()) as any;
  fz.value = res.data;
  loading.value=false
};
getfzlist();
//click
const onEditChannel = (row, $index) => {
  dialog.value.open({row})
}
const onAddChannel = () => {
  dialog.value.open({})
}
</script>
<template>
  <page-container title="文章分类">
    <template #extra>
      <el-button @click="onAddChannel">添加分类</el-button>
    </template>
    <el-table v-loading="loading" :data="fz" style="width: 100%">
      <el-table-column type="index" label="序号" width="100%"></el-table-column>
      <el-table-column prop="yf" label="分类名称"></el-table-column>
      <el-table-column prop="fz" label="分类别名"></el-table-column>
      <el-table-column label="操作">
        <!-- row 就是channelList的一项，$index下标 -->
        <template #default="{ row, $index }">
          <el-button
            :icon="Edit"
            circle
            type="primary"
            plain
            @click="onEditChannel(row, $index)"
          ></el-button>
          <el-button
            :icon="Delete"
            circle
            type="danger"
            plain
            @click="onEditChannel(row, $index)"
          ></el-button>
        </template>
      </el-table-column>
    </el-table>
    <channel-edit ref="dialog"></channel-edit>
  </page-container>
</template>
<style lang="scss" scoped></style>
