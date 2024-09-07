<template>
    <div class="bkg">
        <div class="midcontainer">
            <div class="item">
                <h2>注册</h2>
            </div>
            <div class="item">
                <span>姓名</span>
                <div class="inputcontainer">
                    <el-input v-model="NAME" clearable placeholder="请输入真实姓名"></el-input>
                </div>
            </div>
            <div class="item">
                <span>手机号</span>
                <div class="inputcontainer">
                    <el-input v-model="UID" clearable placeholder="请输入手机号码"></el-input>
                </div>
            </div>
            <div class="item">
                <span>性别</span>
                <div class="inputcontainer">
                    <el-select v-model="genderChoose" placeholder="男/女">
                        <el-option value="1" label="男" />
                        <el-option value="2" label="女" />
                    </el-select>
                </div>
            </div>
            <div class="item">
                <span>出生日期</span>
                <div class="inputcontainer">
                    <el-date-picker v-model="DATE" type="date" placeholder="请选择日期" />
                </div>
            </div>
            <div class="item">
                <span>设置密码</span>
                <div class="inputcontainer">
                    <el-input v-model="PSW1" show-password clearable placeholder="请设置密码"></el-input>
                </div>
            </div>
            <div class="item">
                <span>密码二次确认</span>
                <div class="inputcontainer">
                    <el-input v-model="PSW2" show-password clearable placeholder="请重复输入密码"></el-input>
                </div>
                <span class="smalltext" :style="{ color: msgcolor }">{{ msg }}</span>
            </div>
            <div class="item">
                <el-button type="primary" @click="logupHandle" style="width: 220px;">注册</el-button>
            </div>
        </div>
    </div>

</template>

<script>
import { ElMessage } from 'element-plus';
import { logupReq } from '@/HTTP/http';
import { useRouter } from 'vue-router';
export default {
    data() {
        return {
            NAME: "",
            UID: "",
            genderChoose: null,
            DATE: "",
            PSW1: "",
            PSW2: "",
            msgcolor: "",
            router: useRouter()
        }
    },
    computed: {
        GENDER() {
            if (this.genderChoose == "1") {
                return "男";
            }
            else if (this.genderChoose == "2") {
                return "女";
            }
            else {
                return "";
            }
        },
        YEAR() {
            if (this.DATE != "" && this.DATE != null) {
                return this.DATE.getFullYear() + "";  // 转为字符串
            }
            return "";
        },
        MONTH() {
            if (this.DATE != "" && this.DATE != null) {
                return this.DATE.getMonth() + 1 + "";  // 转为字符串
            }
            return "";
        },
        DAY() {
            if (this.DATE != "" && this.DATE != null) {
                return this.DATE.getDate() + "";  // 转为字符串
            }
            return "";
        },
        msg() {
            if (this.PSW2 != "" && this.PSW2 != null) {
                if (this.PSW1 == this.PSW2) {
                    this.msgcolor = "green";
                    return "密码一致";
                }
                this.msgcolor = "red";
                return "密码不一致";
            }
            return "";
        }
    },
    methods: {
        logupHandle() {
            if (this.Isvalid(this.NAME) && this.Isvalid(this.GENDER) && this.Isvalid(this.DATE) && this.Isvalid(this.UID) && this.Isvalid(this.PSW1) && this.Isvalid(this.PSW2)) {
                if (this.PSW1 == this.PSW2) {
                    logupReq(this.NAME, this.GENDER, this.YEAR, this.MONTH, this.DAY, this.UID, this.PSW1, this.callback);
                }
                else {
                    ElMessage({
                        type: "warning",
                        message: "密码不一致！",
                        showClose: false
                    });
                }
            }
            else {
                ElMessage({
                    type: "warning",
                    message: "输入内容不能为空！",
                    showClose: true
                });
            }
        },
        Isvalid(val) {
            if (val != "" && val != null) {
                return true;
            }
            return false;
        },
        // 注册信息录入成功后，回调函数，进行页面跳转
        callback() {
            this.router.push("/");
        }
    },
}

</script>

<style scoped>
.bkg {
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: #f2f2f2;
    width: 100%;
    height: 750px;
}

.midcontainer {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background-color: #fff;
    width: 550px;
    height: 650px;
}

.inputcontainer {
    width: 220px;
    margin-top: 10px;
}

.item {
    flex: 1;
    margin-top: 10px;
}

.smalltext {
    font-size: 12px;
}
</style>