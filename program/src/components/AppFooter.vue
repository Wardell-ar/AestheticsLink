<template>
  <footer class="footer">
    <div class="container">
      <div class="footer-content">
        <div class="logo-section">
          <img src="../../public/logo.png" alt="Logo" class="logo" />
        </div>
        <div class="footer-section nav">
          <h4 class="section-title">快速导航</h4>
          <ul>
            <li>
              <router-link to="/">
              <i class="fas fa-angle-right icon"></i>
              <span>首页</span>
              </router-link>
            </li>
            <li>
              <router-link to="/contractUs">
              <i class="fas fa-angle-right icon"></i>
              <span>联系我们</span>
            </router-link>

            </li>
            <li>
              <router-link to="/ourService">
                <i class="fas fa-angle-right icon"></i>
              <span>服务</span>
              </router-link>
            </li>
            <li>
              <router-link to="/customerCases">
                <i class="fas fa-angle-right icon"></i>
              <span>客户案例</span>
            </router-link>
            </li>
          </ul>
        </div>
        <div class="footer-section info">
          <h4 class="section-title">联系方式</h4>
          <ul>
            <li>              
              <i class="fas fa-map-marker-alt icon"></i>
              <a href="#">地址: 上海市嘉定区同济大学</a>
            </li>
            <li>
              <i class="fas fa-phone-alt icon"></i>
              <a href="#">电话:13079096197</a>
            </li>
            <li>              
              <i class="fas fa-at icon"></i>
              <a href="#">Email: 2253542@tongji.edu.cn</a>
            </li>
            <li>              
              <i class="fas fa-envelope icon"></i>
              <a href="#">邮编: 210000</a>
            </li>
            <li>              
              <i class="fas fa-desktop-alt icon"></i>
              <a href="#">网址: www.tongji.com</a>
            </li>
          </ul>
        </div>
        <div class="footer-section info">
          <h4 class="section-title">营业时间</h4>
          <ul>
            <li>
              <a hrf="#">Monday - Friday:</a>              
              <i class="fas fa-clock icon" :style="{marginLeft:'0.5rem'}"></i>
              <a href="#">08:00 AM - 18:00 PM</a>
            </li>
            <li>
              <a hrf="#">Satur - Sunday:</a>              
              <i class="fas fa-clock icon" :style="{marginLeft:'0.5rem'}"></i>
              <a href="#">08:00 AM - 18:00 PM</a>
            </li>
            <li>
              <a hrf="#">Holiday:</a>              
              <i class="fas fa-clock icon" :style="{marginLeft:'0.5rem'}"></i>
              <a href="#">08:00 AM - 18:00 PM</a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </footer>
  </template>
  
  <script>
  export default {
    name: "Footer",
  };
  </script>
  
  <style scoped>
  .footer {
    background-color: #000;
    padding: 20px 0;
    color: #fff;
    font-size: 16px;
    width: 100%;
    position: relative;
    bottom: 0;
    left: 0;
    z-index: 1;
    height:310px;
  }
  .container {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 80%;
    height: 100%;
  }
  .footer-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100%;
    height: 100%;
  }
  
  .footer-section {
    margin: 0px 20px;
    padding-top: 20px;
    height: 100%;
    display: flex;
    flex-direction: column;
  }
  
  ul {
    display: flex;
    flex-direction: column;
    justify-content: space-around;
  }
  
  .logo {
    height: 80px;
    color:var(--blue--light);
  }
  
  .section-title {
    font-size: 1.5rem;
    margin-bottom: 1.5rem;
  }
  
  ul {
    list-style: none;
    padding: 0;
  }
  
  ul li {
    margin-bottom: 5px;
    display: flex;
    align-items: center;
    cursor: pointer;
    position: relative;
  }
  
  .nav a {
    color: #fff;
    position: relative;
    text-decoration: none;
    transition: color 0.5s ease, transform 0.5s ease;
    /* padding-left: 16px; */
    margin-left: 0px;
    font-weight: 400;
    line-height: 35px;
  }
  .nav a:not(:last-child) {
    margin-bottom: 10px;
}
  .nav a:hover {
    color: var(--blue--light); /* 根据需要调整悬停时的颜色 */
    letter-spacing: 1px;
    transform: translateX(10px);
  }
  .nav a:hover .icon{
    opacity: 1;
    color:var(--blue--light);
    left:0px;
    margin-right: 0.5rem;
  }
  a {
    text-decoration: none;
  }
  .router-link-active {
    text-decoration: none;
  }
  .nav .icon {
    opacity: 0;
    margin-right:0;
    position: relative;
    left:-15px;
    transition: opacity 0.5s ease, left 0.5s ease;
  }
  .info .icon{
    color:var(--blue--light);
    left:0px;
    margin-right: 0.5rem;
  }
  .info a{
    text-decoration: none;
    color:#fff;
    margin-left: 0px;
    font-weight: 400;
    line-height: 35px;
  }
  </style>
  