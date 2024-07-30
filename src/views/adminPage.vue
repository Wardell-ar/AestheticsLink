<!-- adminPage.vue -->
<template>
  <div id="app">
    <div class="sidebar">
      <div class="showlogo">
        <img src="../assets/pics/logo.png" alt="logo">
        <p>容颜医疗</p>
      </div>
      <ul>
        <li></li>
        <li>
          <RouterLink to="#" @click="showLink2">人事管理</RouterLink>
        </li>
        <li>
          <RouterLink :to="{path:'/customer_infoTable'}" @click="showLink3">顾客信息</RouterLink>
        </li>
        <li>
          <RouterLink to="#" @click="showLink4">医药采购</RouterLink>
        </li>
        <li>
          <RouterLink :to="{path:'/HospitalFinanace'}"  @click="showLink4">收支统计</RouterLink>
        </li>
        <li>
          <RouterLink to="#" @click="showLink4">费用管理</RouterLink>
        </li>
      </ul>
    </div>
    
    <!-- <div class="main-content">
      
      <div class="toolbar">
        <input type="text" v-model="inputValue" placeholder="请输入出生的年/月/日">
        <input type="text" v-model="inputValue" placeholder="请输入姓名/卡号">
        <input type="text" v-model="inputValue" placeholder="请输入手机号">
        <Dropdown_gender />      
        <Dropdown_level />
        
      </div>
      <div class="display-table">
        <RouterView></RouterView>
      </div>
    </div> -->
    <HospitalFinance/>
  </div>
  
</template>

<script>
import { RouterLink, RouterView } from 'vue-router'
import Dropdown_gender from '@/components/Dropdown_gender.vue'
import Dropdown_level from '@/components/Dropdown_level.vue'
import HospitalFinance from '@/components/HospitalFinance.vue';

export default {
  data() {
    return {
    }

  },
  methods: {

  },
  components: {
    Dropdown_level,
    Dropdown_gender,
    HospitalFinance
  }
}
</script scoped>

<style>
html,
body {
  width: 100%;
  height: 100%;
}

#app {
  display: flex;
  width: 100%;
  height: 100%;
  margin: 0;
  padding: 0;
}

.sidebar {
  display: flex;
  margin: 0;
  width: 12%;
  background-color: #f4f4f4;
  flex-direction: column;
  box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
}

.sidebar ul {
  list-style-type: none;
  padding: 0;
}

.sidebar ul li {
  margin: 10px 0;
}


.sidebar ul li a {
  text-decoration: none;
  color: #333;
  display: block;
  padding: 10px;
  border-radius: 4px;
  transition: background-color 0.3s;
  text-align: center;
  line-height: 50px;
}

.sidebar ul li a:hover {
  background-color: #ddd;
}

.main-content {
  flex-grow: 1;
}

.main-content .toolbar {
  display: flex;
  /* justify-content: space-between; */
}

.showlogo {
  display: flex;
  border-bottom: 1px solid black;
  border-right: 1px solid black;
  height: 10%;
}

.showlogo img {
  width: 40%;
  height: auto;
}

.showlogo p {
  align-content: center;
  font-size: 25px;
}

.main-content .title {
  background-color: blue;
  height: 10%;
}
</style>
