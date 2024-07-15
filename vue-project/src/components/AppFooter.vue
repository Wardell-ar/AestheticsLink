<template>
    <footer class="footer">
      <div class="container">
        <div class="footer-content">
          <div class="footer-section logo-section">
            <img src="../../public/logo.png" alt="Logo" class="logo" />
          </div>
          <div class="footer-section">
            <h4 class="section-title">快速导航</h4>
            <ul>
              <li><a href="#">首页</a></li>
              <li><a href="#">联系我们</a></li>
              <li><a href="#">服务</a></li>
              <li><a href="#">客户案例</a></li>
            </ul>
          </div>
          <div class="footer-section">
            <h4 class="section-title">联系方式</h4>
            <ul>
              <li><a href="#">电话: 123-456-789</a></li>
              <li><a href="#">Email: info@example.com</a></li>
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
    font-size: 14px;
    width: 100%;
    position: relative;
    bottom: 0;
    left: 0;
    z-index: 1000;
    height:220px;
  }
  .container {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
  }
  
  .footer-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 80%;
  }
  
  .footer-section {
    margin: 0 20px;
  }
  
  .logo-section {
    flex-shrink: 0;
  }
  
  .logo {
    height: 40px;
  }
  
  .section-title {
    font-size: 16px;
    margin-bottom: 10px;
  }
  
  ul {
    list-style: none;
    padding: 0;
  }
  
  ul li {
    margin-bottom: 5px;
  }
  
  .footer a {
    color: #fff;
    text-decoration: none;
    position: relative;
    transition: color 0.3s ease, transform 0.3s ease;
    padding-left: 16px;
    margin-left: 0px;
    font-weight: 400;
  }
  .footer a::after{
    position: absolute;
    content: "";
    top: 50%;
    left: 0;
    transform: translateY(-50%);
    height: 2px;
    width: 10px;
  }
  .footer a::before {
    content: "-";
    position: absolute;
    left: 0;
    opacity: 0;
    transform: translateX(-10px); /* 初始状态的 - 符号位置 */
    transition: opacity 0.3s ease, transform 0.3s ease;
  }
  .footer a:not(:last-child) {
    margin-bottom: 10px;
}
  .footer a:hover {
    color: #007bff; /* 根据需要调整悬停时的颜色 */
    transform: translateX(10px);
    margin-left: 5;
  }
  .footer a:hover::before {
    opacity: 1;
    transform: translateX(0); /* 移动 - 符号到初始位置 */
  }
  </style>
  