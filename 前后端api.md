#### OperatingRoom手术室

选择医院后点击确定，向后端请求数据。

selectedHospitalName:{id:'',name:''}是选中的医院

数据格式

```
operatingRooms:[
{
  roomId:'',//手术室id
  slots:[需要按照时间顺序排
    {
      status:'',//状态 0 未预约 1 有预约
      info:'',//有预约的：手术名称，未预约:"未预约"
      doctor:'',//员工名字
      customer:'',//客户名字
    }
  ]
}
],
```

#### Person个人中心 点击购物车可以直接看

数据格式（这个我不知道有哪些）

```
customer: {
level: ‘’,
password: "",
name: '',
age: '',
phone: "",
sex: 1, 1表示男
id: '',
birthday: '2024-08-09',//格式
},
```

点保存发给后端，