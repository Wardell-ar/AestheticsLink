<template>
  <h1>按钮颜色</h1>
  <el-button>默认按钮</el-button>
  <el-button type="primary">主要按钮</el-button>
  <el-button type="success">成功按钮</el-button>
  <el-button type="info">信息按钮</el-button>
  <el-button type="warning">警告按钮</el-button>
  <el-button type="danger">危险按钮</el-button>

  <h1>按钮形状</h1>
  <el-button plain>朴素按钮</el-button>
  <el-button round>圆角按钮</el-button>
  <el-button circle>圆</el-button>
  <el-button disabled>禁用按钮</el-button>
  <el-button loading>加载中</el-button>

  <h1>按键大小</h1>
  <el-button size="large">大型按钮</el-button>
  <el-button>普通按钮</el-button>
  <el-button size="small">小型按钮</el-button>

  <h1>图标种类</h1>
  <el-icon>
    <Plus />
  </el-icon>
  <el-icon>
    <Delete />
  </el-icon>
  <el-icon>
    <Edit />
  </el-icon>
  <el-icon>
    <Loading />
  </el-icon>
  <el-icon>
    <Search />
  </el-icon>

  <h1>图标大小</h1>
  <el-icon size="30">
    <Search />
  </el-icon>
  <el-icon size="40">
    <Search />
  </el-icon>
  <el-icon size="50">
    <Search />
  </el-icon>

  <h1>图标颜色</h1>
  <el-icon color="black">
    <Delete />
  </el-icon>
  <el-icon color="red">
    <Delete />
  </el-icon>
  <el-icon color="blue">
    <Delete />
  </el-icon>

  <h1>将图标作为文本放在按钮中</h1>
  <el-button type="primary">
    <el-icon size="20">
      <Search />
    </el-icon>
    <span>搜索</span>
  </el-button>

  <h1>图标按钮组</h1>
  <el-button-group>
    <el-button type="primary">
      <el-icon size="20">
        <Plus />
      </el-icon>
    </el-button>
    <el-button type="primary">
      <el-icon size="20">
        <Edit />
      </el-icon>
    </el-button>
    <el-button type="primary">
      <el-icon size="20">
        <Delete />
      </el-icon>
    </el-button>
  </el-button-group>

  <h1>ElMessage函数使用</h1>
  <el-button @click="ElMessagetest">消息</el-button>
  <el-button @click="ElMessageBoxtest">确认框</el-button>
  <el-button @click="ElNotificationtest">通知</el-button>

  <h1>导航组件</h1>
  <el-menu mode="horizontal" default-active="2" @select="selected" background-color="#545c64" text-color="#fff"
    active-text-color="#ffd04b">
    <el-sub-menu index="1">
      <template #title>产品介绍</template>
      <el-menu-item index="1-1">产品1</el-menu-item>
      <el-menu-item index="1-2">产品2</el-menu-item>
      <el-menu-item index="1-3">产品3</el-menu-item>
    </el-sub-menu>
    <el-menu-item index="2">首页</el-menu-item>
    <el-menu-item index="3">帮助</el-menu-item>
  </el-menu>

  <h1>下拉菜单</h1>
  <el-dropdown @command="ueserCommand">
    <span>
      账号管理
      <el-icon>
        <User />
      </el-icon>
    </span>

    <template #dropdown>
      <el-dropdown-item command="change">切换账号</el-dropdown-item>
      <el-dropdown-item command="quit">退出登录</el-dropdown-item>
    </template>
  </el-dropdown>

  <h1>输入框</h1>
  <div class="inputcontainer">
    <el-input v-model="uid" clearable placeholder="用户名"></el-input>
  </div>
  <div class="inputcontainer">
    <el-input v-model="psw" show-password placeholder="密码"></el-input>
  </div>

  <h1>文本域</h1>
  <div class="inputcontainer">
    <el-input type="textarea" v-model="content" maxlength="20" :rows="10" show-word-limit></el-input>
  </div>

  <h1>输入的前置后置设置</h1>
  <div class="inputcontainer">
    <el-input v-model="url">
      <template #prepend>https://</template>
      <template #append>.com</template>
    </el-input>
  </div>

  <h1>搜索输入组</h1>
  <div class="inputcontainer">
    <el-input v-model="searchContent">
      <template #prepend>
        <el-select v-model="select" placeholder="请选择" style="width: 100px;">
          <el-option label="员工" value="1" />
          <el-option label="客户" value="2" />
        </el-select>
      </template>
      <template #append>
        <el-button @click="search">
          <el-icon>
            <Search />
          </el-icon>
        </el-button>
      </template>
    </el-input>
  </div>

  <h1>单选框组</h1>
  <el-radio-group v-model="radio" @change="radiochange">
    <el-radio value="1">选项1</el-radio>
    <el-radio value="2">选项2</el-radio>
    <el-radio value="3">选项3</el-radio>
  </el-radio-group>

  <h1>复选框组</h1>
  <el-checkbox-group v-model="checked" @change="checkboxchange">
    <el-checkbox value="1">选项1</el-checkbox>
    <el-checkbox value="2">选项2</el-checkbox>
    <el-checkbox value="3">选项3</el-checkbox>
  </el-checkbox-group>

  <h1>下拉框</h1>
  <div class="inputcontainer">
    <el-select v-model="choose" placeholder="请选择">
      <el-option value="1" label="选项1" />
      <el-option value="2" label="选项2" />
      <el-option value="3" label="选项3" />
    </el-select>
  </div>

  <h1>日期选择器</h1>
  <el-date-picker v-model="date" type="date" placeholder="请选择日期" />

  <h1>对话框</h1>
  <el-dialog v-model="dialog" width="400">
    <div class="flexcontainer">
      <span class="birthday">生日快乐</span>
      <el-button style="margin-top: 50px;" type="success" @click="recv">收到祝福</el-button>
    </div>
  </el-dialog>



  <!-- 用来兜底的 -->
  <div class="bottom"></div>
  <!-- 用来兜底的 -->
