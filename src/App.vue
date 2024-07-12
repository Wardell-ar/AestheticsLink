<template>
  <!-- <Person ></Person> -->
  <AppHeader></AppHeader>
  <div class="main-content">
      <router-view />
  </div>  
  <!-- <AppFooter></AppFooter> -->
</template>
<script setup>
import { RouterLink, RouterView } from 'vue-router'
import AppHeader from './components/AppHeader.vue'
import AppFooter from './components/AppFooter.vue'
import Person from './components/Person.vue'

</script>
<style scoped>
.main-content {
  flex: 1;
  overflow-y: auto;
  margin-top: 160px;
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
