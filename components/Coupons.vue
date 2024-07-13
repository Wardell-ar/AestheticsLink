<template>
  <div class="coupons">
    <h3>优惠券</h3>
    <div class="grid">
      <div v-for="coupon in coupons" :key="coupon.id" class="card">
        <div class="name text">{{ coupon.cou_name }}</div>
        <div class="type text">{{ coupon.type }} </div>
        <p v-if="!coupon.status">已使用</p>
        <p v-else-if="!coupon.validity">已过期</p>
        <p v-else>
          <el-button type="primary" size="small" class="use-button">使用</el-button>
        </p>
        <div class="date text">有效期至：{{ coupon.valid_date }}</div>
        
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Coupons',
  data() {
    return {
      coupons: [
        { id: 1, cou_name:'满1000减80',valid_date:'2024.12.31', type:"折扣券"
          ,validity: true,status:true},
        { id: 2, cou_name:'250元',valid_date:'2024.1.1', type:"现金券"
          ,validity: false,status:true},
      ]
    };
  }
};
</script>

<style lang="scss" scoped>
h3 {
  margin-bottom: 30px;
  color: #007bff;
}

.coupons {
  padding: 20px;
  background-color: #f9f9f9;
}
  
.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 20px;
}
  
.card {
  background-color: #333;
  color: #fff;
  padding: 15px;
  border-radius: 10px;
  border:#007bff 2px solid;
  text-align: center;
  position: relative;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width:200px;

  .text{
    font-size:16px;
  }

  .use-button {
    background-color: #fff;
    color: #007bff;
    border: none;
  }
  
}
</style>