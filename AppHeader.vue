<template>
    <header class="header">
      <div class="logo">
        <img src="../../public/favicon.ico" />
      </div>
      <div class="menu">
        <nav class="nav">
          <ul class="nav-list">
            <li class="nav-item">
              <!-- <router-link to="/"> -->
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>首页</span>
                </div>
              <!-- </router-link> -->
            </li>
            <li class="nav-item has-dropdown">
              <!-- <router-link to="/classify"> -->
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>服务</span>
                  <ul class="submenu">
                    <li><a href="#">了解项目</a></li>
                    <li><a href="#">预约</a></li>
                  </ul>
                </div>
              <!-- </router-link> -->
            </li>
            <li class="nav-item">
              <router-link to="/cloud">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>客户案例</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/read">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>联系我们</span>
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
              <router-link to="/">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>个人中心</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <!-- <router-link to="/classify"> -->
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>分享至QQ</span>
                </div>
              <!-- </router-link> -->
            </li>
            <li class="nav-item">
              <router-link to="/cloud">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>分享至微信</span>
                </div>
              </router-link>
            </li>
          </ul>
        </nav>
      </div>
    </header>
    <header class="header-bottom" :class="{ 'visible': isVisible }">
      <div class="logo">
        <img src="../../public/favicon.ico" />
      </div>
      <div class="menu">
        <nav class="nav">
          <ul class="nav-list">
            <li class="nav-item">
              <router-link to="/">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>首页</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item has-dropdown">
              <router-link to="/classify">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>服务</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/cloud">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>客户案例</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/read">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>联系我们</span>
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
              <router-link to="/">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>个人中心</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/classify">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>分享至QQ</span>
                </div>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/cloud">
                <div style="display: flex; align-items: center; cursor: pointer">
                  <span>分享至微信</span>
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
        isVisible.value = window.scrollY > 300;
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
    height: 160px;
    display: flex;
    align-items: center;
    justify-content: space-between;
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
    justify-content: space-between;
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
  
  .logo {
    padding-left: 10px; /* 根据需要调整内边距 */
  }
  
  .logo img {
    height: 40px; /* 可根据需要调整logo的高度 */
  }
  
  .menu {
    flex-grow: 1;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  
  .nav {
    display: flex;
  }
  
  .nav-list {
    display: flex;
    padding: 0;
    margin: 0;
    list-style: none;
  }
  
  .nav-item {
    margin-right: 40px; /* 根据需要调整间距 */
    position: relative;
  }
  .nav-item:last-child {
    margin-right: 0;
  }
  
  .nav-item div {
    display: flex;
    align-items: center;
  }
  
  .nav-item span {
  cursor: pointer;
  transition: color 0.3s ease;
  position: relative;
  display: inline-block;
  background: linear-gradient(to right, #007BFF 50%, black 50%);
  background-size: 200% 100%;
  background-position: 100%;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  transition: background-position 0.5s ease;
}

.nav-item span:hover {
  background-position: 0;
}
  
  /* personal部分在最右侧 */
  .personal {
    display: flex;
    align-items: center;
    margin-right: 20px; /* 根据需要调整内边距 */
  }
  </style>
  