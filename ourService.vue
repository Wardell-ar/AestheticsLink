<template>
    <div class="services">
        <div class="header">
            <div class="sub-header">OUR BEST SERVICE</div>
            <div class="main-header">我们提供的服务</div>
        </div>
        <div class="services-grid">
            <div class="service-item" v-for="(service, index) in services" :key="index">
                <img :src="service.image" class="service-image" alt="Service Image">
                <div class="service-description">
                    <h3 class="service-title">{{ service.title }}</h3>
                    <p class="service-text">{{ service.text }}</p>
                </div>
                <button class="service-button" @click="openDetailModal(service)">
                    <img :src="service.buttonImage" @mouseover="service.buttonImage = service.hoverButtonImage"
                        @mouseleave="service.buttonImage = service.defaultButtonImage" alt="More Info">
                </button>
            </div>
        </div>

        <!-- 手术项目详细介绍界面 -->
        <div v-if="showDetailModal" class="modal-overlay" @click="closeDetailModal">
            <div class="modal-content" @click.stop>
                <button class="close-button" @click="closeDetailModal">✖</button>
                <h2>{{ selectedService.title }}</h2>
                <p>价格：{{ selectedService.price}} RMB/次</p>
                <p>{{ selectedService.text }}</p>
                <button class="add-to-cart" @click="addToCart">加入到购物车</button>
            </div>
        </div>

        <!-- 查看购物车界面 -->
        <div v-if="cart.length > 0" class="view-cart">
            <h2>购物车</h2>
            <div v-if="cart.length === 0">购物车为空</div>
            <div v-else>
                <ul>
                    <li v-for="(item, index) in cart" :key="index">
                        <h3>{{ item.service.title }}</h3>
                        <button @click="removeFromCart(index)">Remove</button>
                    </li>
                </ul>
                <button @click="openHCModal">提交购买信息</button>
            </div>
        </div>

        <!-- 选择医院和优惠券 -->
        <div v-if="showHCModal" class="modal-overlay" @click="closeHCModal">
            <div class="modal-content" @click.stop>
                <button class="close-button" @click="closeHCModal">✖</button>
                <h2>选择详细信息</h2>
                <div class="form-group">
                    <label for="hospital">选择医院：</label>
                    <select id="hospital" v-model="selectedHospital">
                        <option v-for="hospital in hospitals" :key="hospital.id" :value="hospital.id">{{ hospital.name
                            }}</option>
                    </select>
                </div>
                <div class="form-group">
                    <label for="coupon">选择优惠券：</label>
                    <select id="coupon" v-model="selectedCoupon">
                        <option v-for="coupon in coupons" :key="coupon.id" :value="coupon.id">{{ coupon.name }}</option>
                    </select>
                </div>
                <button class="add-to-cart" @click="submitCart">提交购买信息</button>
            </div>
        </div>

    </div>
</template>


