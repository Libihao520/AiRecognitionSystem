<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { getfzzjdesktop } from "../../../http";

const fz = ref([]);

const getfzlist = async () => {
  const res = (await getfzzjdesktop(fz)) as any;
  fz.value = res.data;

  console.log(res);
  console.log(fz);
};
getfzlist();
</script>
<template>
<p1>{{ fz }}</p1>

<el-table :data="fz" style="width: 100%">
    <el-table-column prop="hj" label="合计" width="180" />
    <el-table-column prop="name" label="Name" width="180" />
    <el-table-column prop="address" label="Address" />
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