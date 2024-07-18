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
      path: "/login",
      name: "login",
      component: () => import("../components/login.vue"),
    },
    {
      path: "/Person",
      name: "Person",
      component: () => import("../components/Person.vue"),
    },
    {
      path: "/Worker",
      name: "Worker",
      component: () => import("../components/workerCenter.vue"),
    },
  ],
});

export default router
