<template>
    <button @click="test1">注册</button>
    <br>
    <button @click="test2">登录</button>
    <br>
    <button @click="test3">获取员工信息</button>
    <br>
    <button @click="test4">获取签到码</button>
    <br>
    <button @click="test5">更新签到码</button>
    <br>
    <button @click="test6">获取分院名称</button>
    <br>
    <button @click="test7">获取收支查询</button>
    <br>
    <button @click="test8">优惠券查询</button>
    <br>
    <button @click="test9">下单操作</button>
    <br>
    <button @click="test10">手术室信息查看</button>
    <br>
    <button @click="test11">客户信息获取</button>
    <br>
    <button @click="test12">修改客户密码</button>
    <br>
    <button @click="test13">查询客户手术信息</button>
    <br>
    <button @click="test14">查询客户vip信息</button>
    <br>
    <button @click="test15">查询客户vip信息</button>


</template>
<script>
import { loginReq, GetCouponsInfo, getOperationInfo, editClientPassword, GetVipInfo, getClientInfo, getRoomInfo, getAllCoupons, setOrder, logupReq, getEmployeeInfoReq, getFinanceReq, getHospitalNameReq, getAttendReq, updateAttendReq } from "./HTTP/http"
export default {
    mounted(){
        //console.log(new Date().getMonth()+1);  // 月份
        //console.log(new Date().getDate());     // 日份
    },
    methods: {
        test15(){
            GetCouponsInfo("62", this.c);
        },
        test1() {
            logupReq("LYC","男","2004","3","29","789","789",this.c1);
        },
        test2() {
            loginReq("12345678999","2252550",this.c);
        },
        test3() {
            getEmployeeInfoReq("2252550",this.c);
        },
        test4() {
            getAttendReq("2252550",this.c);
        },
        test5() {
            updateAttendReq("2252550",this.c);
        },
        test6(){
            getHospitalNameReq(this.c);
        },
        test7(){
            getFinanceReq("2024","08","许氏美容所",this.c);
        },
        test8(){
            getAllCoupons("1",this.c);
        },
        test9(){
            setOrder("1", ["激光祛斑", "隆鼻", "疤痕修复"], "许氏美容所", "3", this.c2);
        },
        test10(){
            getRoomInfo("许氏美容所", this.c);
        },
        test11(){
            getClientInfo("1", this.c);
        },
        test12(){
            editClientPassword("1", "123456", this.c);
        },
        test13(){
            getOperationInfo("1", this.c);
        },
        test14(){
            GetVipInfo('1', this.c);
        },
        c1() {
            console.log("注册成功");
        },
        c(response){
            console.log(response.discount*10);
            console.log(response);
        },
        c2(response){
            if(response=="1"){
                console.log("下单成功");
            }
            else{
                console.log("下单失败");
            }
        }
    }
}
</script>