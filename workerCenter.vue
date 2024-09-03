<template>
  <div class="common-layout">
    <el-container>
      <el-header class="header">
        <div class="logo-container">
          <img src="/images/company-logo.png" alt="Company Logo" class="company-logo">
        </div>
        <h2 class="main-title">员工个人中心</h2>
        <div class="header-info">
          <div class="employee-info">
            <div class="employee-name">
              <span>您好, {{ employee.name }} {{ employee.title }}</span>
            </div>
            <!-- <div class="employee-id">
              <span>ID: {{ employee.id }}</span>
            </div>
            <div class="employee-title">
              <span>职称: {{ employee.title }}</span>
            </div> -->
          </div>
          <!-- <el-button class="button" @click="create_dialog=true" type="primary">
            <el-icon><Edit /></el-icon>编辑
          </el-button> -->
        </div>
      </el-header>
      <el-container>
        <el-aside class="aside">
          <h5 class="workercenter">员工中心</h5>
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
          <div class="container">
          <div v-if="activeIndex === '1'">
            <div class="c-title">
                <label >个人信息</label>        
            </div>
            <el-form :model="employee" :rules="rules" ref="employee" class="form ">
              <div class="row">
              <el-form-item label="姓名" prop="name" class="name">
                <div class="input-container">
                  <input type="text" v-model="employee.name" class="input" disabled>
                  <span class="top-line"></span>
                  <span class="under-line"></span>
                </div>
              </el-form-item>
              <el-form-item label="员工ID" prop="id" class="id">
                <div class="input-container">
                  <input type="text" v-model="employee.id" class="input" disabled>
                  <span class="top-line"></span>
                  <span class="under-line"></span>
                </div>
              </el-form-item>
              </div>
              <div class="row">
                <el-form-item label="性别"  prop="gender" class="gender">
                  <div class="input-container">
                    <input type="text" v-model="employee.gender" class="input" disabled>
                    <span class="top-line"></span>
                    <span class="under-line"></span>
                  </div>
                </el-form-item>
                <el-form-item label="员工年龄"  prop="age" class="age">
                  <div class="input-container">
                    <input type="text" v-model="employee.age" class="input" disabled>
                    <span class="top-line"></span>
                    <span class="under-line"></span>
                  </div>
                </el-form-item>
              </div>
              <div class="row">
                <el-form-item label="职称"  prop="title" class="title">
                  <div class="input-container">
                    <input v-model="employee.title" type="text" class="input" disabled>
                    <span class="top-line"></span>
                    <span class="under-line"></span>
                  </div>
                </el-form-item>
                <el-form-item label="所属医院"  prop="hospital" class="hospital">
                  <div class="input-container">
                    <input v-model="employee.hospital" type="text" class="input" disabled>
                    <span class="top-line"></span>
                    <span class="under-line"></span>
                  </div>
                </el-form-item>
              </div>
              </el-form> 
            <!-- <h3>员工信息</h3>
            <el-table :data="[employee]">
              <el-table-column prop="id" label="员工ID"></el-table-column>
              <el-table-column prop="name" label="姓名"></el-table-column>
              <el-table-column prop="gender" label="性别"></el-table-column>
              <el-table-column prop="age" label="年龄"></el-table-column>
              <el-table-column prop="title" label="职称"></el-table-column>
              <el-table-column prop="hospital" label="所属医院"></el-table-column>
            </el-table> -->
          </div>
          <div v-if="activeIndex === '2'">
            <div class="c-title">
                <label >员工考勤</label>        
            </div>

            <div class="attendanceButton-container">
              <el-button @click="handleAttendance" class="attendanceButton">考勤</el-button>
            </div>
            <p class="encouragement-text">每天都要打卡哦~</p>
            <p v-if="employee.attendanceStatus === '0'">您今天还没有考勤</p>
            <p v-if="employee.attendanceStatus  === '1'">您今天已考勤</p>
          </div>
          <div v-if="activeIndex === '3'">
            <div class="c-title">
                <label >员工订单</label>        
            </div>
            <el-table :data="orderData">
              <el-table-column prop="order_id" label="订单ID"></el-table-column>
              <el-table-column prop="order_name" label="订单名称"></el-table-column>
              <el-table-column prop="order_date" label="订单日期"></el-table-column>
            </el-table>
          </div>
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

.header-info {
  flex: 0 1 auto;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  margin-right: 20px;
}

.employee-info {
  margin-bottom: 0px;
}

.employee-name {
  font-size: 20px;
  font-weight:bolder;
  color:rgb(2, 87, 121);
}

/* .employee-id,
.employee-title {
  font-size: 14px;
  color: #666;
} */

.button {
  align-self: flex-end;
}
.aside {
  height: auto;
    border-radius: 5px;
    text-align: center;
    width: 20%;
    background-color: #ffd7d7;
    align-content: center;
    display: flex;
    flex-direction: column;
}
.workercenter {
    width: 100%;
    height: 50px;
    margin-top: 20px;
    margin-bottom: 100px;
    font-size: 22px;
    border-bottom: 1px solid #f0f0f0;
    color:black;
  }
