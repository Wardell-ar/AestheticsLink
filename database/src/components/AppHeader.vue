<template>
    <header class="header">
      <div class="title">
        <div class="companyName">
        容颜医疗
        </div>
        <div class="logo">
        <img src="../../public/logo.png" />
        </div>
      </div>
      <div class="menu">
        <nav class="nav">
          <ul class="nav-list">
            <li class="nav-item has-dropdown">
              <router-link to="/">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span class="gradient-text">首页</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item has-dropdown">
              <router-link to="/ourService">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span class="gradient-text">服务</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item has-dropdown">
              <router-link to="/customerCases">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span class="gradient-text">客户案例</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item has-dropdown">
              <router-link to="/contractUs">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span class="gradient-text">联系我们</span>
                </div>
              </router-link>
            </li>
          </ul>
        </nav>
      </div>
      <div class="personal">
        <nav class="nav">
          <ul class="nav-list">
            <li class="nav-item">
              <router-link to="/Person">
                <div style="display: flex; align-items: center; cursor: pointer" class="signup">
                  <el-icon class="icon"><ShoppingCart /></el-icon>
                  <span>购物车</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/login">
                <div style="display: flex; align-items: center; cursor: pointer" class="signup">
                  <span>个人中心</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/login">
                <div style="display: flex; align-items: center; cursor: pointer" class="signup" >
                  <span >登录</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/logup">
                <div style="display: flex; align-items: center; cursor: pointer" class="signup">
                  <span>注册</span>
                </div>
              </router-link>
            </li>
          </ul>
        </nav>
      </div>
    </header>
    <header class="header-bottom" :class="{ 'visible': isVisible }">
      <div class="title">
        <div class="companyName">
        容颜医疗
        </div>
        <div class="logo">
        <img src="../../public/logo.png" />
        </div>
      </div>
      <div class="menu">
        <nav class="nav">
          <ul class="nav-list">
            <li class="nav-item has-dropdown">
              <router-link to="/">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span class="gradient-text">首页</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item has-dropdown">
              <router-link to="/ourService">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span class="gradient-text">服务</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item has-dropdown">
              <router-link to="/customerCases">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span class="gradient-text">客户案例</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item has-dropdown">
              <router-link to="/contractUs">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span class="gradient-text">联系我们</span>
                </div>
              </router-link>
            </li>
          </ul>
        </nav>
      </div>
      <div class="personal">
        <nav class="nav">
          <ul class="nav-list">
            <li class="nav-item">
              <router-link to="/Person">
                <div style="display: flex; align-items: center; cursor: pointer" class="signup">
                  <el-icon class="icon"><ShoppingCart /></el-icon>
                  <span>购物车</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/login">
                <div style="display: flex; align-items: center; cursor: pointer" class="signup">
                  <span>个人中心</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/login">
                <div style="display: flex; align-items: center; cursor: pointer" class="signup" >
                  <span >登录</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/logup">
                <div style="display: flex; align-items: center; cursor: pointer" class="signup">
                  <span>注册</span>
                </div>
              </router-link>
            </li>
          </ul>
        </nav>
      </div>
    </header>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, onMounted, onBeforeUnmount } from "vue";
  
  export default defineComponent({
    setup() {
      const isVisible = ref(false);
  
      const handleScroll = () => {
        isVisible.value = window.scrollY > 100;
      };
  
      onMounted(() => {
        window.addEventListener('scroll', handleScroll);
      });
  
      onBeforeUnmount(() => {
        window.removeEventListener('scroll', handleScroll);
      });
  
      return {
        isVisible
      };
    }
  });
  </script>
  
  <style scoped>
.submenu{
  position: absolute;
  top: 100%;
  left: -5px;
  width: 240px;
  z-index: 999;
  padding: 30px 40px;
  opacity: 0;
  visibility: hidden;
  transition: 0.4s;
  transform: scaleY(0);
  transform-origin: top center;
  text-align: start;
  background-color: black;
  border-top: 5px solid blue;
  box-shadow: rgba(149, 157, 165, 0.2) 0px 8px 24px;
  }
