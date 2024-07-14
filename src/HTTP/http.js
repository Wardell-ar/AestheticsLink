
// 登录请求
export function loginReq(UID, PSW,check) {
    // 发送UID、PSW，接收role、token
    // 返回role，存储token
    let obj = {
        uid: UID,
        psw: PSW
    };
    let str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://100.78.214.7:5283/api/Login/CheckLogin', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) { // 服务器已完成发送
            if (xhr.status >= 200 && xhr.status < 300) { // 服务器发送成功
                const response = JSON.parse(xhr.response);
                if (response.token) { // 如果有token返回
                    localStorage.setItem('token', response.token); // 存储token
                    check(response.role);
                }
                else {
                    alert("登录验证失败");
                    check("3");
                }
            }
            else {
                alert("网络异常");
                check("3");
            }
        }
    };
}

// 注册请求
export function logupReq(NAME, SEX, YEAR, MONTH, DAY, UID, PSW,jumpToLogin) {
    // 发送参数列表
    // 接收注册标志并返回
    let obj = {
        UID:UID,
        GENDER:SEX,
        NAME:NAME,
        PASSWORD:PSW,
        YEAR:YEAR,
        MONTH:MONTH,
        DAY:DAY
    }
    let str = JSON.stringify(obj);
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://100.78.214.7:5283/api/Register/Register', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(str);
    xhr.onreadystatechange = () => {
        if(xhr.readyState === 4){
            if(xhr.status >= 200 && xhr.status < 300){
                if(xhr.response === "1"){
                    alert("注册成功");
                    alert("请进行登录");
                    // 跳转的回调函数
                    jumpToLogin();
                }
                else{
                    alert("信息录入失败");
                    alert("请重新注册");
                }
            }
            else{
                alert("网络错误");
            }
        }
    };
}

// 员工界面信息获取
export function getEmployeeInfoReq(setData){
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://100.78.214.7:5290/api/Login/CheckLogin', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(token);
    xhr.onreadystatechange = () => {
        if(xhr.readyState === 4){
            if(xhr.status >= 200 && xhr.status < 300){
                setData(xhr.response);
            }
            else{
                alert("员工信息加载失败");
            }
        }
    };
}

// 获取当前员工签到状态
export function getAttendReq(check){
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://100.78.214.7:5290/api/Login/CheckLogin', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(token);
    xhr.onreadystatechange = () => {
        if(xhr.readyState === 4){
            if(xhr.status >= 200 && xhr.status < 300){
                const response = JSON.parse(xhr.response);
                check(response);
            }
            else{
                alert("网络异常，请稍后重试");
            }
        }
    };
}

// 更新员工签到信息(后端写死更新为1)
export function updateAttendReq(check){
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://100.78.214.7:5290/api/Login/CheckLogin', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(token);
    xhr.onreadystatechange = () => {
        if(xhr.readyState === 4){
            if(xhr.status >= 200 && xhr.status < 300){
                check(xhr.response);
            }
            else{
                alert("网络异常，请稍后重试");
            }
        }
    };
}









