- **登录界面**

前端向后端发送数据：

```js
let obj = {
    uid: this.UID,
    psw: this.PSW
}
```

后端向前端发送数据：

```c#
id      // 对应人物的id
role
"0"	    // 表示管理员登录成功
"1"	    // 表示客户登录成功
"2"	    // 表示员工登录成功
"3"	 	// 表示没有用户信息，登录失败
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
```

后端向前端发送数据：

```c#
"0"    // 表示注册信息录入失败
"1"    // 表示注册信息录入成功
```



- **员工界面信息传递**

前端向后端发送数据：

```js
let obj = {
    id:
}
```

后端向前端发送数据：

```c#
// 发送员工对应的信息，以对象形式发送，没有注明数据类型的都是字符串类型
{
    id:
    name:
    gender:
    age:         // 数字
    title:
    hospital:
    attendanceStatus:
    orderData:[
        {
            order_id:
            order_name:
            order_year:
            order_month:
            order_day:
        },
        ....
    ]
}
```



- **签到状态的获取**

前端向后端发送数据：

```js
let obj = {
    id:
}
```

后端向前端发送数据：

```c#
"1"    // 表示已经签好到了
"0"    // 表示没签到
```



- **签到状态的更新**

前端向后端发送数据：

```js
let obj = {
    id:
}
```

后端向前端发送数据：

```c#
// 先写死将签到状态更新为整数1，表示签到成功
// 下面是要传给前端的数据（注意这里传的是字符串类型）
"1"      // 表示更新成功
"0"      // 表示更新失败
```





