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

</template>


<script>
import {loginReq} from "../HTTP/http"
import { useRouter } from "vue-router";
export default {
    data() {
        return {
            UID: "",
            PSW: "",
            router: useRouter()
        }
    },

    methods: {
        loginHandle() {
            if (this.UID === "" || this.PSW === "") {
                alert("手机号与密码不能为空！");
                return;
            }
            loginReq(this.UID,this.PSW,this.handleLoginSuccess);
        },
        logupHandle() {
            // 跳转界面到注册界面
            this.router.push("/logup");
        },
        handleLoginSuccess(role) {
            if (role === "0") {
                alert("管理员登录成功！");
                // 跳转到管理员页面
            } else if (role === "1") {
                alert("客户登录成功！");
                // 跳转到客户页面
            } else if (role === "2") {
                alert("员工登录成功！");
                // 跳转到员工页面
            }
            else{
                this.UID = "";
                this.PSW = "";
            }
        }
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
    background: url("../assets/logo.svg") no-repeat center center;
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
</style>