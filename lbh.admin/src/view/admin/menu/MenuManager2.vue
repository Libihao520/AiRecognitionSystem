<script lang="ts" setup>
import { ref } from "vue";
import { Edit, Delete } from "@element-plus/icons-vue";
import { delfzzjtable, getfzzjtable } from "../../../http";
import ChannelEdit from "./components/ChannelEdit.vue";
import { ElMessageBox } from "element-plus";

const fz = ref([]);
const loading = ref(false);
const dialog = ref();
const onSuccess = ()=>{
  getfzlist()
}
//请求获取数据
const getfzlist = async () => {
  loading.value = true;
  const res = (await getfzzjtable()) as any;
  fz.value = res.data;
  loading.value = false;
};
getfzlist();
//click
const onDelChannel = async (row) => {
  await ElMessageBox.confirm('确认要删除？','温馨提示',{
    type:'warning',
    confirmButtonText:'确认',
    cancelButtonText:'取消'

  })
  await delfzzjtable(row.id)
  ElMessage.success('删除成功')
  getfzlist()
}
const onEditChannel = (row, $index) => {
  dialog.value.open( row );
};
const onAddChannel = () => {
  dialog.value.open({});
};
</script>
<template>
  <page-container title="详情">
    <template #extra>
      <el-button @click="onAddChannel">添加</el-button>
    </template>
    <el-table v-loading="loading" :data="fz" style="width: 100%">
      <el-table-column type="index" label="序号" width="60"></el-table-column>
      <el-table-column prop="yf" label="月份"></el-table-column>
      <el-table-column prop="fz" label="房租"></el-table-column>
      <el-table-column prop="sf" label="水"></el-table-column>
      <el-table-column prop="df" label="电"></el-table-column>
      <el-table-column prop="hj" label="合计"></el-table-column>
      <el-table-column prop="cr" label="存入"></el-table-column>
      <el-table-column prop="sy" label="剩余"></el-table-column>
      <el-table-column prop="jldc" label="大餐"></el-table-column>
      <el-table-column prop="ck" label="存款"></el-table-column>
      <el-table-column label="操作" width="100%">
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
            @click="onDelChannel(row, $index)"
          ></el-button>
        </template>
      </el-table-column>
    </el-table>
    <channel-edit ref="dialog" @success="onSuccess"></channel-edit>
  </page-container>
</template>
<style lang="scss" scoped></style>
