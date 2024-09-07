<template>
    <div class="common-layout">
        <el-container>
            <el-header class="header">
                <div class="person-img">
                    <img src="../../public/logo.png" alt="Logo" />
                </div>
                <div class='info'>
                    <div class="user-container">
                        <div class="user smooth-type">{{ customer.name }}</div>
                        <label class="user-label">姓名</label>
                    </div>
                    <div class="user-container">
                        <div class="user smooth-type">{{ customer.id }}</div>
                        <label class="user-label">账号</label>
                    </div>
                </div>
                <div class="user-level">
                    <span>会员等级：{{ customer.level }}</span>
                </div>
            </el-header>
            <el-container>
                <el-aside class="aside">
                    <div class="personcenter">个人中心</div>
                    <el-menu default-active="1" class="el-menu-vertical-demo" @select="selectMenu">
                        <el-menu-item index="1">
                            <el-icon>
                                <User />
                            </el-icon>
                            <span>个人信息</span>
                        </el-menu-item>
                        <router-link to="/Person/myBenefits">
                            <el-menu-item index="2">
                                <el-icon>
                                    <Wallet />
                                </el-icon>
                                <span>我的权益</span>
                            </el-menu-item>
                        </router-link>
                        <router-link to="/Person/orders">
                            <el-menu-item index="3">
                                <el-icon>
                                    <ShoppingCartFull />
                                </el-icon>
                                <span>我的订单</span>
                            </el-menu-item>
                        </router-link>
                    </el-menu>
                </el-aside>
                <el-main class="main">
                    <div class="container">
                        <div v-if="selectIndex === '1'">
                            <div class="title">
                                <label>个人信息</label>
                                <el-button class='button' @click="openOldPasswordDialog" type="primary" size="large">
                                    <i class="fas fa-pen-to-square icon1"></i>修改密码
                                </el-button>
                            </div>
                            <el-form :model="customer" :rules="rules" ref="customer" class="form ">
                                <div class="row">
                                    <el-form-item label="姓名" prop="name" class="name">
                                        <div class="input-container">
                                            <input type="text" v-model="customer.name" class="input" disabled>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                        </div>
                                    </el-form-item>
                                    <el-form-item label="账号" prop="id" class="id">
                                        <div class="input-container">
                                            <input type="text" v-model="customer.id" class="input" disabled>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                        </div>
                                    </el-form-item>
                                </div>
                                <div class="row">
                                    <el-form-item label="会员" prop="level" class="phone">
                                        <div class="input-container">
                                            <input type="text" v-model="displayLevel" class="input" disabled>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                        </div>
                                    </el-form-item>
                                    <el-form-item label="性别" prop="sex" class="sex">
                                        <div class="input-container">
                                            <input v-model="displaySex" type="text" class="input" disabled>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                        </div>
                                    </el-form-item>
                                </div>
                                <div class="row">
                                    <el-form-item label="生日" prop="birth" class="birth">
                                        <div class="input-container">
                                            <input v-model="customer.birthday" type="date" class="input" disabled>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                        </div>
                                    </el-form-item>
                                    <el-form-item label="余额" prop="balance" class="balance">
                                        <div class="input-container">
                                            <input v-model="customer.balance" type="text" class="input" disabled>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                        </div>
                                    </el-form-item>
                                    <el-button class='recharge' @click="isRecharge = true" type="primary" size="large">
                                        <i class="fas fa-donate icon1"></i>充值
                                    </el-button>
                                </div>
                            </el-form>
                            <el-dialog title="输入充值金额" v-model="isRecharge" width="30%" class="dialog"
                                style="  background-color: rgb(255, 250, 235)">
                                <el-form ref="rechargeForm" :model="rechargeForm">
                                    <el-form-item prop="amount">
                                        <div class="input-container">
                                            <input v-model="rechargeForm.amount" @focus="isEditing = true"
                                                @blur="isEditing = false" placeholder="请输入充值金额" type="text"
                                                :class="{ 'input': true, 'active': isEditing }">
                                            <label class="label">充值金额</label>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                            <i v-if="isEditing" class="fa fa-edit icon"></i>
                                        </div>
                                    </el-form-item>
                                </el-form>
                                <div slot="footer" class="dialog-footer">
                                    <el-button @click="isRecharge = false">取消</el-button>
                                    <el-button type="primary" @click="confirmRecharge">确定</el-button>
                                </div>
                            </el-dialog>
                            <el-dialog title="输入旧密码" v-model="isOldPasswordDialogVisible" width="30%" class="dialog"
                                style="  background-color: rgb(255, 250, 235)">
                                <el-form ref="oldPasswordForm" :model="oldPasswordForm" :rules="rules">
                                    <el-form-item prop="oldPassword">
                                        <div class="input-container">
                                            <input v-model="oldPasswordForm.oldPassword" @focus="isEditing = true"
                                                @blur="isEditing = false" placeholder="请输入旧密码" type="password"
                                                :class="{ 'input': true, 'active': isEditing }">
                                            <label class="label">旧密码</label>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                            <i v-if="isEditing" class="fa fa-edit icon"></i>
                                        </div>
                                    </el-form-item>
                                </el-form>
                                <div slot="footer" class="dialog-footer">
                                    <el-button @click="closeOldPasswordDialog">取消</el-button>
                                    <el-button type="primary" @click="checkOldPassword">确定</el-button>
                                </div>
                            </el-dialog>
                            <el-dialog title="输入新密码" v-model="isNewPasswordDialogVisible" width="30%" class="dialog"
                                style="  background-color: rgb(255, 250, 235)">
                                <el-form ref="newPasswordForm" :model="newPasswordForm" :rules="newPasswordRules">
                                    <el-form-item prop="newPassword">
                                        <div class="input-container">
                                            <input v-model="newPasswordForm.newPassword" @click="isEditing = true"
                                                @blur="isEditing = false" placeholder="请输入新密码" type="password"
                                                :class="{ 'input': true, 'active': isEditing }" :readonly="!isEditing">
                                            <label class="label">新密码</label>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                            <i v-if="isEditing" class="fa fa-edit icon"></i>
                                        </div>
                                    </el-form-item>
                                    <el-form-item prop="confirmPassword">
                                        <div class="input-container">
                                            <input v-model="newPasswordForm.confirmPassword" @focus="isEditing = true"
                                                @blur="isEditing = false" placeholder="请再次输入新密码" type="password"
                                                :class="{ 'input': true, 'active': isEditing }" :readonly="!isEditing">
                                            <label class="label">确认新密码</label>
                                            <span class="top-line"></span>
                                            <span class="under-line"></span>
                                            <i v-if="isEditing" class="fa fa-edit icon"></i>
                                        </div>
                                    </el-form-item>
                                </el-form>
                                <div slot="footer" class="dialog-footer">
                                    <el-button @click="closeNewPasswordDialog">取消</el-button>
                                    <el-button type="primary" @click="saveNewPassword">保存</el-button>
                                </div>
                            </el-dialog>
                        </div>
                        <router-view v-else></router-view>
                    </div>
                </el-main>
            </el-container>
        </el-container>
    </div>
