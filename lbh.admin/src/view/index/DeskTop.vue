<template>
  <el-row :gutter="10">
    <el-col :span="8">
      <el-card shadow="hover">登录成功！nickName： {{ NickName }} </el-card>
    </el-col>
  </el-row>

  <el-row class="mb-4">
    <el-button type="primary" @click="logOut">退出</el-button>
  </el-row>
</template>
<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import Tool from "../../global";
import { getfzzj } from "../../http";
import { UserInfo } from "./class/UserInfo";
const circleUrl = ref("/images/Person.jpg");
const NickName = ref();
const router = useRouter();
onMounted(() => {
  NickName.value = useStore().state.NickName;
});

const logOut = () => {
  new Tool().ClearLocalStorage();
  router.push({ path: "/login" });
};
let fz =  getfzzj() as any as string;
console.log(fz)
</script>