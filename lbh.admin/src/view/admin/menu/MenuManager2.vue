<script lang="ts" setup>
import { ref } from 'vue'
import { Edit, Delete } from '@element-plus/icons-vue'
import { getfzzjtable } from "../../../http";

const fz = ref([]);
const loading = ref(false)
const dialogVisible = ref(false)
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
  console.log(row, $index)
}
const onAddChannel = () => {
  dialogVisible.value = true
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
    <el-dialog v-model="dialogVisible" title="添加弹层" width="30%">
      <span>内容</span>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="dialogVisible = false">
            确认
          </el-button>
        </span>
      </template>
    </el-dialog>
  </page-container>
</template>
<style lang="scss" scoped></style>
