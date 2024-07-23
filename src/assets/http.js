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
    xhr.open('POST', 'http://192.168.43.146:5283/Register/Register/Register', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                if (xhr.response === "1") {
                    alert("注册成功");
                    alert("请进行登录");
                    // 跳转的回调函数
                    callback();
                }
                else {
                    alert("信息录入失败");
                    alert("请重新注册");
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
    xhr.open('POST', 'http://192.168.43.146:5283/ServerInfo/ServerInfo/ServerInfo', true);
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
    xhr.open('POST', 'http://192.168.43.146:5283/Signin/SigninState/SigninState', true);
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
    xhr.open('POST', 'http://192.168.43.146:5283/Signin/Signin/Signin', true);
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

// 查询会员信息
export function searchCustomerInfo(dataToSend, displayTable) {
    let str = JSON.stringify(dataToSend);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/QueryAllUsers/QueryCustomers/QueryCustomers', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const response = JSON.parse(xhr.response);
                displayTable(response);
            }
            else {
                alert("网络异常");
            }
        }
    };
}


