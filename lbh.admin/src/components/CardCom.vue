<script lang = "ts" setup>
import { ShoppingCart, Ship, CreditCard, Food } from '@element-plus/icons-vue'

import { CardModel } from '../class/CardModel'
defineProps({
    info: CardModel
})
</script>
<template>
    <div class="card">
        <div class="left">

            <CreditCard v-if="info?.Icon == 'CreditCard'" />
            <Food v-if="info?.Icon == 'Food'" />
            <Ship v-if="info?.Icon == 'Ship'" />
            <ShoppingCart v-if="info?.Icon == 'ShoppingCart'" />
        </div>
        <div class="right">
            <div><span>{{ info?.Title }}</span></div>
            <div><span>{{ info?.Count }}</span></div>
        </div>
    </div>
</template>
<style lang="scss" scoped>
.card {
    height: 50px;
    line-height: 25px;
}

.left {
    width: 50px;
    height: 50px;
    float: left;

    .el-icon {
        font-size: 50px;
    }
}

.right {
    width: 100px;
    height: 50px;
    float: right;
}
</style>