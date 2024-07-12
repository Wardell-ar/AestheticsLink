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
                <span>{{ form.nickname }}</span>
              </div>
              <div class="user-id">
                <span>{{ form.id }}</span>
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
              default-active="1"
              class="el-menu-vertical-demo"
              @select="selectMenu"
            >
            <el-menu-item index="1">
              <el-icon><User /></el-icon>
                <span>个人信息</span>
            </el-menu-item>
            <el-menu-item index="2">
              <el-icon><Wallet /></el-icon>
              <span>我的权益</span>
            </el-menu-item>
            <el-sub-menu index="3">
              <template #title>
                <el-icon><ShoppingCartFull /></el-icon>
                <span>我的项目</span>
              </template>
              <el-menu-item index="4-1">
                <el-icon><List /></el-icon>
                <span>未完成</span>
              </el-menu-item>
              <el-menu-item index="4-2">
                <el-icon><SuccessFilled /></el-icon>
                <span>已完成</span>
              </el-menu-item>
            </el-sub-menu>
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
                        <el-form-item label="账号密码" prop="password" >
                          <el-input v-model="form.password" type="password" show-password></el-input>
                        </el-form-item>
                        <el-form-item label="昵称" prop="nickname">
                          <el-input v-model="form.nickname"></el-input>
                        </el-form-item>
                        <el-form-item label="年龄" prop="age">
                          <el-input v-model="form.age"></el-input>
                        </el-form-item>
                          <el-form-item label="生日" prop="birth">
                              <el-date-picker
                                v-model="form.birthday"
                                type="date"
                                placeholder="选择你的生日"
                                :size="size"
                              />
                          </el-form-item>
                        </div>
                      <div class="right">
                        <el-form-item label="真实姓名" prop="name">
                          <el-input v-model="form.name"></el-input>
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
                        <el-form-item label="用户编号" prop="id">
                          <el-input v-model="form.id" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="账号" prop="account">
                          <el-input v-model="form.account" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="住址" prop="area">
                          <el-input v-model="form.area"></el-input>
                        </el-form-item>
                        <el-form-item label="职业" prop="work">
                          <el-input v-model="form.work"></el-input>
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
                <el-card v-if="selectIndex === '1'" class="infoCard">
                  <el-descriptions class="margin-top" title="个人信息" :column="2" border>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        <el-icon><Avatar /></el-icon>
                        头像
                      </template>
                      <img class="img" :src="form.avatar" alt="" />
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        <el-icon><User /></el-icon>                        
                        用户编号
                      </template>
                      {{ form.id }}
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        <el-icon><UserFilled /></el-icon>
                        账号
                      </template>
                      {{ form.account }}
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        昵称
                      </template>
                      {{ form.nickname }}
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        年龄
                      </template>
                      {{ form.age }}
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        <el-icon><Female /></el-icon>
                        <el-icon><Male /></el-icon>
                        性别
                      </template>
                      <el-tag size="small">{{ form.sex === 1 ? '男' : '女' }}</el-tag>
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        <el-icon><PhoneFilled /></el-icon>
                        手机号码
                      </template>
                      {{ form.mobilePhoneNumber }}
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        <el-icon><Place /></el-icon>
                        住址
                      </template>
                      {{ form.area }}
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        <el-icon><InfoFilled /></el-icon>
                        注册日期
                      </template>
                      {{ createDate | formatDate }}
                    </el-descriptions-item>
                    <el-descriptions-item label-align="center",align="center">
                      <template #label>
                        <el-icon><Calendar /></el-icon>
                        生日
                      </template>
                      {{ form.birthday }}
                    </el-descriptions-item>
                  </el-descriptions>
                </el-card>
              </el-main>
        </el-container>
      </el-container>
    </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name:'Person',
  data() {
    return {
      selectIndex:"1",
      user_level:5,
      create_dialog:false,
      form: {
        avatar: "",
        password: "111",
        nickname: "hh",
        name:'111',
        age: 12,
        mobilePhoneNumber: "11111111111",
        sex: 1,
        id: '1111',
        account: "111111111111",
        area: "1111111111",
        birthday:ref('')
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
    selectMenu(index){
      this.selectIndex=index;
      console.log(this.selectIndex);
    }
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
