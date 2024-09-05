import { createRouter, createWebHistory } from 'vue-router'
import { get_id } from '@/identification';

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
    {
      path: "/admin",
      component: () => import("../components/adminPage.vue"),
      children: [
          {
              path: "customer_infoPage",
              component: () => import("../views/CustomerInfo.vue"),
          },
          {
              path: "pill_managePage",
              component: () => import("../views/Pills.vue"),
          },
          {
              path: "or_managePage",
              component: () => import("../views/OperatingRooms.vue"),
          },
          {
              path: "service_managePage",
              component: () => import("../views/Service.vue"),
          },
          {
              path: "money_managePage",
              component: () => import("../views/HospitalFinance.vue"),
          },
          {
              path: "branch_managePage",
              component: () => import("../views/BranchManage.vue"),
          },
          {
              path: "personal_managePage",
              component: () => import("../views/personalManagePage.vue"),
          }
      ]
    },
  ],
});
router.afterEach(() => {
  // 确保每次路由切换后滚动到顶部
  window.scrollTo(0, 0);
});
// 全局路由守卫
router.beforeEach((to, from, next) => {
  if ((to.path === '/Person'||to.path === '/Worker'||to.path === '/admin')&& !!!get_id()) {
    // 如果用户未登录，跳转到登录页面
    next('/login');
  } else {
    next();
  }
});

export default router