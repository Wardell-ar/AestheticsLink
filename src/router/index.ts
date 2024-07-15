import { createRouter, createWebHistory } from "vue-router"
import adminPage from "../components/adminPage.vue"
import customer_infoTable from "../views/customer_infoTable.vue"
//创建路由器
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            component: adminPage,
            children: [
                {
                    path: "customer_infoTable",
                    component: customer_infoTable,
                }]
        }
    ]
})

export default router