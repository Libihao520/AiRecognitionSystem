<script setup>
import { Delete, Edit } from '@element-plus/icons-vue'
import { ref } from 'vue'
import { artGetListService } from '../../api/article'
import ChannelSelect from './components/ChannelSelect.vue'
import { formatTime } from '@/utils/format.js'

const articleList = ref([]) //文章列表
const total = ref(0) //总条数
const loading = ref(false)
//定义请求参数对象
const params = ref({
  pagenum: 1,
  pagesize: 5,
  cate_id: '',
  state: ''
})
//获取数据
const getArticleList = async () => {
  loading.value = true
  const res = await artGetListService(params.value)
  console.log(res.data)
  articleList.value = res.data.data
  total.value = res.data.total
  loading.value = false
}
getArticleList()
//处理分页逻辑

const onSizeChange = (size) => {
  params.value.pagenum = 1
  params.value.pagesize = size
  getArticleList()
}
const onCurrentChange = (page) => {
  params.value.pagenum = page
  getArticleList()
}
//编辑
const onEditArticle = (row) => {
  console.log(row)
}
//删除
const onDeleteArticle = (row) => {
  console.log(row)
}
</script>
<template>
  <page-container title="文章管理">
    <template #extra><el-button>添加文章</el-button></template>
    <el-form inline>
      <el-form-item label="文章分类："
        ><channel-select v-model="params.cate_id"></channel-select
      ></el-form-item>
      <el-form-item label="发布状态：">
        <el-select v-model="params.state">
          <el-option label="已发布" value="已发布"></el-option>
          <el-option label="草稿" value="草稿"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary">搜索</el-button>
        <el-button>重置</el-button>
      </el-form-item>
    </el-form>
    <!-- 表格区域 -->
    <el-table :data="articleList" v-loading="loading">
      <el-table-column label="文章标题" prop="title">
        <template #default="{ row }">
          <el-link type="primary" :underline="false">{{
            row.title
          }}</el-link></template
        >
      </el-table-column>
      <el-table-column label="分类" prop="cate_name"></el-table-column>
      <el-table-column label="发表时间" prop="pub_date">
        <template #default="{ row }">
          {{ formatTime(row.pub_date) }}
        </template>
      </el-table-column>
      <el-table-column label="状态" prop="state"></el-table-column>
      <el-table-column label="操作">
        <template #default="{ row }">
          <el-button
            circle
            plain
            type="primary"
            :icon="Edit"
            @click="onEditArticle(row)"
          ></el-button>
          <el-button
            circle
            plain
            type="danger"
            :icon="Delete"
            @click="onDeleteArticle(row)"
          ></el-button>
        </template>
      </el-table-column>
    </el-table>
    <!-- 分页区域 -->
    <el-pagination
      v-model:current-page="params.pagenum"
      v-model:page-size="params.pagesize"
      :page-sizes="[2, 3, 5, 10]"
      :background="true"
      layout="jumper,total, sizes, prev, pager, next"
      :total="total"
      @size-change="onSizeChange"
      @current-change="onCurrentChange"
      style="margin-top: 20px"
    />
  </page-container>
</template>
<style lang="scss" scoped></style>
