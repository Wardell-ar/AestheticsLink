<template>
  <div class="common-layout">
    <el-container>
      <el-header class="header">
        <div class="logo-container">
          <img src="/images/company-logo.png" alt="Company Logo" class="company-logo">
        </div>
        <h2 class="main-title">员工个人中心</h2>
        <div class="employee-img">
          <img :src="employee.avatar" alt="Employee Avatar">
        </div>
        <div class="header-info">
          <div class="employee-info">
            <div class="employee-name">
              <span>你好, {{ employee.name }}</span>
            </div>
            <div class="employee-id">
              <span>ID: {{ employee.id }}</span>
            </div>
            <div class="employee-title">
              <span>职称: {{ employee.title }}</span>
            </div>
          </div>
          <el-button class="button" @click="create_dialog=true" type="primary">
            <el-icon><Edit /></el-icon>编辑
          </el-button>
        </div>
      </el-header>
      <el-container>
        <el-aside class="aside">
          <h5 class="mb-2">员工中心</h5>
          <el-menu
            :default-active="activeIndex"
            class="el-menu-vertical-demo"
            @select="handleMenuSelect"
          >
            <el-menu-item index="1">
              <el-icon><location /></el-icon>
              <span>员工信息</span>
            </el-menu-item>
            <el-menu-item index="2">
              <el-icon><icon-menu /></el-icon>
              <span>员工考勤</span>
            </el-menu-item>
            <el-menu-item index="3">
              <el-icon><document /></el-icon>
              <span>员工订单</span>
            </el-menu-item>
          </el-menu>
        </el-aside>
        <el-main class="main">
          <div v-if="activeIndex === '1'">
            <h3>员工信息</h3>
            <el-table :data="employeeData">
              <el-table-column prop="id" label="员工ID"></el-table-column>
              <el-table-column prop="name" label="姓名"></el-table-column>
              <el-table-column prop="gender" label="性别"></el-table-column>
              <el-table-column prop="age" label="年龄"></el-table-column>
              <el-table-column prop="title" label="职称"></el-table-column>
              <el-table-column prop="hospital" label="所属医院"></el-table-column>
            </el-table>
          </div>
          <div v-if="activeIndex === '2'">
            <h3>员工考勤</h3>
            <el-button @click="handleAttendance">考勤</el-button>
            <p v-if="employee.attendanceStatus === '0'">您今天还没有考勤</p>
            <p v-if="employee.attendanceStatus  === '1'">您今天已考勤</p>
          </div>
          <div v-if="activeIndex === '3'">
            <h3>员工订单</h3>
            <el-table :data="orderData">
              <el-table-column prop="order_id" label="订单ID"></el-table-column>
              <el-table-column prop="order_name" label="订单名称"></el-table-column>
              <el-table-column prop="order_date" label="订单日期"></el-table-column>
            </el-table>
          </div>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import { ref } from 'vue';
import { Edit } from '@element-plus/icons-vue';
import { getEmployeeInfoReq,getAttendReq,updateAttendReq } from "../HTTP/http"  // 请入HTTP请求函数
import {get_id} from "../identification"

export default {
  data() {
    return {
      activeIndex: '1',
      create_dialog: false,
      // 写给前后端交接人员：以下三种数据均需从数据库中查询获得，时机是页面初始加载时。
      employee: {
        id: "",
        name: "",
        gender: "",
        age: 0,
        title: "",
        hospital: "",
        attendanceStatus: "",
      },
      orderData: []
    };
  },

  // 在挂载前就发送请求获取数据
  beforeMount(){
    getEmployeeInfoReq(get_id(),this.setData);
  },

  methods: {
    handleMenuSelect(index) {
      this.activeIndex = index;
    },

    // 点击签到
    handleAttendance() {
      getAttendReq(get_id(),this.check);
    },

    // 获取员工信息后，填入到本地数据
    setData(res){
      // 开始填写数据
      this.employee.id=res.id;
      this.employee.name=res.name;
      this.employee.gender=res.gender;
      this.employee.age=res.age;
      this.employee.title=res.title;
      this.employee.hospital=res.hospital;
      this.employee.attendanceStatus=res.attendanceStatus;

      for(const order of res.orderData){
        let obj = {
          order_id:order.order_id,
          order_name:order.order_name,
          order_date:order.order_year + "-" + order.order_month + "-" + order.order_day
        }
        this.orderData.push(obj);
      }
    },


    check(s){
      this.employee.attendanceStatus = s;
      if (s === "0") {
        this.employee.attendanceStatus = "1";
        // 向数据库更新签到状态
        updateAttendReq(get_id(),this.IsRegistered);
      }
      else{
        this.$message.error('您今日已经考勤');
      }
    },

    IsRegistered(r){
      if(r === "1"){
        this.$message.success('考勤成功！');
      }
      else{
        this.$message.success('考勤信息录入失败，请稍后重试');
      }
    }
  },
  components: {
    Edit
  }
};
</script>

<style scoped>
.header {
  width: 100%;
  height: 100px;
  padding: 20px;
  background-color: white;
  display: flex;
  align-items: center;
  border-radius: 5px;
}

.logo-container {
  flex: 0 0 auto;
}

.company-logo {
  width: 80px;
  height: 80px;
  margin-right: 20px;
}

.main-title {
  flex: 1;
  font-size: 24px;
  font-weight: bold;
  text-align: center;
}

.employee-img {
  flex: 0 0 auto;
  width: 80px;
  height: 80px;
  background-color: #c2d7f5;
  border-radius: 50%;
  overflow: hidden;
  margin-right: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.employee-img img {
  width: auto;
  height: 100%;
  object-fit: contain;
}

.header-info {
  flex: 0 1 auto;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  margin-right: 20px;
}

.employee-info {
  margin-bottom: 10px;
}

.employee-name {
  font-size: 16px;
  font-weight: bold;
}

.employee-id,
.employee-title {
  font-size: 14px;
  color: #666;
}

.button {
  align-self: flex-end;
}
.aside {
  height: auto;
  border-radius: 5px;
  margin-right: 3%;
  text-align: center;
  width: 15%;
  background-color: #b3dff8;
  /* align-content: center; */
}
h5 {
  width: 100%;
  height: 30px;
  margin-top: 30px;
  font-size: 30px;
}
.el-menu-item {
  margin-top: 22px;
}
.main {
  width: 70%;
  height: 500px;
  border-radius: 5px;
  background-color: white;
}
.updateinfo {
  height: 350px;
  overflow: auto;
}
.left {
  /* width: 330px; */
  float: left;
}
.right {
  overflow: hidden;
}
</style>