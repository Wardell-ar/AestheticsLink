import { createApp } from 'vue'
import App from './App.vue'
import ElementPlus from "element-plus"  // 导入 ElementPlus 组件库的所有模块和功能
import "element-plus/dist/index.css"    // 导入 ElementPlus 组件库所需的全局 CSS 样式
import * as ElementPlusIconsVue from "@element-plus/icons-vue"  // 导入 ElementPlus 组件库中的所有图标
const app = createApp(App)

// 注册 ElementPlus 组件
app.use(ElementPlus)

// 注册 ElementPlus 中的所有图标组件
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component);
}

// app 的挂载
app.mount('#app')
