<template>
    <div class="background">
        <div class="mid-container">
            <div class="img"></div>
            <span class="illustrate">支付过程可能出现的延迟是正常现象</span>
            <el-button 
                type="primary"
                :loading="isloading"
                :disabled="isloading"
                @click="paydetect"
            >
                {{ buttonText }}
            </el-button>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            isloading: false,
            buttonText: "我已支付"
        }
    },
    methods: {
        paydetect() {
            this.isloading = true;
            this.buttonText = "正在确认";

            // 定时器，两秒后执行的东西
            setTimeout(() => {
                this.isloading = false;
                this.buttonText = '我已支付';
                this.$router.go(-1);
            }, 2000);
        }
    }
}
</script>

<style scoped>
.background {
    display: flex;
    background-color: #f2f2f2;
    width: 100%;
    height: 750px;
    justify-content: center;
    align-items: center;
}

.mid-container {
    display: flex;
    background-color: #fff;
    width: 300px;
    height: 400px;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}

.img {
    width: 200px;
    height: 200px;
    background: url("../assets/payqr.png") no-repeat center center;
    background-size: cover;
}

.illustrate {
    width: 200px;
    margin-top: 10px;
    font-size: 12px;
    color: #777;
}

button {
    width: 200px;
    margin-top: 20px;
}
</style>