<template>
  <div class="discounts">
    <h3>折扣信息</h3>
      <p>您当前的 VIP 等级为：{{ vipLevel }}</p>
      <p>您可以享受全场{{ discount }}折的优惠</p>
  </div>
</template>

<script>
import { get_id } from '@/identification';
import { GetVipInfo } from '@/HTTP/http';

export default {
  data(){
    return {
      vipLevel: null,
      discount: null
    }
  },
  mounted(){
    GetVipInfo(get_id(), this.setdata);
  },
  methods:{
    setdata(response){
      this.vipLevel = response.vipLevel;
      this.discount = response.discount * 10;
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