</template>

<script>
import { ElMessage, ElMessageBox, ElNotification } from 'element-plus'

export default {
  data() {
    return {
      uid: "",
      psw: "",
      content: "",
      searchContent: "",
      select: "",
      url: "",
      radio: "",
      checked: [],
      choose: "",
      date: "",
      dialog: true,

    }
  },
  watch: {
    date(n, o) {
      console.log(n.getFullYear());
      console.log(n.getMonth() + 1);
      console.log(n.getDate());
    }
  },
  methods: {
    ElMessagetest() {
      ElMessage({
        // 消息的样式 success | warning | info | error
        type: "success",

        // 这是显示的消息内容
        message: "成功登录",

        // 是否显示关闭按钮，无论是否显示，一定时间后都会自行关闭
        showClose: true
      });
    },
    ElMessageBoxtest() {
      ElMessageBox.confirm("主体内容", "标题", {
        type: "warning",   // 确认框类型
        confirmButtonText: "yes",  // 确认按键的文本信息
        cancelButtonText: "no"    // 取消按键的文本信息
      }).then(() => {  // 点击确认按键后执行
        console.log("点击确认按键成功");
      }).catch(() => {  // 点击取消按键或者点击屏幕其它部分后执行
        console.log("点击取消按键成功");
      });
    },
    ElNotificationtest() {
      ElNotification({
        type: "success",
        title: "标题",
        message: "通知内容",
        duration: 1500,  // 显示时间，单位为ms
        position: "bottom-right"  // 显示位置
      });
    },
    selected(key, keypath) {  // 该函数必须接收两个参数
      switch (key) {
        case "1-1":
          // 选中 1-1 选项需要的操作
          console.log("1-1");
          break;
        case "1-2":
          // 选中 1-2 选项需要的操作
          console.log("1-2");
          break;
        case "1-3":
          // 选中 1-3 选项需要的操作
          console.log("1-3");
          break;
        case "2":
          // 选中 2 选项需要的操作
          console.log("2");
          break;
        case "3":
          // 选中 3 选项需要的操作
          console.log("3");
          break;
      }
    },
    ueserCommand(command) {
      switch (command) {
        case "change":
          console.log("切换账号");
          break;
        case "quit":
          console.log("退出登录");
          break;
      }
    },
    search() {
      console.log(this.select);
      console.log(this.searchContent);
    },
    radiochange(val) {
      console.log(val);
    },
    checkboxchange(val) {
      console.log(val);
    },
    recv() {
      this.dialog = false;
    },
  }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Pacifico&display=swap');

.bottom {
  width: 100%;
  height: 300px;
}

.inputcontainer {
  margin-top: 10px;
  width: 300px;
}

.flexcontainer {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.birthday {
  font-family: 'Pacifico', cursive;
  font-size: 60px;
  text-shadow: 3px 3px 5px rgba(0, 0, 0, 0.3);
  background: linear-gradient(45deg, #ff9a9e, #fad0c4);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  margin: 20px;
  padding: 10px;
}
</style>