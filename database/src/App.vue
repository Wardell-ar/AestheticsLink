<template>
  <AppHeader></AppHeader>
  <div class="main-content">
      <router-view />
      <BackToTop></BackToTop>
  </div>  
  <AppFooter></AppFooter> 
  <!-- <Person></Person> -->
  <!-- <OperatingRooms></OperatingRooms> -->
  <!-- <BranchManage></BranchManage> -->
</template>
<script setup>
import { RouterLink, RouterView } from 'vue-router'
import AppHeader from './components/AppHeader.vue'
import AppFooter from './components/AppFooter.vue'
import BackToTop from './components/BackToTop.vue'
import Person from './components/Person.vue'
import OperatingRooms from './views/OperatingRooms.vue'
import BranchManage from './components/BranchManage.vue'
</script>
<style scoped>
.main-content {
  flex: 1;
  overflow-y: auto;
  margin-top:120px;
}
#app {
  display: flex;
  flex-direction: column;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 0px;
  height: 100vh; /* 占满整个视口高度 */
  width: 100%;
}
</style>
