import { createApp } from 'vue'
import App from './App.vue'

import ElementPlus from "element-plus"  // 导入 ElementPlus 组件库的所有模块和功能
import "element-plus/dist/index.css"    // 导入 ElementPlus 组件库所需的全局 CSS 样式

// 引入路由器
import router from "./router"

// 创建一个应用
const app = createApp(App);

// 使用路由器
app.use(router);

app.use(ElementPlus);

// 挂载整个应用到id为app的容器中
app.mount('#app');