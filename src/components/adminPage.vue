<template>
  <div id="app">
    <div class="sidebar">
      <div class="showlogo">
        <img src="../assets/pics/logo.png" alt="logo">
        <p>容颜医疗</p>
      </div>
      <ul>
        <li>
          <RouterLink to="#" @click="showLink2">智慧首页</RouterLink>
        </li>
        <li>
          <RouterLink to="#" @click="showLink2">人事管理</RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/customer_infoTable' }" @click="showLink3">顾客信息</RouterLink>
        </li>
        <li>
          <RouterLink to="#" @click="showLink4">医药采购</RouterLink>
        </li>
        <li>
          <RouterLink to="#" @click="showLink4">收支统计</RouterLink>
        </li>
        <li>
          <RouterLink to="#" @click="showLink4">服务管理</RouterLink>
        </li>
        <li>
          <RouterLink to="#" @click="showLink4">分院管理</RouterLink>
        </li>
        <li>
          <RouterLink to="#" @click="showLink4">手术室管理</RouterLink>
        </li>
      </ul>
    </div>
    <div class="main-content">
      <div class="title">
        顾客信息资料表
      </div>
      <div class="toolbar">
        <input type="text" v-model="agemin" placeholder="请输入年龄的下界">
        <p style="font-size:larger">~</p>
        <input type="text" v-model="agemax" placeholder="请输入年龄的上界">
        <input type="text" v-model="name" placeholder="请输入姓名">
        <input type="text" v-model="phone_num" placeholder="请输入手机号">
        <Dropdown_gender @updateGender="getGender" :clearData="clearData" @reset-clear-data="Reset" />
        <Dropdown_level @updateLevel="getLevel" :clearData="clearData" @reset-clear-data="Reset" />
        <div class="clear">
          <button class="clearbtn" @click="Clear">清空</button>
        </div>
        <div class="search">
          <button class="searchbtn" @click="Search">查询</button>
        </div>
      </div>
      <div class="display-table">
        <RouterView></RouterView>
      </div>
    </div>
  </div>
</template>

<script>
import Dropdown_gender from '@/components/Dropdown_gender.vue'
import Dropdown_level from '@/components/Dropdown_level.vue'
import { searchCustomerInfo } from '@/assets/http.js'


export default {
  data() {
    return {
      agemin: "",
      agemax: "",
      name: "",
      phone_num: "",
      gender: "",
      viplevel: "",
      clearData: false,
    }
  },
  components: {
    Dropdown_gender,
    Dropdown_level
  },
  methods: {
    displayTable(response) {
      console.log(response);
    },
    getGender(data) {
      this.gender = data;
    },
    getLevel(data) {
      this.viplevel = data;
    },
    Clear() {
      this.agemin = "",
      this.agemax = "",
      this.name = "",
      this.phone_num = "",
      this.gender = "",
      this.viplevel = "",
      this.clearData = true;
    },
    Reset() {
      this.clearData = false;
    },
    Search() {
      const dataToSend = {
        ...(this.agemin && { agemin: this.agemin }),
        ...(this.agemax && { agemax: this.agemax }),
        ...(this.name && { name: this.name }),
        ...(this.phone_num && { phone_num: this.phone_num }),
        ...(this.gender && { gender: this.gender }),
        ...(this.viplevel && { viplevel: this.viplevel }),
        // agemin: 20,
      }
      searchCustomerInfo(dataToSend, this.displayTable);
    }
  }
}
</script>

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
}

.sidebar {
  display: flex;
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

.showlogo {
  display: flex;
  border-bottom: 2px solid black;
  border-right: 2px solid black;
  height: 10%;
}

.main-content {
  width: 100%;
  margin-left: 4px;
}

.main-content .title {
  background-color: whitesmoke;
  text-align: center;
  line-height: 60px;
  height: 6%;
  margin-bottom: 4px;
  box-shadow: 0 2px 2px rgba(0, 0, 0, 0.2);
}

.main-content .toolbar {
  display: flex;
  margin-bottom: 20px;
}

.toolbar input {
  border-radius: 10px;
  font-size: medium
}

.showlogo img {
  width: 40%;
  height: auto;
}

.showlogo p {
  align-content: center;
  font-size: 25px;
}

.display-table {
  display: flex;
}

.clear {
  margin-left: auto;
  /* 将按钮推到最右边 */
}

.clearbtn {
  background-color: rgb(54, 63, 198);
  height: 100%;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: 1px solid grey;
  cursor: pointer;
}

.clearbtn:active {
  background-color: black;
  transform: scale(0.98);
}

.searchbtn {
  background-color: rgb(54, 63, 198);
  height: 100%;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: 1px solid grey;
  cursor: pointer;
}

.searchbtn:active {
  background-color: black;
  transform: scale(0.98);
}
</style>
