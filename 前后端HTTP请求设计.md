- **登录界面**

前端向后端发送数据：

```js
let obj = {
    uid: this.UID,
    psw: this.PSW
};
let str = JSON.stringify(obj);
// 目的IP路径：http://IPV4:端口号/路径
```

后端向前端发送数据：

```c#
"0"    // 表示管理员登录成功
"1"    // 表示客户登录成功
"2"    // 表示员工登录成功
"3"    // 表示没有用户信息，登录失败
```





- **注册界面**

前端向后端发送数据：

```js
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
// 目的IP路径：http://IPV4:端口号/路径
```

后端向前端发送数据：

```c#
"0"   // 表示注册信息录入失败
"1"   // 表示注册信息录入成功
```