<!-- MyBenefits.vue -->
<template>
    <div class="benefits">
        <el-tabs v-model="activeTab" @tab-click="handleTabClick" class="header">
            <el-tab-pane label="折扣" name="discount"></el-tab-pane>
            <el-tab-pane label="优惠券" name="coupon"></el-tab-pane>
        </el-tabs>
        <div v-if="activeTab === 'discount'">
            <Discounts :vipLevel="userVipLevel" />
        </div>
        <div v-else>
            <Coupons />
        </div>
    </div>
</template>
  
<script>
import { ref } from 'vue';
import Discounts from '../components/Discounts.vue';
import Coupons from '../components/Coupons.vue';
  
export default {
    name: 'App',
    components: {
        Discounts,
        Coupons
    },
    setup() {
        const activeTab = ref('discount');
        const userVipLevel = ref(3); // 假设用户的 VIP 等级为 3
        const handleTabClick = (tab) => {
            console.log('Tab clicked:', tab);
        };
        return {
            activeTab,
            userVipLevel,
            handleTabClick
        };
    }
};
</script>
  
<style>
.benefits {
    margin-top: 60px;
}
  
.header {
    margin-bottom: 20px;
}
  
.el-tabs__item {
    font-size: 16px;
}
  
.el-tabs__item.is-active {
    font-weight: bold;
    color: #007bff;
}

</style>