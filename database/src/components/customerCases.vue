<template>
  <div class="customer-case">
    <div class="header">
      我们的客户案例
      <div class="sub-header">专业美容指导，你也可以拥有</div>
    </div>
    <div class="section-video">
      <div class="section-video-header">精彩视频展示</div>
      <div class="video-content">
        <video class="video" src="/videos/sample-video.mp4" controls></video>
        <div class="video-description">
          这是视频的描述信息，可以随便填写。
        </div>
      </div>
    </div>
    <div class="section-case">
      <div class="section-case-header">经典客户案例</div>
      <div v-for="(caseItem, index) in cases" :key="index">
        <div class="case-content">
          <img :src="caseItem.image" class="case-image" alt="Case Image">
          <div class="case-details">
            <div class="case-project">
              <strong>项目：</strong> {{ caseItem.project }}
            </div>
            <div class="case-name">
              <strong>客户：</strong> {{ caseItem.name }}
            </div>
            <div class="case-description">
              <strong>心得体会：</strong> {{ caseItem.description }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
  
<script>
export default {
  name:'customerCases',
  data() {
    return {
      cases: [
        {
          image: "/images/contact-image.png",
          project: "激光祛斑",
          name: "张女士",
          description: "我一直因为脸上的色斑而感到困扰，尝试了很多方法都没能有效改善。听朋友介绍了这家美容院的激光祛斑技术，决定来试一试。整个过程非常舒适，医生非常专业，详细解答了我所有的疑问。做完激光祛斑后，色斑明显淡化了，皮肤变得光滑细腻，整个人的气色都好了很多。非常感谢美容院的专业团队，让我重新找回了自信。"
        },
        {
          image: "/images/contact-image.png",
          project: "割双眼皮",
          name: "李小姐",
          description: "我的单眼皮一直让我感到不太自信，尤其是拍照的时候，总觉得眼睛不够有神。经过朋友的推荐，我来到了这家美容院做双眼皮手术。医生非常专业，术前详细和我沟通了手术的细节，并根据我的面部特点设计了适合我的双眼皮方案。手术过程非常顺利，恢复期也比我预期的要快很多。现在我的双眼皮非常自然，眼睛看起来更大更有神，我对这次手术的效果非常满意。感谢美容院的专业服务，让我变得更加美丽。"
        },
        {
          image: "/images/contact-image.png",
          project: "抗衰老保养",
          name: "王女士",
          description: "岁月不饶人，随着年龄的增长，我的皮肤开始出现各种衰老的迹象，比如细纹、松弛等。为了让自己看起来更年轻，我决定来这家美容院做抗衰老保养。美容院的护理项目非常全面，采用了最新的技术和高端的产品。护理师非常细心，整个护理过程非常放松舒适。几次护理下来，我的皮肤明显紧致了很多，细纹也淡化了，看起来年轻了不少。这里的服务让我感到非常满意，我会继续在这里做保养，保持青春活力。"
        }
      ]
    };
  },
  mounted() {
    const options = {
      root: null,
      threshold: 0.7
    };
  
    const observer = new IntersectionObserver((entries, observer) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          entry.target.classList.add('visible');
          observer.unobserve(entry.target);
        }
      });
    }, options);
  
    this.$el.querySelectorAll('.case-content').forEach((element) => {
      observer.observe(element);
    });
  }
};
</script>

<style scoped>
.customer-case {
  background-color: white;
  border-radius: 5px;
  overflow: hidden;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
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
}

.sub-header {
  color: #e1dcdc;
  font-size: 20px;
  font-weight: normal;
  margin-top: 10px;
}

.section-video {
  margin: 20px;
  background-color: #e8f0fa;
  padding: 20px;
  border-radius: 10px;
  display: flex;
  flex-wrap: wrap; /* 确保项目在容器宽度不足时换行 */
  gap: 20px; /* 为项目之间添加间距 */
}

.section-video-header {
  font-size: 40px;
  font-weight: bold;
  margin-top: 20px;
  margin-bottom: 20px;
  max-width: 600px; /* 设置最大宽度 */
  margin-left: auto; /* 自动左边距 */
  margin-right: auto; /* 自动右边距 */
  background: linear-gradient(to bottom, #8db8eb, #c6defa);
  padding: 10px;
  border-radius: 5px;
  text-align: center;
}

.video-content {
  display: flex;
  align-items: center;
  margin-bottom: 40px;
  padding: 20px;
  border-radius: 10px;
  
}

.video {
  width: 45%;
  height: auto;
  margin-left: 30px;
  margin-right: 300px;
  transition: transform 0.3s ease-out, box-shadow 0.3s ease-out;
}

.video-description {
  font-size: 20px;
  transition: color 0.3s ease-out, font-size 0.3s ease-out;
  background: linear-gradient(to bottom, #cfe0f5, #ffffff);
  padding: 10px;
  border-radius: 5px;
}

.video-content:hover .video {
  transform: scale(1.05);
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.video-content:hover .video-description {
  color: #5e88d1;
  font-size: 22px;
}

.section-case {
  padding: 20px;
  margin: 20px;
  border-radius: 10px;
  background-color: #e8f0fa;
  display: flex;
  flex-wrap: wrap; /* 确保项目在容器宽度不足时换行 */
  gap: 20px; /* 为项目之间添加间距 */
}

.section-case-header {
  font-size: 40px;
  font-weight: bold;
  margin-top: 20px;
  margin-bottom: 40px;
  max-width: 600px; /* 设置最大宽度 */
  margin-left: auto; /* 自动左边距 */
  margin-right: auto; /* 自动右边距 */
  background: linear-gradient(to bottom, #8db8eb, #c6defa);
  padding: 10px;
  border-radius: 5px;
  text-align: center;
}

.case-content {
  display: flex;
  align-items: center;
  margin-bottom: 40px;
  opacity: 0;
  transform: translateY(20px);
  transition: opacity 1s ease-out, transform 1s ease-out;
}

.case-content.visible {
  opacity: 1;
  transform: translateY(0);
}

.case-image {
  width: 45%;
  height: auto;
  margin-bottom: 20px;
  margin-left: 30px;
  margin-right: 30px;
  transition: transform 0.3s ease-out, box-shadow 0.3s ease-out;
}

.case-details {
  font-size: 25px;
  font-weight:normal;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  display: flex;
  flex-direction: column;
  margin-left: 50px;
  margin-right: 50px;
  transition: background-color 0.3s ease-out, color 0.3s ease-out;
  padding: 10px;
  border-radius: 5px;
  background: linear-gradient(to bottom, #cfe0f5, #ffffff);
}

.case-content:hover .case-image {
  transform: scale(1.05);
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}
</style>