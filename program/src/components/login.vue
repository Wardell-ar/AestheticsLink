<template>
    <div class="Body">
        <div class="container">
            <div class="image-container"></div>
            <div class="form-container">
                <h1>登录</h1>
                <div class="form-group">
                    <p>手机号</p>
                    <input type="text" v-model="UID">
                </div>
                <div class="form-group">
                    <p>密码</p>
                    <input type="password" v-model="PSW">
                </div>
                <button @click="loginHandle">登录</button>
                <br>
                <br>
                <button @click="logupHandle">注册</button>
                <p class="terms">
                    点击“登录”即表示您同意<a href="#">条款</a>。有关我们如何处理您的个人数据的更多信息，请参阅我们的<a href="#">隐私政策</a>。
                </p>
            </div>
        </div>
    </div>
    <el-dialog :show-close="false" v-model="dialog" width="400">
        <div class="flexcontainer">
            <span class="birthday">生日快乐</span>
            <el-button style="margin-top: 50px;" type="success" @click="recv">收到祝福</el-button>
        </div>
    </el-dialog>
</template>


<script>
import { loginReq } from "../HTTP/http"
import { useRouter } from "vue-router";
import { get_id, get_role, set_id, set_role } from "../identification"
import { ElMessage } from "element-plus";
export default {
    data() {
        return {
            UID: "",
            PSW: "",
            router: useRouter(),
            dialog: false,
        }
    },

    methods: {
        // 点击登录按键
        loginHandle() {
            if (this.UID === "" || this.PSW === "") {
                ElMessage({
                    type: "warning",
                    message: "手机号与密码不能为空！",
                    showClose: false
                });
                return;
            }
            loginReq(this.UID, this.PSW, this.callback);
        },
        // 点击注册按键
        logupHandle() {
            // 跳转界面到注册界面
            this.router.push("/logup");
        },
        // 回调函数处理HTTP响应的数据
        callback(response) {
            set_id(response.id);
            set_role(response.role);
            if (get_role() === "0") {
                ElMessage({
                    type: "success",
                    message: "管理员登录成功！",
                    showClose: false
                });
                // 跳转到管理员页面
                this.router.push("/admin");
            } else if (get_role() === "1") {
                ElMessage({
                    type: "success",
                    message: "客户登录成功！",
                    showClose: false
                });
                this.IsBirthday(response.month, response.day);
            } else if (get_role() === "2") {
                ElMessage({
                    type: "success",
                    message: "员工登录成功！",
                    showClose: false
                });
                this.IsBirthday(response.month, response.day);
            }
            else {
                ElMessage({
                    type: "error",
                    message: "登录失败",
                    showClose: false
                });
                this.UID = "";
                this.PSW = "";
            }
        },
        // 检测今天是否是生日
        IsBirthday(month, day) {
            console.log(month);
            console.log(day);
            const m = new Date().getMonth() + 1;
            const d = new Date().getDate();
            if (month == m && day == d) {
                this.dialog = true;  // 显示生日祝福的对话框
            }
            else {
                if (get_role() == "1") {
                    this.router.push("/Person");
                }
                else {
                    this.router.push("/Worker");
                }
            }
        },
        // 用于接收祝福后的关闭对话框
        recv() {
            this.dialog = false;
            if (get_role() == "1") {
                this.router.push("/Person");
            }
            else {
                this.router.push("/Worker");
            }
        },
    }
}

</script>


<style scoped>
.Body {
    font-family: Arial, sans-serif;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    margin: 0;
    background-color: #f2f2f2;
}

.container {
    display: flex;
    background-color: #fff;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    overflow: hidden;
    max-width: 800px;
    width: 100%;
}

.image-container {
    flex: 1;
    background: url("../assets/logo.png") no-repeat center center;
    background-size: cover;
}

.form-container {
    flex: 1;
    padding: 40px;
}

h1 {
    margin: 0 0 20px;
    font-size: 24px;
}

.form-group {
    margin-bottom: 20px;
}

.form-group p {
    display: block;
    margin-bottom: 5px;
}

.form-group input {
    width: calc(100% - 20px);
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
}

.form-container button {
    width: 100%;
    padding: 10px;
    background-color: #007bff;
    color: #fff;
    border: none;
    border-radius: 5px;
    font-size: 16px;
    cursor: pointer;
}

.form-container button:hover {
    background-color: #0056b3;
}

.terms {
    font-size: 12px;
    color: #777;
}

.terms a {
    color: #007bff;
    text-decoration: none;
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