<template>
    <a href="#" class="back-to-top" v-show="visible" @click="scrollToTop">
      <!-- <el-icon color="#fff" class="top"><Top /></el-icon> -->
      <i class="fa fa-arrow-up"></i>
    </a>
  </template>
  
  <script>
  export default {
    data() {
      return {
        visible: false,
      };
    },
    methods: {
      handleScroll() {
        this.visible = window.scrollY > 300;
        // console.log(this.visible);
      },
      scrollToTop(event) {
        event.preventDefault();
        window.scrollTo({ top: 0, behavior: 'smooth' });
      },
    },
    mounted() {
      window.addEventListener('scroll', this.handleScroll);
    },
    beforeDestroy() {
      window.removeEventListener('scroll', this.handleScroll);
    },
  };
  </script>
  
  <style scoped>
  .back-to-top {
    position: fixed;
    right: 30px;
    bottom: 30px;
    transition: 0.5s;
    z-index: 99;
    padding: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    font-weight: normal;
    width: 56px;
    height: 56px;
    transition: .5s;
    border-radius: 50% !important;
    background-color: var(--blue--2);
    text-align: center;
    vertical-align: middle;
    cursor: pointer;
    user-select: none;
    color:#fff;
    text-decoration: none;
  }
  .back-to-top:hover {
    background: #16243D;
  }
  .top{
    font-weight:900;
    font-size:large;
    -webkit-font-smoothing: antialiased;
    display: inline-block;
    font-style: normal;
    font-variant: normal;
    text-rendering: auto;
    line-height: 1;
  }
  </style>
  