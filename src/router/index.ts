import { createRouter, createWebHistory } from "vue-router"
import adminPage from "../components/adminPage.vue"
import customer_infoPage from "../views/customer_infoPage.vue"
//创建路由器
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            component: adminPage,
            children: [
                {
                    path: "customer_infoPage",
                    component: customer_infoPage,
                }]
        }
    ]
})

export default router