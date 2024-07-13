<!-- Discounts.vue -->
<template>
  <div class="discounts">
    <h3>折扣信息</h3>
      <p>您当前的 VIP 等级为：VIP{{ vipLevel }}</p>
      <p v-if="vipLevel === 0">您当前没有享受全场折扣</p>
      <p v-else>您可以享受全场{{ discount }}折的优惠</p>
  </div>
</template>

<script>
export default {
  name: 'Discounts',
  props: {
    vipLevel: {
      type: Number,
      required: true
    }
  },
  computed: {
    discount() {
      const discounts = {
        1: '95',
        2: '9',
        3: '8',
        4: '7',
        5: '65'
      };
      return discounts[this.vipLevel] || '10';
    }
  }
}
</script>

<style scoped>
.discounts{
  padding:20px;
  background-color: #f9f9f9;
}

h3 {
  margin-bottom: 30px;
  color: #007bff;
}


p {
  font-size: 18px;
  font-weight: bold;
}
</style>