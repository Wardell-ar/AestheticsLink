<template>
  <div class="advantages" :class="{ 'fade-in': loaded }" v-lazy-load="loadAdvantages" >
    <div class="area">
      <div class="title-box">
        <span class="welcome" v-if="loaded">
          <img src="/img/icon/dot-icon.png" alt="img" class="icon">
          WHY CHOOSE US
          <img src="/img/icon/dot-icon.png" alt="img" class="icon">
        </span>
        <h1 class="title" v-if="loaded">我们的优势</h1>
        <h3 class="subtitle" v-if="loaded">Our Advantages</h3>
      </div>
      <div class="content">
        <div class="column left-column">
          <IconBox v-for="(advantage, index) in leftAdvantages" :key="index" :iconSrc="advantage.iconSrc" 
          :title="advantage.title" :description="advantage.description" :additionalClass="'animate-left'"
          v-lazy-load-element="()=>loadIconBox('left',index)" :class="leftLoaded[index]" />
        </div>
        <div class="column middle-column">
          <img src="/img/advantages/specialist.png" alt="Specialist" class="specialist-image" 
          v-if="loaded" v-lazy-load-element="loadSpecialistImage" :class="{ 'fade-in':specialistLoaded }">
        </div>
        <div class="column right-column">
          <IconBox v-for="(advantage, index) in rightAdvantages" :key="index" :iconSrc="advantage.iconSrc" 
          :title="advantage.title" :description="advantage.description" :additionalClass="'animate-right'" 
          v-lazy-load-element="()=>loadIconBox('right',index)" :class="rightLoaded[index]"/>
        </div>
      </div>
    </div>
  </div>

</template>
  
<script>
import IconBox from '../components/IconBox.vue';
import lazyLoad from '../assets/directives/lazyLoad.js';

export default {
  name: 'Advantages',
  components: { IconBox },
  directives: { lazyLoad },
  data() {
    return {
      loaded: false,
      specialistLoaded:false,
      leftLoaded:Array(3).fill(false),
      rightLoaded:Array(3).fill(false),
      leftAdvantages: [
      {
        iconSrc: '/img/advantages/ico-font1.jpg',
        title: '专业团队',
        description: '我们的美容医院拥有一支由经验丰富、技术精湛的医师和护理团队，确保每位顾客都能享受到最优质的医疗服务。'
      },
      {
        iconSrc: '/img/advantages/ico-font2.jpg',
        title: '先进设备',
        description: '我们配备了世界领先的医疗美容设备，确保每一次治疗都能达到最佳效果。'
      },
      {
        iconSrc: '/img/advantages/ico-font3.jpg',
        title: '优雅环境',
        description: '我们的医院环境优雅舒适，让每一位顾客在治疗过程中都能享受到愉悦的体验。'
      }],
      rightAdvantages: [
      {
        iconSrc: '/img/advantages/ico-font4.jpg',
        title: '个性化服务',
        description: '我们根据每位顾客的需求和情况，提供量身定制的美容方案，确保每位顾客都能获得满意的效果。'
      },
      {
        iconSrc: '/img/advantages/ico-font5.jpg',
        title: '严格消毒',
        description: '我们的医院严格遵循卫生标准，所有设备和工具都经过严格消毒，确保顾客的安全。'
      },
      {
        iconSrc: '/img/advantages/ico-font6.jpg',
        title: '贴心护理',
        description: '我们的护理团队贴心周到，随时为顾客提供帮助和支持，让每一位顾客感受到温暖和关怀。'
      }],
    };
  },
  methods: {
    loadAdvantages() {
      this.loaded = true;
    },
    loadSpecialistImage(){
      this.specialistLoaded=true;
    },
    loadIconBox(column,index){
      if (column==='left'){
        this.leftLoaded[index]=true;
      }
      else if (column==='right'){
        this.rightLoaded[index]=true;
      }
    }
  }
};
</script>
  
<style lang="scss" scoped>
@import '../assets/styles/advantages.scss';
</style>