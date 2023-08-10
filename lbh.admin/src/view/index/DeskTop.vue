<template>
  <div class="cardContent">
    <el-card class="box-card" v-for="item in list" :key="item.Title">
      <CardCom :info="item"></CardCom>
    </el-card>
  </div>

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
import { getfzzjdesktop } from "../../http";
import { UserInfo } from "./class/UserInfo";
import CardCom from "../../components/CardCom.vue";
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
const fz = ref([]);
const getfzlist = async () => {
  const res = (await getfzzjdesktop(fz)) as any;
  fz.value = res.data;
  const a = fz[0].value;
};
getfzlist();

const list = ref([
  {
    Title: "收入",
    Icon: "CreditCard",
    Count: 0,
  },
  {
    Title: "大餐",
    Icon: "Food",
    Count: 0,
  },
  {
    Title: "通勤金额",
    Icon: "Ship",
    Count: 0,
  },
  {
    Title: "购物金额",
    Icon: "ShoppingCart",
    Count: 0,
  },
]);
</script>
<style lang="scss" scoped>
.cardContent {
  width: 100%;
  margin: 0px auto;

  .box-card {
    float: left;
    width: 24%;
    margin-right: 5px;
    margin-bottom: 20px;
  }

  .left,
  .right {
    float: left;
    width: 48%;
    margin-bottom: 20px;
  }

  .lineCard {
    width: 97.5%;
  }

  .right {
    margin-left: 20px;
  }
}
</style>