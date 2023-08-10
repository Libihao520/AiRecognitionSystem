<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { getfzzjtable } from "../../../http";

const fz = ref([]);
//用于转菊花
const loading = ref(false)
const getfzlist = async () => {
  loading.value = true
  const res = (await getfzzjtable()) as any;
  fz.value = res.data;
  loading.value = false
};
getfzlist();
</script>
<template>
  <el-table v-loading="loading" :data="fz" style="width: 100%">
    <el-table-column prop="yf" label="月份" width="90" />
    <el-table-column prop="fz" label="房租" width="90" />
    <el-table-column prop="sf" label="水" width="90" />
    <el-table-column prop="df" label="电" width="90" />
    <el-table-column prop="hj" label="合计" width="90" />
    <el-table-column prop="cr" label="存入" width="90" />
    <el-table-column prop="sy" label="剩余" width="90" />
    <el-table-column prop="jldc" label="大餐" width="90" />
    <el-table-column prop="ck" label="存款" />
  </el-table>
</template>

<style lang="scss" scoped>
.el-pagination {
  margin-top: 50px;
}

.cell {
  text-align: center !important;
}

.queryTable {
  width: 500px;
  border-collapse: collapse;

  tr {
    height: 50px;

    td {
      padding: 10px;
    }

    .left {
      width: 30%;
      background-color: #f5f7fa;
    }

    .right {
      width: 80%;
    }
  }
}
</style>