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
            <el-button class='button' true @click="create_dialog=true" type="primary">
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
              @open="handleOpen"
              @close="handleClose"
            >
            <el-sub-menu index="1">
              <template #title>
                <el-icon><location /></el-icon>
                <span>Navigator One</span>
              </template>
              <el-menu-item-group title="Group One">
                <el-menu-item index="1-1">item one</el-menu-item>
                <el-menu-item index="1-2">item two</el-menu-item>
              </el-menu-item-group>
              <el-menu-item-group title="Group Two">
                <el-menu-item index="1-3">item three</el-menu-item>
              </el-menu-item-group>
              <el-sub-menu index="1-4">
                <template #title>item four</template>
                <el-menu-item index="1-4-1">item one</el-menu-item>
              </el-sub-menu>
            </el-sub-menu>
            <el-menu-item index="2">
              <el-icon><icon-menu /></el-icon>
              <span>Navigator Two</span>
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
                            :active-value= "1"
                            :inactive-value= "0"
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
              </el-main>
        </el-container>
      </el-container>
    </div>
</template>

<script>
import { ref } from 'vue'
const birthday = ref('')
export default {
  name:'Person',
  data() {
    return {
      nickname:'hh',
      user_id:'1111',
      user_level:5,
      create_dialog:false,
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
  methods:{
    edit(){
      this.dialogVisible=true;
      console.log(this.dialogVisible);
    },
    handleClose() {
      this.dialogVisible = false;
      // this.$emit("flesh");
    },
  }
}
</script>

<style scoped>
.person-img{
  width: 150px;
  height: 120px;
  background-color: #8c939d;
  margin-right: 24px;
  margin-left: 20px;
  overflow: hidden;
  border-radius: 20px;
}
.header{
  width: 100%;
  height: 220px;
  padding-top: 20px;
  background-color: white;
  margin-top: 0px;
  display: flex;
  border-radius: 5px;
  align-items: center;
}
.info{
  height: 120px;
  width: 880px;
  display: flex;
}
.user{
  width: 60%;
  height: 100%;
  line-height: 30px;
}
.user-name {
  font-weight: bold;
}
.user-level {
  margin-bottom: -5px;
  font-size: 15px;
  color: #00c3ff;
}

.aside{
  height: auto;
  border-radius: 5px;
  margin-right: 3%;
  text-align: center;
  width:30%;
  background-color:#d1edc4;
  align-content: center;
}
h5{
  width: 100%;
  height: 30px;
  margin-top: 5px;
  font-size: 22px;
  border-bottom: 1px solid #f0f0f0;
  background-image: -webkit-linear-gradient(
    left,
    rgb(42, 134, 141),
    #e9e625dc 20%,
    #3498db 40%,
    #e74c3c 60%,
    #09ff009a 80%,
    rgba(82, 196, 204, 0.281) 100%
  );
  -webkit-text-fill-color: transparent;
  -webkit-background-clip: text;
  -webkit-background-size: 200% 100%;
  -webkit-animation: masked-animation 4s linear infinite;
}
.el-menu-item {
  margin-top: 22px;
}
.main{
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
