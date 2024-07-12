// main.js
import { createApp } from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import router from './router'
import lazyLoad from './assets/directives/lazyLoad';
import lazyLoadElement from './assets/directives/lazyLoadElement';

const app = createApp(App)
app.directive('lazy-load', lazyLoad);
app.directive('lazy-load-element',lazyLoadElement)
app.use(ElementPlus)
app.use(router)
app.mount('#app')