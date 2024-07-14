/* 创建一个路由器，并暴露出去 */
// 引入路由器配置相关函数
import {createRouter,createWebHistory} from "vue-router"

// 引入需要配置路由的组件
import Login from "../components/login.vue"
import Logup from "../components/logup.vue"

// 创建路由器
const router = createRouter({

    history:createWebHistory(),   // 设定路由器的工作模式

    routes:[  // 一个一个的路由规则

        // 一个路由规则以一个对象数据的形式表示，path表示路由的路径，component表示目标组件
        {
            path: "/",   // 此处路径可以随意设计
            component: Login
        },

        {
            path: "/logup",
            component: Logup
        }
    ]
});

// 将路由器暴露在外
export default router;