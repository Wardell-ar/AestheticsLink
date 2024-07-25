<template>
  <div id="app">
    <div class="sidebar">
      <div class="showlogo">
        <img src="../assets/pics/logo.png" alt="logo">
        <p>容颜医疗</p>
      </div>
      <ul>
        <li>
          <RouterLink to="#">智慧首页</RouterLink>
        </li>
        <li>
          <RouterLink to="#">人事管理</RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/customer_infoPage' }">顾客信息</RouterLink>
        </li>
        <li>
          <RouterLink to="#">医药采购</RouterLink>
        </li>
        <li>
          <RouterLink to="#">收支统计</RouterLink>
        </li>
        <li>
          <RouterLink to="#">服务管理</RouterLink>
        </li>
        <li>
          <RouterLink to="#">分院管理</RouterLink>
        </li>
        <li>
          <RouterLink to="#">手术室管理</RouterLink>
        </li>
      </ul>
    </div>
    <div class="main-content">
      <RouterView></RouterView>
    </div>
  </div>
</template>

<style>
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
  margin: 10px 0;
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
}

.main-content {
  width: 100%;
  margin-left: 4px;
}
</style>