<script>
//import { getHospitalNameReq, getAllCoupons, setOrder } from "../HTTP/http"
import { get_id } from "../identification"
export default {
    data() {
        return {
            services: [
                {
                    image: "/images/service_1.png",
                    title: "激光祛斑",
                    text: "通过先进的激光技术，有效祛除各种色斑，使您的皮肤恢复光滑细腻。",
                    price:"30000",
                    defaultButtonImage: "/images/service-button-default.png",
                    hoverButtonImage: "/images/service-button-choose.png",
                    buttonImage: "/images/service-button-default.png"
                },
                {
                    image: "/images/service_2.png",
                    title: "割双眼皮",
                    text: "专业的双眼皮手术，提升眼部美感，打造自然迷人的双眼皮效果。",
                    price:"20000",
                    defaultButtonImage: "/images/service-button-default.png",
                    hoverButtonImage: "/images/service-button-choose.png",
                    buttonImage: "/images/service-button-default.png"
                },
                {
                    image: "/images/service_3.png",
                    title: "隆鼻",
                    text: "个性化隆鼻方案，重塑鼻部线条，让您的五官更加立体美观。",
                    price:"40000",
                    defaultButtonImage: "/images/service-button-default.png",
                    hoverButtonImage: "/images/service-button-choose.png",
                    buttonImage: "/images/service-button-default.png"
                },
                {
                    image: "/images/service_4.png",
                    title: "美白护理",
                    text: "提供全方位的美白护理服务，改善肤色暗沉，焕发自然光彩。",
                    price:"10000",
                    defaultButtonImage: "/images/service-button-default.png",
                    hoverButtonImage: "/images/service-button-choose.png",
                    buttonImage: "/images/service-button-default.png"
                },
                {
                    image: "/images/service_5.png",
                    title: "疤痕修复",
                    text: "针对各种类型的疤痕，采用先进的修复技术，显著改善疤痕外观。",
                    price:"8000",
                    defaultButtonImage: "/images/service-button-default.png",
                    hoverButtonImage: "/images/service-button-choose.png",
                    buttonImage: "/images/service-button-default.png"
                },
                {
                    image: "/images/service_6.png",
                    title: "抗衰老保养",
                    text: "全面的抗衰老护理，延缓皮肤老化，保持青春活力。",
                    price:"60000",
                    defaultButtonImage: "/images/service-button-default.png",
                    hoverButtonImage: "/images/service-button-choose.png",
                    buttonImage: "/images/service-button-default.png"
                }
            ],
            defaultButtonImage: "/images/button.png",
            hoverButtonImage: "/images/button-hover.png",
            buttonImage: "/images/button.png",

            selectedService: null,
            showDetailModal: false,
            showHCModal: false,
            selectedHospital: null,   // hospital的id
            selectedCoupon: null,     // coupon的id
            hospitals: [],
            coupons: [],
            cart: [] // 购物车数组
        };
    },
    methods: {
        openDetailModal(service) {
            this.selectedService = service;
            this.showDetailModal = true;
        },

        closeDetailModal() {
            this.showDetailModal = false;
        },

        addToCart() {
            if(!!get_id())
            {
                const isAlreadyInCart = this.cart.some(item => item.service.title === this.selectedService.title);
                if (!isAlreadyInCart) {
                    const cartItem = {
                        service: this.selectedService,
                    };
                    this.cart.push(cartItem);
                    this.$message.success('已成功加入购物车！');
                }
                else {
                    this.$message.error('该服务已在购物车中！');
                }
                this.closeDetailModal();
            }
            else{
                this.$router.push('/login');
            }
        },

        removeFromCart(index) {
            this.cart.splice(index, 1);
            this.$message.success('已成功删除购物车项目！');
        },

        openHCModal() {
            this.showHCModal = true;
            this.hospitals = [];
            this.coupons = [];
            //此处向后端发送请求，获取医院数据和优惠券数据
            getHospitalNameReq(this.setHospitalData);
            getAllCoupons(get_id(), this.setCouponsData);
        },

        closeHCModal() {
            this.showHCModal = false;
        },

        submitCart() {
            // 将购物车内数据及所选的医院、优惠券提交给后端
            // ...
            let items = [];
            for(const item of this.cart){
                items.push(item.service.title);
            }
            let hospital = "";
            for(const item of this.hospitals){
                if(this.selectedHospital == item.id){
                    hospital = item.name;
                }
            }
            if(this.selectedCoupon == null || this.selectedCoupon == ""){
                this.selectedCoupon = "";
            }
            setOrder(get_id(),items,hospital,this.selectedCoupon,this.isOrdered);
        },

        // 填入医院名称的数据
        setHospitalData(response){
            for(const name of response){
                let len = this.hospitals.length;
                if(len == 0){ // 原本没有数据
                    let obj = {
                        id: 1,
                        name: name
                    };
                    this.hospitals.push(obj);
                }
                else{
                    let newid = this.hospitals[len-1].id + 1;
                    let obj = {
                        id: newid,
                        name: name
                    };
                    this.hospitals.push(obj);
                }
            }
        },

        // 将可以使用的优惠券放入 this.coupons 中
        setCouponsData(response){
            this.coupons = [];
            for(const coupon of response){
                if(this.isValid(coupon.name)){
                    this.coupons.push(coupon);
                }
            }
        },

        // 判断优惠券在本次下单中是否可用
        isValid(coupon){
            let temp = false;
            for(const item of this.cart){
                if(coupon[0]==item.service.title[0] && coupon[1]==item.service.title[1]){
                    temp = true;
                }
            }
            return temp;
        },

        // 判断下单是否成功
        isOrdered(response){
            //根据后端信息判断是否提交成功
            if (response == "1") {
                this.$message.success('成功下单！');
                //此处还需清空购物车、清空医院和优惠券的选择
                this.cart = [];
                this.selectedHospital = null;
                this.selectedCoupon = null;
            }
            else {
                this.$message.success('下单失败！');
            }
            //关闭页面
            this.showHCModal = false;
        },
    },
    watch:{
        selectedHospital(n){
            console.log(n);
        },
        selectedCoupon(n){
            console.log(n);
        }
    }
};
</script>

