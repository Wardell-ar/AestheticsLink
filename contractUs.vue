<template>
  <div class="contact-us">
    <div class="header">
      联系我们
      <div class="sub-header">快速联系我们，获取专业美容分析</div>
    </div>
    <div class="content">
      <img class="contact-image" src="/images/contact-image.png" alt="Contact Us">
      <div class="contact-details">
        <p style="font-size: 30px; font-weight: bold;" >上海（总部）</p>
        <p>地址：上海市嘉定区同济大学</p>
        <p>邮编：210000</p>
        <p>电话：13079096197</p>
        <p>邮箱：2253542@tongji.edu.cn</p>
        <p>网址：www.tongji.com</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'contactUs'
}
</script>

<style scoped>

/* 部件初次加载的渐入动画 */
@keyframes fadeIn {
    from {
      opacity: 0;
    }
    to {
      opacity: 1;
    }
  }

.contact-us {
  background-color: white;
  border-radius: 5px;
  overflow: hidden;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  animation: fadeIn 1s ease-out; /* 初次加载时的渐入效果 */
}

.header {
  background: linear-gradient(to bottom, #5895ff, #34475f);
  color: white;
  text-align: center;
  padding: 60px;
  font-size: 40px;
  font-weight: normal;
  position: relative;
  margin-bottom: 50px;
  transition: background-color 0.3s ease-out; /* 悬停时的背景颜色变化 */
}

.header:hover {
  background-color: #4e77b1;
}

.sub-header {
  color: #e1dcdc;
  font-size: 20px;
  font-weight: thick;
  margin-top: 10px;
  animation: fadeIn 2s ease-out; /* 初次加载时的渐入效果 */
}

.content {
  display: flex;
  font-size: 20px;
  padding: 20px;
  margin-top: 15px;
  background: linear-gradient(to bottom, #cfe0f5, #ffffff);
  transition: padding 0.3s ease-out; /* 悬停时增加内边距 */
  animation: fadeIn 2s ease-out; /* 初次加载时的渐入效果 */
} 

.content:hover {
  padding: 40px; /* 悬停时增加内边距 */
}

.contact-image {
  width: 45%;
  height: auto;
  margin-right: 300px; /* 图片与右侧文字的距离 */
  transition: transform 0.3s ease-out; /* 悬停时的缩放效果 */
}

.contact-image:hover {
  transform: scale(1.05); /* 悬停时放大 */
}

.contact-details {
  display: flex;
  flex-direction: column;
  justify-content: center;
  animation: fadeIn 2s ease-out; /* 初次加载时的渐入效果 */
}

.contact-details p {
  margin: 5px 0;
  transition: color 0.3s ease-out; /* 悬停时的文字颜色变化 */
}

.contact-details p:hover {
  color: #5e88d1;
}

.contact-title {
  font-size: 30px;
  font-weight: bold;
  animation: fadeIn 2s ease-out; /* 初次加载时的渐入效果 */
}
</style>