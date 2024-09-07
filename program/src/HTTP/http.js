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

// 查询员工信息
export async function searchEmployeeInfo(dataToSend, displayTable) {
    let str = JSON.stringify(dataToSend);
    try {
        const response = await fetch('http://121.199.32.139:8081/QueryAllUsers/QueryServers/QueryServers', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 0)
                alert("未找到匹配条件的员工")
            else {
                console.log("回复为：");
                console.log(data);
                displayTable(data);
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//删除员工信息
export async function deleteEmployeeInfo(selected_Ids, Search) {
    const dataToSend = {
        ser_ids: selected_Ids
    };

    let str = JSON.stringify(dataToSend); // 将对象转换为 JSON 字符串

    console.log("删除的员工信息为：");
    console.log(dataToSend);

    try {
        const response = await fetch('http://121.199.32.139:8081/QueryAllUsers/DeleteServers/DeleteServers', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
                alert("删除成功");
            }
            else {
                alert("删除失败");
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

// 查询会员信息
export async function searchCustomerInfo(dataToSend, displayTable) {
    let str = JSON.stringify(dataToSend);
    try {
        const response = await fetch('http://121.199.32.139:8081/QueryAllUsers/QueryCustomers/QueryCustomers', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 0)
                alert("未找到匹配条件的顾客")
            else {
                console.log("回复为：");
                console.log(data);
                displayTable(data);
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//删除会员信息
export async function deleteCustomerInfo(selectedCus_Ids, Search) {
    const dataToSend = {
        cus_ids: selectedCus_Ids
    };

    let str = JSON.stringify(dataToSend); // 将对象转换为 JSON 字符串

    console.log("删除的会员信息为：");
    console.log(dataToSend);

    try {
        const response = await fetch('http://121.199.32.139:8081/QueryAllUsers/DeleteCustomers/DeleteCustomers', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
                alert("删除成功");
            }
            else {
                alert("删除失败");
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//增加会员信息
export async function addServer(dataToSend, Search) {
    let str = JSON.stringify(dataToSend); // 将对象转换为 JSON 字符串

    try {
        const response = await fetch('http://121.199.32.139:8081/QueryAllUsers/CreateServer/CreateServer', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
            }
        } 
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//查询药品信息
export async function searchPills(dataToSend, displayTable) {
    let str = JSON.stringify(dataToSend);
    try {
        const response = await fetch('http://121.199.32.139:8081/Medicine/CheckStorage/CheckStorage', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 0)
                alert("未找到匹配条件的药品")
            else {
                displayTable(data);
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//一键补药
export async function ReplenishPills() {
    try {
        const response = await fetch('http://121.199.32.139:8081/Medicine/AutoReplenish/AutoReplenish', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: null
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 0)
                alert("补药失败")
            else {
                alert("补药成功")
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//查询服务信息
export async function searchService(dataToSend, displayTable) {
    let str = JSON.stringify(dataToSend);
    try {
        const response = await fetch('http://121.199.32.139:8081/api/Surgery/GetProjectInfo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 0)
                alert("未找到匹配条件的服务")
            else {
                console.log("回复为：");
                console.log(data);
                displayTable(data);
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//删除服务信息
export async function deleteService(selected_Ids, Search) {
    const dataToSend = {
        ids: selected_Ids
    };

    let str = JSON.stringify(dataToSend); // 将对象转换为 JSON 字符串

    console.log("删除的服务信息为：");
    console.log(dataToSend);

    try {
        const response = await fetch('http://121.199.32.139:8081/ProjectChange/RemoveProject/RemoveProject', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
                alert("删除成功");
            }
            else {
                alert("删除失败");
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//修改服务信息
export async function changeService(dataToSend, Search) {

    let str = JSON.stringify(dataToSend); // 将对象转换为 JSON 字符串

    console.log("修改的服务信息为：");
    console.log(dataToSend);

    try {
        const response = await fetch('http://121.199.32.139:8081/ProjectChange/ChangeProject/ChangeProject', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
            }
            else {
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//增加服务信息
export async function addService(dataToSend, Search) {

    let str = JSON.stringify(dataToSend); // 将对象转换为 JSON 字符串

    try {
        const response = await fetch('http://121.199.32.139:8081/ProjectChange/AddProject/AddProject', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
              
            }
        } 
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//批量删除部门信息
export async function batchdeleteBranch(datatoSend, Search) {
    let str = JSON.stringify(datatoSend); // 将对象转换为 JSON 字符串

    try {
        const response = await fetch('http://121.199.32.139:8081/HosAndDep/BatchDeleteHosAndDepInfo/BatchDeleteHosAndDepInfo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
            }
            else {
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//删除部门信息
export async function deleteBranch(datatoSend, Search) {
    let str = JSON.stringify(datatoSend); // 将对象转换为 JSON 字符串

    try {
        const response = await fetch('http://121.199.32.139:8081/HosAndDep/DeleteHosAndDepInfo/DeleteHosAndDepInfo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
            }
            else {
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//修改部门信息
export async function updateBranch(datatoSend, Search) {
    let str = JSON.stringify(datatoSend); // 将对象转换为 JSON 字符串

    try {
        const response = await fetch('http://121.199.32.139:8081/HosAndDep/UpdateHosAndDepInfo/UpdateHosAndDepInfo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
            }
            else {
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

//增加部门信息
export async function createBranch(datatoSend, Search) {
    let str = JSON.stringify(datatoSend); // 将对象转换为 JSON 字符串

    try {
        const response = await fetch('http://121.199.32.139:8081/HosAndDep/CreateHosAndDepInfo/CreateHosAndDepInfo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: str
        });
        if (response.ok) {
            const data = await response.json();
            console.log("回复为：");
            console.log(data);
            if (data === 1) {
                Search();
            }
            else {
            }
        } else {
            alert("网络异常");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("网络异常");
    }
}

// 修改员工密码
export function editEmployeePsw(id, psw, callback){
    let obj = {
        employeeId: id,
        newpassword: psw
    };
    const str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/ServerInfo/UpdateServerPSW/UpdateServerPSW', true);
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

// 获取手术金额
export function getSurgeryPrice(callback){
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://121.199.32.139:8081/OrderPlace/GetPrice/GetPrice', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify("1"));
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