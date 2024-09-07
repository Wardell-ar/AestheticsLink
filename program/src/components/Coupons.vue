<template>
  <div class="coupons">
    <h3>优惠券</h3>
    <div class="grid">
      <div v-for="coupon in coupons" :key="coupon.id" class="card">
        <div class="name text">{{ coupon.cou_name }}</div>
        <div class="type text">{{ coupon.type }} </div>
        <p>
          <el-button @click="useCoupons" type="primary" size="small" class="use-button">使用</el-button>
        </p>
        <div class="date text">有效期至：{{ coupon.valid_date }}</div>

      </div>
    </div>
  </div>
</template>

<script>
import { get_id } from '@/identification';
import { GetCouponsInfo } from '@/HTTP/http';

export default {
  name: 'Coupons',
  data() {
    return {
      coupons: []
    };
  },
  mounted() {
    GetCouponsInfo(get_id(), this.setdata);
  },
  methods: {
    setdata(response) {
      this.coupons = [];
      let i = 1;
      for (const item of response) {
        let obj = {
          id: i,
          cou_name: item.name,
          type: item.price + "现金券",
          valid_date: item.year + "-" + item.month + "-" + item.day
        }
        this.coupons.push(obj);
        i++;
      }
    },
    useCoupons(){
      // 跳转到购物车界面
      // ...
      this.$router.push("/ourService");
    }
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
  border: #007bff 2px solid;
  text-align: center;
  position: relative;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: 200px;

  .text {
    font-size: 16px;
  }

  .use-button {
    background-color: #fff;
    color: #007bff;
    border: none;
  }

}
</style>