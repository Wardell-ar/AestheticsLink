import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index'
import ElementPlus from "element-plus";
import "element-plus/dist/index.css";
import * as ElementPlusIconsVue from "@element-plus/icons-vue";
import lazyLoad from './assets/directives/lazyLoad';
import lazyLoadElement from './assets/directives/lazyLoadElement';
import './assets/color.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@fortawesome/fontawesome-free/css/all.css';
const app = createApp(App)
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component);
}
app.use(router)
app.use(ElementPlus);
app.mount('#app')
