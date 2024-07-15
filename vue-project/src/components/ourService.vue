<template>
    <div class="services">
      <div class="header">
        <div class="sub-header">OUR BEST SERVICE</div>
        <div class="main-header">我们提供的服务</div>
      </div>
      <div class="services-grid">
        <div class="service-item" v-for="(service, index) in services" :key="index">
          <img :src="service.image" class="service-image" alt="Service Image">
          <div class="service-description">
            <h3 class="service-title">{{ service.title }}</h3>
            <p class="service-text">{{ service.text }}</p>
          </div>
          <button class="service-button" @click="handleClick">
          <img :src="service.buttonImage" @mouseover="service.buttonImage = service.hoverButtonImage" @mouseleave="service.buttonImage = service.defaultButtonImage" alt="More Info">
        </button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name:'ourService',
    data() {
      return {
        services: [
          {
            image: "/images/service-1.png",
            title: "激光祛斑",
            text: "通过先进的激光技术，有效祛除各种色斑，使您的皮肤恢复光滑细腻。",
            defaultButtonImage: "/images/service-button-default.png",
            hoverButtonImage: "/images/service-button-choose.png",
            buttonImage: "/images/service-button-default.png"
          },
          {
            image: "/images/service-2.png",
            title: "割双眼皮",
            text: "专业的双眼皮手术，提升眼部美感，打造自然迷人的双眼皮效果。",
            defaultButtonImage: "/images/service-button-default.png",
            hoverButtonImage: "/images/service-button-choose.png",
            buttonImage: "/images/service-button-default.png"
          },
          {
            image: "/images/service-3.png",
            title: "隆鼻",
            text: "个性化隆鼻方案，重塑鼻部线条，让您的五官更加立体美观。",
            defaultButtonImage: "/images/service-button-default.png",
            hoverButtonImage: "/images/service-button-choose.png",
            buttonImage: "/images/service-button-default.png"
          },
          {
            image: "/images/service-1.png",
            title: "美白护理",
            text: "提供全方位的美白护理服务，改善肤色暗沉，焕发自然光彩。",
            defaultButtonImage: "/images/service-button-default.png",
            hoverButtonImage: "/images/service-button-choose.png",
            buttonImage: "/images/service-button-default.png"
          },
          {
            image: "/images/service-2.png",
            title: "疤痕修复",
            text: "针对各种类型的疤痕，采用先进的修复技术，显著改善疤痕外观。",
            defaultButtonImage: "/images/service-button-default.png",
            hoverButtonImage: "/images/service-button-choose.png",
            buttonImage: "/images/service-button-default.png"
          },
          {
            image: "/images/service-3.png",
            title: "抗衰老保养",
            text: "全面的抗衰老护理，延缓皮肤老化，保持青春活力。",
            defaultButtonImage: "/images/service-button-default.png",
            hoverButtonImage: "/images/service-button-choose.png",
            buttonImage: "/images/service-button-default.png"
          }
        ],
        defaultButtonImage: "/images/button.png",
        hoverButtonImage: "/images/button-hover.png",
        buttonImage: "/images/button.png"
      };
    },
    methods: {
      handleClick() {
        // 页面跳转逻辑将来会在这里实现
        console.log('按钮被点击');
      }
    }

  };
  </script>
  
  <style scoped>
  .services {
    background-color: white;
    border-radius: 5px;
    overflow: hidden;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    padding: 40px;
  }
  
  .header {
    text-align: center;
    margin-bottom: 50px;
  }
  
  .sub-header {
    font-size: 20px;
    color: #5e88d1;
    margin-bottom: 20px;
  }
  
  .main-header {
    font-size: 47px;
    font-weight: bold;
  }
  
  .services-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 40px;
  }
  
  .service-item {
    background-color: #f2f4f9;
    border-radius: 10px;
    padding: 20px;
    text-align: center;
    position: relative;
    transition: transform 0.3s ease-out, box-shadow 0.3s ease-out;
  }
  
  .service-item:hover {
    transform: scale(1.05);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    background-color: #5e88d1;
  }

  .service-item:hover .service-title,
   .service-item:hover .service-text {  /*这是对鼠标悬停时的文字进行颜色修改 */
  color: rgb(255, 255, 255);
  }

  .service-image {
    width: 100%;
    height: auto;
    border-radius: 10px;
    margin-bottom: 20px;
  }
  
  .service-description {
    transition: color 0.3s ease-out;
  }
  
  .service-title {
    font-size: 24px;
    font-weight: bold;
    margin-bottom: 10px;
  }
  
  .service-text {
    font-size: 16px;
    color: #666;
  }

  .service-button {
    position: absolute;
    bottom: 120px;
    right: 50px;
    background: none;
    border: none;
    cursor: pointer;
    outline: none;
   }

  .service-button img {
    width: 70px;
    height: 70px;
    transition: transform 1s ease;
  }

  .service-button img:hover {
    transform: rotate(360deg);
  }

  </style>