.submenu > li {
  margin: 0;
  margin-bottom: 10px;
  display: block;
}
.submenu > li:last-child {
  margin-bottom: 0;
}
.submenu > li > a {
  padding: 0;
  text-transform: capitalize;
  color: #fff;
  position: relative;
  text-decoration: none
}
.submenu > li > a::after {
  content: "";
  position: absolute;
  bottom: 0;
  right: 0;
  left: auto;
  height: 1px;
  width: 0;
  background-color: blue;
  transition: 0.7s;
  display: inline-block;
}
.submenu > li:hover > a {
  color: blue;
}
.submenu > li:hover > a::after {
  width: 100%;
  right: auto;
  left: 0;
}
.nav > ul > li.has-dropdown {
  display: inline-flex; /* 确保文本和伪元素在同一行 */
  align-items: center; /* 垂直对齐 */
}
.nav > ul > li.has-dropdown::after {
  content: "+";
  /* font-family: "Font Awesome 6 Pro"; */
  display: inline-block;
  font-size: 22px;
  font-weight: 700;
  transition: 0.3s;
  /* margin-left: -4px; */
  /* transform: translateY(4px); */
  transform-origin: center;
  color: blue;
}
.nav > ul > li:hover.has-dropdown::after {
  transform: rotate(-180deg);
  color:blue !important;
}
.nav > ul > li:hover > span {
  color: blue !important;
}
.nav > ul > li:hover .submenu {
  opacity: 1;
  visibility: visible;
  -webkit-transform: scaleY(1);
  -moz-transform: scaleY(1);
  -ms-transform: scaleY(1);
  -o-transform: scaleY(1);
  transform: scaleY(1);
}
.header {
  height: 120px;
  display: flex;
  align-items: center;
  justify-content: space-around;
  position: absolute;
  width: 100%;
  padding: 0;
  margin: 0;
  top: 0;
  left:0;
  background-color: #fff; /* 可根据需要更改背景颜色 */
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* 添加阴影以区分顶部栏 */
  z-index: 1000; /* 确保在最上层 */
  font-size: 22px;
  }
  .header-bottom{
    height: 120px;
    display: flex;
    align-items: center;
    justify-content: space-around;
    position: fixed;
    width: 100%;
    padding: 0;
    margin: 0;
    top: 0;
    left:0;
    background-color: #fff; /* 可根据需要更改背景颜色 */
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* 添加阴影以区分顶部栏 */
    z-index: 900; /* 确保在最上层 */
    font-size: 22px;
    opacity: 0; /* 初始隐藏 */
    transition: opacity 0.5s ease; /* 过渡效果 */
  }
  .header-bottom.visible {
    opacity: 1; /* 显示 */
  }
  .title{
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    margin:auto 15px;
  }
  .logo img {
    height: 80px;
  }
  .menu {
    flex-grow: 2;
  }
  .nav{
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  .nav-list {
    width:100%;
    display: flex;
    padding: 0;
    margin: 0;
    justify-content: space-evenly;
    list-style: none;
  }
  /* .nav-item {
    position: relative;
  } */
  .nav-item:last-child {
    margin-right: 0;
  }
  
  .nav-item div {
    display: flex;
    align-items: center;
    text-decoration: none;
  }
  
  .gradient-text{
  cursor: pointer;
  transition: color 0.3s ease;
  position: relative;
  display: inline-block;
  background: linear-gradient(to right, #074ddb 50%, var(--black--light) 50%);
  background-size: 200% 100%;
  background-position: 100%;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  transition: background-position 0.5s ease;
}
.gradient-text:hover {
  background-position: 0;
}
  
  /* personal部分在最右侧 */
  .personal {
    display: flex;
    align-items: center;
    margin-left:auto;
    flex-grow: 1;
    display: flex;
    justify-content: center;
    align-items: center;
  }

a {
  text-decoration: none!important;
}
 
.menu{
  height:60px;
  width: 500px;
  display: flex;
  justify-content: center;
  align-items: center;
  background: #F2F5F9;
  border-radius: 10px;
  flex-direction: row;
  margin-right: auto !important;
  margin-left: auto !important;
  padding-left: 10;
  margin-bottom: 0;
  list-style: none;
  box-sizing: border-box;
  unicode-bidi: isolate;
  margin: 0;
  font-family: "Inter", sans-serif;
  /* font-size: 1rem;
  font-weight: 400;
  line-height: 1.5; */
  color: #787878;
  /* background-color: #fff; */
  -webkit-text-size-adjust: 100%;
  -webkit-tap-highlight-color: rgba(0, 0, 0, 0);
}

.signup{
  background: rgb(107, 170, 232) !important;
  border: 2px solid rgb(107, 170, 232);
  font-weight: 400;
  transition: 0.5s;
  flex-shrink: 0 !important;
  border-radius: 50rem !important;
  padding-top: .5rem !important;
  padding-bottom: .5rem !important;
  padding-right: 1.5rem !important;
  padding-left: 1.5rem !important;
  margin-left: .5rem !important;
}
.icon,.signup>span{
 color:#fff;
}
.signup:hover>span,.signup:hover>.icon{
  color:var(--blue--light) !important;
}
.signup:hover{
  background: white!important;
  border-color: var(--blue--light);
  border:2px solid;
  color:var(--blue--light) !important;
}
.companyName{
  width: 100%;
  height: 30px;
  margin-top: 5px;
  font-size: 22px;
  border-bottom: 1px solid #f0f0f0;
  background-image: -webkit-linear-gradient(
    left,
    #88bce6 60%,
    #62aaf2 40%,
    #79bbff 80%,
    rgb(19, 95, 201),
    rgba(8, 187, 218, 0.705) 80%
  );
  -webkit-text-fill-color: transparent;
  -webkit-background-clip: text;
  -webkit-background-size: 200% 100%;
  -webkit-animation: masked-animation 4s linear infinite;
}
  </style>
  