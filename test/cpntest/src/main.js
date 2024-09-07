import { createApp } from 'vue'
import ElementPlus from "element-plus"  // 导入 ElementPlus 组件库的所有模块和功能
import "element-plus/dist/index.css"    // 导入 ElementPlus 组件库所需的全局 CSS 样式
import App from './App.vue'
const app = createApp(App)
app.use(ElementPlus)
app.mount('#app')
