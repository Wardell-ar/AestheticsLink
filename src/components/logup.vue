<template>
    <div class="Body">
        <div class="container">
        <div class="image-container"></div>
        <div class="form-container">
            <h1>注册</h1>
            <div class="form-group">
                <p>姓名</p>
                <input type="text" v-model="NAME" required>
            </div>
            <div class="form-group">
                <p>性别</p>
                <input type="text" v-model="SEX" required>
            </div>
            <div class="form-group">
                <p>出生年</p>
                <input type="text" v-model="YEAR" required>
            </div>
            <div class="form-group">
                <p>出生月</p>
                <input type="text" v-model="MONTH" required>
            </div>
            <div class="form-group">
                <p>出生日</p>
                <input type="text" v-model="DAY" required>
            </div>
            <div class="form-group">
                <p>手机号</p>
                <input type="text" v-model="UID" required>
            </div>
            <div class="form-group">
                <p>设置密码</p>
                <input type="password" v-model="PSW1" required>
            </div>
            <div class="form-group">
                <p>密码二次确认</p>
                <input type="password" v-model="PSW2" required>
                <p class="terms" :style="{ color: msgcolor }">{{ msg }}</p>
            </div>
            <button @click="logupHandle">注册</button>
            <p class="terms">
                点击“登录”即表示您同意<a href="#">条款</a>。有关我们如何处理您的个人数据的更多信息，请参阅我们的<a href="#">隐私政策</a>。
            </p>
        </div>
    </div>
    </div>

</template>


<script>
export default{
    data(){
        return {
            NAME: "",
            SEX: "",
            YEAR: "",
            MONTH: "",
            DAY: "",
            UID: "",
            PSW1: "",
            PSW2: "",

            msg: "",
            msgcolor:""
        }
    },

    methods:{
        logupHandle(){
            if(this.NAME!=""&&this.SEX!=""&&this.YEAR!=""&&this.MONTH!=""&&this.DAY!=""&&this.UID!=""&&this.PSW1!=""&&this.PSW2!=""){
                if(this.PSW1 === this.PSW2){
                    let obj = {
                        name: this.NAME,
                        sex: this.SEX,
                        year: this.YEAR,
                        month: this.MONTH,
                        day: this.DAY,
                        uid: this.UID,
                        psw: this.PSW1
                    };
                    let str = JSON.stringify(obj);
                    const xhr = new XMLHttpRequest();  // 创建实例
                    xhr.open('POST','http://100.78.214.7:5287/Login/CheckLogin/checklogin',true);  // 请求行（请求类型、目的IP）
                    xhr.setRequestHeader('Content-Type', 'application/json');  // 请求头（传输内容...）
                    xhr.send(str);  // 请求体（发送的内容，只有POST请求可以发送内容）
                    // 对服务器响应的处理
                    xhr.onreadystatechange = function(){
                        if(xhr.readyState === 4){
                            if(xhr.status >= 200 && xhr.status < 300){
                                if(xhr.response === "0"){  // 注册失败
                                    alert("注册失败！");
                                }
                                else if(xhr.response === "1"){
                                    alert("注册成功，请重新登录！");
                                    // 跳转到登录界面
                                    // ......
                                }
                            }
                        }
                    }
                }
                else{
                    alert("密码不一致！");
                }
            }
            else{
                alert("输入内容不能为空！");
            }
        }
    },

    watch:{
        PSW2(newValue,oldValue){
            if(newValue === this.PSW1){
                this.msg = "密码一致";
                this.msgcolor = 'green'
            }
            else {
                this.msg = "密码不一致";
                this.msgcolor = 'red';
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
        height: 120vh;
        margin: 0;
        background-color: #f2f2f2;
    }
    .container {
        display: flex;
        background-color: #fff;
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        overflow: hidden;
        max-width: 1000px;
        height: 850px;
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