.el-menu-item {
  margin-top: 22px;
}
.main {
    width: 80%;
    height: 500px;
    border-radius: 5px;
    background-color: white;
    --color1: hwb(0 72% 2%);
    --color2: #ffe5e5;
    background-color: var(--color1);
    background-image: linear-gradient(45deg, var(--color2) 25%, transparent 25%, transparent 75%, var(--color2) 75%, var(--color2)),
      linear-gradient(45deg, var(--color2) 25%, var(--color1) 25%, var(--color1) 75%, var(--color2) 75%, var(--color2));
    background-size: 60px 60px;
    background-position: 0 0, 30px 30px;
    display: flex;
    align-items: center;
    justify-content: center;
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



.container {
    width:100%;
    margin: auto 20px;
    border-radius: 19px 19px 0px 0px;
    background: rgb(255, 250, 235);
    min-height: 450px;
    box-shadow: 0px 187px 75px rgba(0, 0, 0, 0.01), 0px 105px 63px rgba(0, 0, 0, 0.05), 0px 47px 47px rgba(0, 0, 0, 0.09), 0px 12px 26px rgba(0, 0, 0, 0.1), 0px 0px 0px rgba(0, 0, 0, 0.1);
  }


  .form{
    margin-top: 0;
    width: 100%;
    height: 350px ;
    display: flex;
    padding:0;
    flex-direction: column;
  }

  .c-title {
    width: 100%;
    height: 40px;
    position: relative;
    display: flex;
    align-items: center;
    justify-content:space-between;
    padding-left: 20px;
    border-bottom: 1px solid rgba(16, 86, 82, .75);
    font-weight: 700;
    font-size: 20px;
    color: #000000;
    margin: 20px auto;
  }

  .el-form-item {
    position: relative;
    flex: 1;
  }
  
  .input-container {
    position: relative;
    min-width: 250px;
    width: auto;
  }
  .input {
    padding: 10px;
    height: 40px;
    width: 100%;
    border: 2px solid #0b2447;
    border-top: none;
    font-size: 16px;
    background: transparent;
    outline: none;
    box-shadow: 7px 7px 0px 0px #0b2447;
    transition: all 0.5s;
  }
  
  .input.active {
    box-shadow: none;
    transition: all 0.5s;
  }
  
  .label {
    position: absolute;
    top: 10px;
    left: 10px;
    color: #0b2447;
    height:auto;
    transition: all 0.5s;
    transform: scale(0);
    z-index: 0;
    font-size:18px;
  }
  
  .input-container .top-line {
    position: absolute;
    background-color: #0b2447;
    width: 100%;
    height: 2px;
    right: 0;
    top: 0;
    transition: all 0.5s;
  }
  
  .input-container .under-line {
    position: absolute;
    background-color: #0b2447;
    width: 0%;
    height: 2px;
    right: 0;
    bottom: 0;
    transition: all 0.5s;
  }
  
  .input.active ~ .top-line {
    width: 35%;
    transition: all 0.5s;
  }
  
  .input.active ~ .under-line {
    width: 100%;
    transition: all 0.5s;
  }
  
  .input.active ~ .label {
    top: -20px;
    transform: scale(1);
    transition: all 0.5s;
  }
  .row{
    margin:10px 40px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 10px; /* 控制两个 el-form-item 之间的间距 */
  }
  ::v-deep.form .el-form-item__label {
    color: #0b2447;         
    font-size: 18px;        
    font-weight: bold;      
    text-align: left;   
    vertical-align: middle; 
    line-height: 45px;
  }
  .icon {
    position: absolute;
    right: -25px; 
    top: 50%;
    scale:1.5;
    transform: translateY(-50%);
    cursor: pointer;
  }
  /* .button{
    margin-bottom: 10px;
  } */

  .attendanceButton {
  width: 300px; /* 原按钮宽度 */
  height: 100px; /* 原按钮高度 */
  font-size: 23px; /* 原字体大小 */
    
  padding: 1.3em 3em;
  margin-top: 20px;
  text-transform: uppercase;
  letter-spacing: 2.5px;
  font-weight: 500;
  color: #000;
  background-color: #fff;
  border: none;
  border-radius: 45px;
  box-shadow: 0px 8px 15px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease 0s;
  cursor: pointer;
  outline: none;
}

.attendanceButton-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%; /* 根据需要调整 */
}

 .attendanceButton:hover {
  background-color: #ffa4a4;
  box-shadow: 0px 15px 20px rgba(244, 71, 71, 0.313);
  color: #fff;
  transform: translateY(-7px);
}

.attendanceButton:active {
  transform: translateY(-1px);
}

.encouragement-text {
  text-align: center;
  margin-top: 100px;
  font-size: 35px;
  font-weight: bold;
  color: #f8c8c8;
}
</style>