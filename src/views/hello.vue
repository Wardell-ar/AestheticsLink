<template>
    <div class="common-layout">
      <el-container>
        <el-header class="header">
          <div class="person-img">
            <img>
          </div>
          <div class='info'>
            <div class="user">
              <div class="user-name">
                <span>{{ nickname }}</span>
              </div>
              <div class="user-id">
                <span>{{ user_id }}</span>
              </div>
              <div class="user-level">
                <span>lv.{{ user_level }}</span>
              </div>
            </div>
            <el-button class='button' @click="create_dialog=true" type="primary">
              <el-icon><Edit /></el-icon>编辑
            </el-button>
          </div>
        </el-header>
        <el-container>
          <el-aside width="200px" class="aside">
            <h5 class="mb-2">个人中心</h5>
            <el-menu
              default-active="2"
              class="el-menu-vertical-demo"
              @select="handleSelect"
            >
              <el-menu-item index="1">
                <el-icon><location /></el-icon>
                <span>个人信息</span>
              </el-menu-item>
              <el-menu-item index="2" @click="change">
                <el-icon><icon-menu /></el-icon>
                <span>我的权益</span>
              </el-menu-item>
              <el-menu-item index="3" disabled>
                <el-icon><document /></el-icon>
                <span>Navigator Three</span>
              </el-menu-item>
              <el-menu-item index="4">
                <el-icon><setting /></el-icon>
                <span>Navigator Four</span>
              </el-menu-item>
            </el-menu>
          </el-aside>
          <el-main class="main">
            <component :is="currentComponent"></component>
          </el-main>
        </el-container>
      </el-container>
      <el-dialog
        style="z-index: 1000;"
        title="修改个人信息"
        v-model="create_dialog"
        width="800px">
        <el-form :model="form" :rules="rules" ref="form" label-width="150px">
          <div class="updateinfo">
            <div class="left">
              <el-form-item label="头像" prop="avatar">
                <img style="width:150px;height:110px" :src="form.avatar"></img>
              </el-form-item>
              <el-form-item label="账号密码" prop="password">
                <el-input v-model="form.password"></el-input>
              </el-form-item>
              <el-form-item label="昵称" prop="nickname">
                <el-input v-model="form.nickname"></el-input>
              </el-form-item>
              <el-form-item label="年龄" prop="age">
                <el-input v-model="form.age"></el-input>
              </el-form-item>
              <el-form-item label="性别" prop="sex">
                <el-switch
                  v-model="form.sex"
                  active-color="#13ce66"
                  inactive-color="#ff4949"
                  active-text="男"
                  inactive-text="女"
                  :active-value="1"
                  :inactive-value="0"
                >
                </el-switch>
              </el-form-item>
              <el-form-item label="生日" prop="birth">
                <el-date-picker
                  v-model="birthday"
                  type="date"
                  placeholder="选择你的生日"
                  :size="size"
                />
              </el-form-item>
            </div>
            <div class="right">
              <el-form-item label="用户编号" prop="id">
                <el-input v-model="form.id" disabled></el-input>
              </el-form-item>
              <el-form-item label="账号" prop="account">
                <el-input v-model="form.account" disabled></el-input>
              </el-form-item>
              <el-form-item label="地区" prop="area">
                <el-input v-model="form.area"></el-input>
              </el-form-item>
              <el-form-item label="兴趣爱好" prop="hobby">
                <el-input v-model="form.hobby"></el-input>
              </el-form-item>
              <el-form-item label="职业" prop="work">
                <el-input v-model="form.work"></el-input>
              </el-form-item>
              <el-form-item label="个性签名" prop="design">
                <el-input v-model="form.design"></el-input>
              </el-form-item>
              <el-form-item label="手机号码" prop="mobilePhoneNumber">
                <el-input v-model="form.mobilePhoneNumber"></el-input>
              </el-form-item>
            </div>
          </div>
        </el-form>
        <span slot="footer" class="dialog-footer">
          <el-button @click="create_dialog=false">取 消</el-button>
          <el-button type="primary" @click="submit">提 交</el-button>
        </span>
      </el-dialog>
    </div>
  </template>
  
  <script>
  import { ref } from 'vue';
  import MyBenefits from '../components/myBenefits.vue';
  
  export default {
    data() {
      return {
        nickname: 'hh',
        user_id: '1111',
        user_level: 5,
        create_dialog: false,
        currentComponent: 'MyBenefits', // 默认显示个人信息
        form: {
          avatar: "",
          password: "",
          nickname: "",
          age: Number,
          email: "",
          mobilePhoneNumber: "",
          sex: Number,
          id: Number,
          account: "",
          area: "",
          hobby: "",
          work: "",
          design: "",
        },
        rules: {
          nickname: [
            { required: true, message: "昵称不能为空", trigger: "blur" },
          ],
          password: [
            { required: true, message: "账号密码不能为空", trigger: "blur" },
          ],
        },
      }
    },
    methods: {
      handleSelect(index) {
        if (index === '1') {
          this.currentComponent = PersonalInfo;
        }
      },
      change() {
        this.currentComponent = MyBenefits;
      }
    }
  };
  </script>
  
  <style scoped>
  .header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    background-color: #f0f2f5;
    padding: 10px 20px;
  }
  .person-img img {
    width: 50px;
    height: 50px;
    border-radius: 50%;
  }
  .info {
    display: flex;
    align-items: center;
  }
  .user {
    margin-right: 20px;
  }
  .user-name {
    font-size: 18px;
    font-weight: bold;
  }
  .user-id,
  .user-level {
    font-size: 14px;
    color: #888;
  }
  .button {
    margin-left: 20px;
  }
  .aside {
    background-color: #f0f2f5;
  }
  .main {
    padding: 20px;
    background-color: #fff;
  }
  .dialog-footer {
    text-align: right;
  }
  .updateinfo {
    display: flex;
    justify-content: space-between;
  }
  .left,
  .right {
    width: 48%;
  }
  </style>