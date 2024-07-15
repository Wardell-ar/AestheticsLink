<template>
    <div class="Body">
        <div class="container">
        <div class="image-container"></div>
        <div class="form-container">
            <h1>登录</h1>
            <div class="form-group">
                <p>手机号</p>
                <input type="text" id="full-name" name="full-name" v-model="UID" required>
            </div>
            <div class="form-group">
                <p>密码</p>
                <input type="password" id="password" name="password" v-model="PSW" required>
            </div>
            <button type="submit" @click="loginHandle">登录</button>
            <p class="terms">
                点击“登录”即表示您同意<a href="#">条款</a>。有关我们如何处理您的个人数据的更多信息，请参阅我们的<a href="#">隐私政策</a>。
            </p>
        </div>
    </div>
    </div>

</template>


<script>
import router from "@/router";
export default{
    data(){
        return {
            UID: "",
            PSW: ""
        }
    },
    name:'login',

    methods:{
        loginHandle(){
            let obj = {
                uid: this.UID,
                psw: this.PSW
            };
            let str = JSON.stringify(obj);  // 发送的序列化数据
            const xhr = new XMLHttpRequest();
            xhr.open('POST','http://100.79.204.71:5290/api/Login/CheckLogin',true);
            xhr.setRequestHeader('Content-Type', 'application/json');
            xhr.send(str);
            xhr.onreadystatechange = () => {
                if (xhr.readyState === 4) { // 服务器已完成发送
                    if (xhr.status >= 200 && xhr.status < 300) { // 服务器发送成功
                        const response = JSON.parse(xhr.responseText);
                         if (response.token) { // 如果有token返回
                            localStorage.setItem('token', response.token); // 存储token
                            this.handleLoginSuccess(response.role);
                        } 
                        else {
                            alert("登录验证失败");
                        }
                    }
                }
            };
        },
        handleLoginSuccess(role) {
            if (role === 0) {
                alert("管理员登录成功！");
                // 跳转到管理员页面
            } else if (role === 1) {
                alert("客户登录成功！");
                router.push({ name: "Person" });
                // 跳转到客户页面
            } else if (role === 2) {
                alert("员工登录成功！");
                // 跳转到员工页面
            }
            else{
                alert("登录失败")
            }
        }
    }
};

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