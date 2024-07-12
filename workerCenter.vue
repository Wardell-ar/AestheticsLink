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

export default {
  data() {
    return {
      activeIndex: '1',
      create_dialog: false,
      // 写给前后端交接人员：以下三种数据均需从数据库中查询获得，时机是页面初始加载时。
      employee: {
        avatar: "/images/doctor-1.png",
        id: "12345",
        name: "张三",
        gender: "男",
        age: 30,
        title: "医生",
        hospital: "某某医院",
        attendanceStatus: '0',
      },
      employeeData: [
        { id: "12345", name: "张三", gender: "男", age: 30, title: "医生", hospital: "某某医院" }
      ],
      orderData: [
        { order_id: "001", order_name: "订单一", order_date: "2024-07-10" },
        { order_id: "002", order_name: "订单二", order_date: "2024-07-11" }
      ]
    };
  },
  methods: {
    handleMenuSelect(index) {
      this.activeIndex = index;
    },
    // 写给前后端交接人员：这个函数每次触发都需要向数据库获取一个员工的考勤属性！！！
    handleAttendance() {
      //这里获取员工的考勤值，送给this.employee.attendanceStatus
      //因为需要每次考勤之前实时刷新与数据库进行同步
      if (this.employee.attendanceStatus === '0') {
        this.employee.attendanceStatus = '1';
        this.$message.success('考勤成功！');
        // 写给前后端交接人员：这里this.employee.attendanceStatus的数据发生了变化(从0->1)需要考勤数据发送回数据库！！
        //......
      }
      else{
        this.$message.error('您今日已经考勤');
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