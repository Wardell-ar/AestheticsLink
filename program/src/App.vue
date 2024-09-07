<template>
  <template v-if="!isAdminRoute&&!isWorker">
    <AppHeader />
    <div class="main">
      <router-view />
      <BackToTop />
    </div>
    <AppFooter />
  </template>
  <template v-else>
    <router-view />
  </template>
</template>
<script>
import { computed } from 'vue';
import { useRoute } from 'vue-router';
import AppHeader from './components/AppHeader.vue';
import AppFooter from './components/AppFooter.vue';
import BackToTop from './components/BackToTop.vue';

export default {
  components: {
    AppHeader,
    AppFooter,
    BackToTop
  },
  setup() {
    const route = useRoute();
    
    const isAdminRoute = computed(() => route.path.startsWith('/admin'));
    const isWorker = computed(() => route.path.startsWith('/Worker'));
    return {
      isAdminRoute,
      isWorker
    };
  }
}
</script>

<style scoped>
.main {
  flex: 1;
  overflow-y: auto;
  margin-top:120px;
}
/* #app {
  overflow-y: auto;
} */
</style>