<style scoped>
.services {
    background-color: white;
    border-radius: 5px;
    overflow: hidden;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    padding: 40px;
}

.header {
    text-align: center;
    margin-bottom: 50px;
}

.sub-header {
    font-size: 20px;
    color: #5e88d1;
    margin-bottom: 20px;
}

.main-header {
    font-size: 47px;
    font-weight: bold;
}

.services-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 40px;
}

.service-item {
    background-color: #f2f4f9;
    border-radius: 10px;
    padding: 20px;
    text-align: center;
    position: relative;
    transition: transform 0.3s ease-out, box-shadow 0.3s ease-out;
}

.service-item:hover {
    transform: scale(1.05);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    background-color: #5e88d1;
}

.service-item:hover .service-title,
.service-item:hover .service-text {
    /*这是对鼠标悬停时的文字进行颜色修改 */
    color: rgb(255, 255, 255);
}

.service-image {
    width: 100%;
    height: auto;
    border-radius: 10px;
    margin-bottom: 20px;
}

.service-description {
    transition: color 0.3s ease-out;
}

.service-title {
    font-size: 24px;
    font-weight: bold;
    margin-bottom: 10px;
}

.service-text {
    font-size: 16px;
    color: #666;
}

.service-button {
    position: absolute;
    bottom: 120px;
    right: 50px;
    background: none;
    border: none;
    cursor: pointer;
    outline: none;
}

.service-button img {
    width: 70px;
    height: 70px;
    transition: transform 1s ease;
}

.service-button img:hover {
    transform: rotate(360deg);
}

/* Modal Styles */
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.7);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 1000;
}

.modal-content {
    background-color: white;
    border-radius: 10px;
    padding: 20px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    width: 80%;
    max-width: 600px;
    position: relative;
}

.close-button {
    position: absolute;
    top: 10px;
    right: 10px;
    background: none;
    border: none;
    font-size: 24px;
    cursor: pointer;
}

.form-group {
    margin-bottom: 20px;
}

label {
    display: block;
    margin-bottom: 5px;
    font-weight: bold;
}

select {
    width: 100%;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
}

.add-to-cart {
    background-color: #5e88d1;
    color: white;
    border: none;
    border-radius: 5px;
    padding: 10px 20px;
    font-size: 16px;
    cursor: pointer;
}

.add-to-cart:hover {
    background-color: #4a72b3;
}

/* 购物车界面样式 */
.view-cart {
    background-color: white;
    border-radius: 10px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    padding: 20px;
    margin-top: 30px;
    max-width: 600px;
    margin-left: auto;
    margin-right: auto;
}

.view-cart h2 {
    text-align: center;
    font-size: 30px;
    font-weight: bold;
    color: #333;
    margin-bottom: 20px;
}

.view-cart ul {
    list-style-type: none;
    padding: 0;
}

.view-cart li {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 0;
    border-bottom: 1px solid #ddd;
}

.view-cart li:last-child {
    border-bottom: none;
}

.view-cart h3 {
    font-size: 20px;
    color: #555;
    margin: 0;
}

.view-cart button {
    background-color: #98b0ef;
    color: white;
    border: none;
    border-radius: 5px;
    padding: 8px 12px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.view-cart button:hover {
    background-color: #325db8;
}

.view-cart .submit-button {
    background-color: #5e88d1;
    color: white;
    border: none;
    border-radius: 5px;
    padding: 10px 20px;
    font-size: 16px;
    cursor: pointer;
    display: block;
    width: 100%;
    margin-top: 20px;
    transition: background-color 0.3s ease;
}

.view-cart .submit-button:hover {
    background-color: #4a72b3;
}
</style>