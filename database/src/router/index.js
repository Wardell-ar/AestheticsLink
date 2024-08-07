import { createRouter, createWebHistory } from 'vue-router'
// import Advertise from '../components/Advertise.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "frontPage",
      component: () => import("../components/frontPage.vue"),
    },
    {
      path: "/contractUs",
      name: "contractUs",
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import("../components/contractUs.vue"),
    },
    {
      path: "/customerCases",
      name: "customerCases",
      component: () => import("../components/customerCases.vue"),
    },
    {
      path: "/ourService",
      name: "ourService",
      component: () => import("../components/ourService.vue"),
    },
    {
      path: "/pay",
      name: "pay",
      component: () => import("../components/pay.vue"),
    },
    {
      path: "/login",
      name: "login",
      component: () => import("../components/login.vue"),
    },
    {
      path: "/logup",
      name: "logup",
      component: () => import("../components/logup.vue"),
    },
    {
      path: "/Person",
      name: "Person",
      component: () => import("../components/Person.vue"),
      children:[
        {
          path: "myBenefits",
          name: "myBenefits",
          component: () => import("../components/myBenefits.vue"),
        },
        {
          path: "orders",
          name: "orders",
          component: () => import("../components/Orders.vue"),
        },
      ]
    },
    {
      path: "/Worker",
      name: "Worker",
      component: () => import("../components/workerCenter.vue"),
    },
    // {
    //   path: "/orders",
    //   name: "orders",
    //   component: () => import("../components/Orders.vue"),
    // },    
    // {
    //   path: "/myBenefits",
    //   name: "myBenefits",
    //   component: () => import("../components/myBenefits.vue"),
    //   info:"myBenefits",
    // },
    
  ],
});
router.afterEach(() => {
  // 确保每次路由切换后滚动到顶部
  window.scrollTo(0, 0);
});
export default router
