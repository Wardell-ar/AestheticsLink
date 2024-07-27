<template>
  <div id="app">
    <div class="sidebar">
      <div class="showlogo">
        <img src="../assets/pics/logo.png" alt="logo">
        <p>容颜医疗</p>
      </div>
      <ul>
        <li>
          <RouterLink to="#">
            <div class="sidebutton">
              <img src="@/assets/pics/home.png" alt="智慧首页" width="34" height="30" style="margin: 0 0 0 2px ">
              <p>智慧首页</p>
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="#">
            <div class="sidebutton">
              <img src="@/assets/pics/hr_manage.png" alt="人事管理" width="20" height="20" style="margin: 0 8px 0 ">
              <p>人事管理</p>
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/customer_infoPage' }">
            <div class="sidebutton">
              <img src="@/assets/pics/cus_info.png" alt="顾客信息" width="20" height="20" style="margin: 0 8px 0 ">
              <p>顾客信息</p>
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="#">
            <div class="sidebutton">
              <img src="@/assets/pics/pill_manage.png" alt="医药管理" width="20" height="20" style="margin: 0 8px 0 ">
              <p>医药管理</p>
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="#">
            <div class="sidebutton">
              <img src="@/assets/pics/money_manage.png" alt="收支管理" width="20" height="20" style="margin: 0 8px 0 ">
              <p>收支管理</p>
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="#">
            <div class="sidebutton">
              <img src="@/assets/pics/service_manage.png" alt="服务管理" width="20" height="20" style="margin: 0 8px 0 ">
              <p>服务管理</p>
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="#">
            <div class="sidebutton">
              <img src="@/assets/pics/hospital_manage.png" alt="分院管理" width="25" height="25" style="margin: 0 2px 0 8px ">
              <p>分院管理</p>
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="#">
            <div class="sidebutton">
              <img src="@/assets/pics/or_manage.png" alt="手术室管理" width="25" height="25" style="margin: 0 2px 0 8px ">
              <p>手术室管理</p>
            </div>
          </RouterLink>
        </li>
      </ul>
    </div>
    <div class="main-content">
      <RouterView></RouterView>
    </div>
  </div>
</template>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

html,
body {
  width: 100%;
  height: 100%;
}

#app {
  display: flex;
  width: 100%;
  height: 100%;
}

.sidebar {
  display: flex;
  width: 12%;
  background-color: #f4f4f4;
  flex-direction: column;
  box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
}

.sidebar ul {
  list-style-type: none;
  padding: 0;
}

.sidebar ul li {
  margin: 20px 0;
}

.sidebar ul li a {
  text-decoration: none;
  color: #333;
  display: block;
  padding: 10px;
  border-radius: 4px;
  transition: background-color 0.3s;
  text-align: center;
  line-height: 50px;
}

.sidebar ul li a:hover {
  background-color: #ddd;
}

.showlogo {
  display: flex;
  border-bottom: 2px solid black;
  border-right: 2px solid black;
  height: 10%;
  line-height: 100px;
  font-size: larger;
}

.main-content {
  width: 100%;
  margin-left: 4px;
}

.sidebutton {
  display: flex;
  text-align: center;
  padding-left: 40px;
  align-items: center;
}
</style>
