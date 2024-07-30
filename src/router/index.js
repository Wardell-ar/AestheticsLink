// index.js
import {createRouter,createWebHistory} from 'vue-router'

const routes =[
    {
        path:"/",
        alias:["/home", "/index"], //定义别名
        component:()=>import ("@/views/frontPage.vue")
    }  
]

const router = createRouter({
    history:createWebHistory(),
    routes
})

export default router