</template>

<script scoped>
import { ref } from 'vue';
import { ElMessage } from 'element-plus';
import { getClientInfo, editClientPassword ,sendRecharge } from '../HTTP/http';
import { get_id } from "../identification"

export default {
    name: 'Person',
    data() {
        return {
            selectIndex: "1",
            isEditing: false,
            isOldPasswordDialogVisible: false,
            isNewPasswordDialogVisible: false,
            isRecharge: false,
            oldPasswordForm: { oldPassword: '' },
            newPasswordForm: { newPassword: '', confirmPassword: '' },
            rechargeForm: { amount: '' },
            customer: {//客户信息
                level: '',
                password: "",
                name: '',
                sex: '',
                id: '',
                birthday: "",
                balance: "",
            },
            rules: {
                oldPassword: [
                    { required: true, message: "密码不能为空", trigger: "blur" },
                ],
            },
            newPasswordRules: {
                newPassword: [
                    { required: true, message: '请输入新密码', trigger: 'blur' },
                ],
                confirmPassword: [
                    { required: true, message: '请再次输入新密码', trigger: 'blur' },
                    {
                        validator: (rule, value, callback) => {
                            if (value !== this.newPasswordForm.newPassword) {
                                callback(new Error('两次输入的新密码不一致'));
                            } else {
                                callback();
                            }
                        },
                        trigger: 'blur'
                    }
                ]
            },
        }
    },
    mounted() {
        this.originalCustomer = { ...this.customer };
        console.log(this.customer.id);
        ElMessage({
            message: '欢迎来到用户个人中心！',
            type: 'success',
        });
        // 获取并填入数据
        getClientInfo(get_id(), this.setClientData);
    },
    computed: {
        displaySex: {
            get() {
                return this.customer.sex === '1' ? '女' : '男';
            }
        },
        displayLevel: {
            get() {
                return this.customer.level;
            }
        }
    },
    methods: {
        isNumeric(value) {
            return !isNaN(value) && !isNaN(parseFloat(value));
        },
        selectMenu(index) {
            this.selectIndex = index;
            console.log(this.selectIndex);
        },
        confirmRecharge() {
            if(this.isNumeric(this.rechargeForm.amount)){
                let money = Number(this.rechargeForm.amount);
                sendRecharge(get_id(), money, this.chargeVerdict);
            }
            else{
                ElMessage.error("输入内容不可用");
            }
            this.isRecharge = false;
        },
        chargeVerdict(response){
            if(response == "1"){
                this.customer.balance = Number(this.customer.balance) + Number(this.rechargeForm.amount) + "";
                ElMessage.success("充值成功");
            }
            else{
                ElMessage.error("充值失败");
            }
            this.rechargeForm.amount='';
        },
        openOldPasswordDialog() {
            this.isOldPasswordDialogVisible = true;
            this.oldPasswordForm.oldPassword = '';
        },
        closeOldPasswordDialog() {
            this.isOldPasswordDialogVisible = false;
        },
        checkOldPassword() {
            if (this.oldPasswordForm.oldPassword === this.customer.password) {
                this.isOldPasswordDialogVisible = false;
                this.isNewPasswordDialogVisible = true;
                this.newPasswordForm.newPassword = '';
                this.newPasswordForm.confirmPassword = '';
            } else {
                ElMessage.error('旧密码错误');
                this.isOldPasswordDialogVisible = false;
            }
        },
        closeNewPasswordDialog() {
            this.isNewPasswordDialogVisible = false;
        },
        saveNewPassword() {
            if (this.newPasswordForm.newPassword === this.newPasswordForm.confirmPassword) {
                //修改密码
                editClientPassword(this.customer.id, this.newPasswordForm.newPassword, this.IsNewpwdSaved);
            } else {
                ElMessage.error('两次输入的新密码不一致');
            }
        },
        IsNewpwdSaved(response) {
            if (response == "1") {
                this.customer.password = this.newPasswordForm.newPassword;
                ElMessage.success('密码修改成功');
                this.isNewPasswordDialogVisible = false;
            }
            else {
                ElMessage.error('网络异常，请稍后重试');
            }
        },
        setClientData(response) {
            this.customer.level = response.vipLevel;
            this.customer.password = response.password;
            this.customer.name = response.name;
            this.customer.sex = (response.gender == "男") ? 0 : 1;
            this.customer.id = response.phoneNum;
            this.customer.balance = response.money + "";
            let year = response.year;
            let month = response.month;
            let day = response.day;
            if (month.length == 1) {
                month = '0' + month;
            }
            if (day.length == 1) {
                day = '0' + day;
            }
            this.customer.birthday = ref(year + '-' + month + '-' + day);
        }
    }
}
</script>

