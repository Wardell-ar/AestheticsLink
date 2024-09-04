// 登录请求
export function loginReq(UID, PSW, callback) {
    // 发送UID、PSW，接收role、token
    // 返回role，存储token
    let obj = {
        uid: UID,
        psw: PSW
    };
    let str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/Login/CheckLogin/CheckLogin', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) { // 服务器已完成发送
            if (xhr.status >= 200 && xhr.status < 300) { // 服务器发送成功
                const response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 注册请求
export function logupReq(NAME, SEX, YEAR, MONTH, DAY, UID, PSW, callback) {
    // 发送参数列表
    // 接收注册标志并返回
    let obj = {
        name: NAME,
        gender: SEX,
        year: YEAR,
        month: MONTH,
        day: DAY,
        uid: UID,
        psw: PSW
    }
    let str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/Register/Register/Register', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                if (xhr.response === "1") {
                    alert("注册成功，请进行登录");
                    // 跳转的回调函数
                    callback();
                }
                else {
                    alert("信息录入失败，请重新注册");
                }
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 员工界面信息获取
export function getEmployeeInfoReq(id, callback) {
    let obj = {
        id: id
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/ServerInfo/ServerInfo/ServerInfo', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 获取当前员工签到状态
export function getAttendReq(id, callback) {
    let obj = {
        id: id
    };
    let str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/Signin/SigninState/SigninState', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                callback(xhr.response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 更新员工签到信息(后端写死更新为1)
export function updateAttendReq(id, callback) {
    let obj = {
        id: id
    };
    let str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/Signin/Signin/Signin', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                callback(xhr.response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 获取所有分院的名称
export function getHospitalNameReq(callback) {
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/Financial/GetHospital/GetHospital', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send("1");
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const response = JSON.parse(xhr.response);  // 解析
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 各个分院某年某月的收支情况查询
export function getFinanceReq(year, month, hospitalname, callback) {
    let obj = {
        hospitalname: hospitalname,
        year: year,
        month: month
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/Financial/GetFinancial/GetFinancial', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 获取某个顾客的所有优惠券信息
export function getAllCoupons(id, callback) {
    let obj = {
        id: id
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/OrderPlace/Cus_Coupons/Cus_Coupons', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 下单信息
export function setOrder(clientid, items, hospital, couponid, callback) {
    let obj = {
        clientid: clientid,
        items: items,
        hospital: hospital,
        couponid: couponid
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/OrderPlace/PlaceOrder/CheckOrder', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                callback(xhr.response);  // 由于直接接收0、1字符串，无需解析
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 手术室预约情况查看
export function getRoomInfo(hospitalname, callback) {
    let obj = {
        hospitalname: hospitalname
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/ORSchedule/QueryO_RSchedule/QueryO_RSchedule', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                let response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 获取客户个人信息
export function getClientInfo(id, callback) {
    let obj = {
        cusId: id
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/CustomerMessage/GetCustomerInfo', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                let response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 修改客户密码
export function editClientPassword(id, newpwd, callback) {
    let obj = {
        cusId: id,
        newpassword: newpwd
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/CustomerMessage/UpdatePassword/updatePassword', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                let response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 客户查询手术信息
export function getOperationInfo(id, callback) {
    let obj = {
        cusId: id,
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/api/Surgery/GetOperationDetails', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                let response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 手术的推迟
export function OperationDelay(customerId, billid, operationName, callback) {
    let obj = {
        customerId: customerId,
        billid: billid,
        operationName: operationName
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/RebookOperate/DalayOperate/DalayOperate', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                let response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 获取客户会员等级以及折扣信息
export function GetVipInfo(cusId, callback) {
    let obj = {
        cusId: cusId
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/CustomerMessage/GetCustomerVipInfo/getVipInfo', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                let response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 获取客户优惠券的具体信息
export function GetCouponsInfo(cusId, callback){
    let obj = {
        cusId: cusId
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/CustomerMessage/GetCustomerCouponInfo/getCouponInfo', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                let response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}

// 客户充值功能
export function sendRecharge(cusId, money, callback){
    let obj = {
        cusId: cusId,
        money: money
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/Recharge/PostRecharge/Recharge', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                let response = JSON.parse(xhr.response);
                callback(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}