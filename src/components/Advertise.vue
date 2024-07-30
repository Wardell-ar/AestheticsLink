<template>
  <div class="advertise" v-lazy-load="loadAdvertise" :class="{ 'fade-in': loaded }">
    <div class="area">
      <div class="wrap">
        <div class="icon1" v-if="loaded">
          <img src="/img/advertise/icon1.png" alt="img">
        </div>
        <div class="icon2" v-if="loaded">
          <img src="/img/advertise/icon2.png" alt="img">
        </div>
        <div class="icon3" v-if="loaded">
          <img src="/img/advertise/icon3.png" alt="img">
        </div>
        <div class="title-box">
          <span class="welcome" v-if="loaded">
            <img src="/img/icon/dot-icon.png" alt="img" height="15px" weight="15px">
            全同济最棒的美容院！
          </span>
          <h1 class="title" v-if="loaded">我们只提供最专业的医学
            <br>美容服务</h1>
          <p class="subtitle" v-if="loaded">We only provide the most professional medical beauty service</p>
        </div>
        <div class="button-box">
          <CommonButton v-if="loaded" type="primary" size="large" class="btn-1 common-btn" @click="handleExperience"
            :color="'#0056b1'" :hoverColor="'#004494'">
            立即体验
          </CommonButton>
          <CommonButton v-if="loaded" type="primary" size="large" class="btn-2 common-btn" @click="handleContact"
            :color="'#0f0f0f'" :hoverColor="'#333333'">
            联系我们
          </CommonButton>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CommonButton from './CommonButton.vue';
import lazyLoad from '../assets/directives/lazyLoad.js';

export default {
  name: 'Advertise',
  components: { CommonButton },
  directives: { lazyLoad },
  data() {
    return {
      loaded: false
    };
  },
  methods: {
    handleExperience() {
      // 处理立即体验按钮点击事件
    },
    handleContact() {
      // 处理联系我们按钮点击事件
    },
    loadAdvertise() {
      this.loaded = true;
    }
  }
};
</script>
<style lang="scss" scoped>
@import '../assets/styles/advertise.scss';
</style>