<style scoped>
.common-layout {
    width: 100%;
}

.person-img {
    width: 150px;
    height: 150px;
    margin-right: 24px;
    margin-left: 60px;
    overflow: hidden;
    position: relative;
    border-radius: 20px;
}

.person-img img {
    position: absolute;
    top: 50%;
    left: 50%;
    display: block;
    width: 100%;
    height: 100%;
    transform: translate(-50%, -50%);
}

.header {
    width: 100%;
    height: 220px;
    padding-top: 20px;
    background-color: white;
    margin-top: 0px;
    display: flex;
    border-radius: 5px;
    align-items: center;
    justify-content: space-around;
    --color: #E1E1E1;
    background-color: #F3F3F3;
    background-image: linear-gradient(0deg, transparent 24%, var(--color) 25%, var(--color) 26%, transparent 27%, transparent 74%, var(--color) 75%, var(--color) 76%, transparent 77%, transparent),
        linear-gradient(90deg, transparent 24%, var(--color) 25%, var(--color) 26%, transparent 27%, transparent 74%, var(--color) 75%, var(--color) 76%, transparent 77%, transparent);
    background-size: 55px 55px;
}

.info {
    height: auto;
    width: auto;
    margin: auto 40px;
    padding: 0 20px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;
}

.user-level {
    margin-bottom: -5px;
    font-size: 18px;
    color: #00c3ff;
    background: -webkit-linear-gradient(90deg, #c7f2ff, #5cd3ff);
    background: linear-gradient(90deg, #c7f2ff, #5cd3ff);
    background-repeat: no-repeat;
    border-radius: 25px;
    border-color: transparent;
    width: 200px;
    height: 50px;
    padding: 20px;
    display: flex;
    justify-content: center;
    align-items: center;
    text-align: center;
    font-weight: 500;
    font-family: inherit;
    box-shadow: 23px 23px 46px #bebebe,
        -23px -23px 46px #ffffff;
    color: white;
    margin-right: 50px;
}

.aside {
    height: auto;
    border-radius: 5px;
    text-align: center;
    width: 20%;
    background-color: #c6e2ff;
    align-content: center;
    display: flex;
    flex-direction: column;
}

.icon1 {
    margin-right: 0.5rem;
}

.personcenter {
    width: 100%;
    height: 50px;
    margin-top: 20px;
    margin-bottom: 100px;
    font-size: 22px;
    border-bottom: 1px solid #f0f0f0;
    color: black;
}

.main {
    width: 80%;
    min-height: 500px;
    height:auto;
    border-radius: 5px;
    background-color: white;
    --color1: hwb(210 42% 9%);
    --color2: #d9ecff;
    background-color: var(--color1);
    background-image: linear-gradient(45deg, var(--color2) 25%, transparent 25%, transparent 75%, var(--color2) 75%, var(--color2)),
        linear-gradient(45deg, var(--color2) 25%, var(--color1) 25%, var(--color1) 75%, var(--color2) 75%, var(--color2));
    background-size: 60px 60px;
    background-position: 0 0, 30px 30px;
    display: flex;
    align-items: center;
    justify-content: center;
}


a {
    text-decoration: none;
    color: inherit;
}

.router-link-active {
    text-decoration: none;
}

.user-container {
    position: relative;
    width: 250px;
    font-family: monospace;
    height: 45px;
    margin: 20px 0;
}

.user {
    width: 100%;
    height: 100%;
    padding: 15px;
    font-size: 18px;
    font-weight: bold;
    color: #000;
    background-color: #fff;
    border: 4px solid #000;
    position: relative;
    overflow: hidden;
    border-radius: 0;
    outline: none;
    transition: all 0.3s cubic-bezier(0.25, 0.8, 0.25, 1);
    box-shadow: 5px 5px 0 #000, 10px 10px 0 #4a90e2;
    cursor: pointer;
    display: flex;
    align-items: center;
}

@keyframes glitch {
    0% {
        transform: translate(0);
    }

    20% {
        transform: translate(-2px, 2px);
    }

    40% {
        transform: translate(-2px, -2px);
    }

    60% {
        transform: translate(2px, 2px);
    }

    80% {
        transform: translate(2px, -2px);
    }

    100% {
        transform: translate(0);
    }
}

.user:hover {
    animation: focus-pulse 4s cubic-bezier(0.25, 0.8, 0.25, 1) infinite,
        glitch 0.3s cubic-bezier(0.25, 0.8, 0.25, 1) infinite;
}

.user:hover::after {
    content: "";
    position: absolute;
    top: -2px;
    left: -2px;
    right: -2px;
    bottom: -2px;
    background: white;
    z-index: -1;
}

.user:hover::before {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: black;
    z-index: -2;
    clip-path: inset(0 100% 0 0);
    animation: glitch-slice 4s steps(2, end) infinite;
}

@keyframes glitch-slice {
    0% {
        clip-path: inset(0 100% 0 0);
    }

    10% {
        clip-path: inset(0 5% 0 0);
    }

    20% {
        clip-path: inset(0 80% 0 0);
    }

    30% {
        clip-path: inset(0 10% 0 0);
    }

    40% {
        clip-path: inset(0 50% 0 0);
    }

    50% {
        clip-path: inset(0 30% 0 0);
    }

    60% {
        clip-path: inset(0 70% 0 0);
    }

    70% {
        clip-path: inset(0 15% 0 0);
    }

    80% {
        clip-path: inset(0 90% 0 0);
    }

    90% {
        clip-path: inset(0 5% 0 0);
    }

    100% {
        clip-path: inset(0 100% 0 0);
    }
}

.user-label {
    position: absolute;
    left: -3px;
    top: -28px;
    font-size: 14px;
    font-weight: bold;
    color: #fff;
    background-color: #000;
    padding: 5px 10px;
    transform: rotate(-1deg);
    z-index: 1;
    transition: all 0.3s cubic-bezier(0.25, 0.8, 0.25, 1);
}

.user:hover+.user-label {
    transform: rotate(0deg) scale(1.05);
    background-color: #4a90e2;
    cursor: pointer;
}

.smooth-type {
    position: relative;
    overflow: hidden;
}

.smooth-type::before {
    content: "";
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    background: linear-gradient(90deg, #fff 0%, rgba(255, 255, 255, 0) 100%);
    z-index: 1;
    opacity: 0;
    transition: opacity 0.3s ease;
}

.smooth-type:hover::before {
    opacity: 1;
    animation: type-gradient 2s linear infinite;
}

@keyframes type-gradient {
    0% {
        background-position: 300px 0;
    }

    100% {
        background-position: 0 0;
    }
}

.user:hover {
    animation: focus-pulse 4s cubic-bezier(0.25, 0.8, 0.25, 1) infinite;
}

@keyframes focus-pulse {

    0%,
    100% {
        border-color: #000;
    }

    50% {
        border-color: #4a90e2;
    }
}

a:hover {
    text-decoration: none;
}

.container {
    width: 100%;
    margin: auto 30px;
    border-radius: 19px 19px 0px 0px;
    background: rgb(255, 250, 235);
    min-height: 450px;
    height:auto;
    box-shadow: 0px 187px 75px rgba(0, 0, 0, 0.01), 0px 105px 63px rgba(0, 0, 0, 0.05), 0px 47px 47px rgba(0, 0, 0, 0.09), 0px 12px 26px rgba(0, 0, 0, 0.1), 0px 0px 0px rgba(0, 0, 0, 0.1);
}

.dialog {
    box-shadow: 0px 187px 75px rgba(0, 0, 0, 0.01), 0px 105px 63px rgba(0, 0, 0, 0.05), 0px 47px 47px rgba(0, 0, 0, 0.09), 0px 12px 26px rgba(0, 0, 0, 0.1), 0px 0px 0px rgba(0, 0, 0, 0.1);
}

.dialog .el-dialog__title {
    border-bottom: 1px solid rgba(16, 86, 82, .75);

}

.form {
    margin-top: 0;
    width: 100%;
    height: 350px;
    display: flex;
    padding: 0;
    flex-direction: column;
}

.title {
    width: 100%;
    height: 40px;
    position: relative;
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding-left: 20px;
    border-bottom: 1px solid rgba(16, 86, 82, .75);
    font-weight: 700;
    font-size: 20px;
    color: #000000;
    margin: 20px auto;
}

.el-form-item {
    position: relative;
}

.input-container {
    position: relative;
    min-width: 220px;
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
    height: auto;
    transition: all 0.5s;
    transform: scale(0);
    z-index: 0;
    font-size: 18px;
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

.input.active~.top-line {
    width: 35%;
    transition: all 0.5s;
}

.input.active~.under-line {
    width: 100%;
    transition: all 0.5s;
}

.input.active~.label {
    top: -20px;
    transform: scale(1);
    transition: all 0.5s;
}

.row {
    margin: 10px 40px;
    display: flex;
    align-items: center;
    gap: 0px;
}

.row> :first-child {
    margin-right: 120px;
}

.recharge {
    margin-left: 30px;
    margin-bottom: 10px;
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
    scale: 1.5;
    transform: translateY(-50%);
    cursor: pointer;
}

.button {
    margin-bottom: 10px;
}
</style>