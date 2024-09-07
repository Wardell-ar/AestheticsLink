<template>
  <div id="app" class="container1">
    <div class="sidebar">
      <div class="showlogo">
        <img src="../assets/pics/logo.png" alt="logo">
      </div>
      <ul>
        <li>
          <RouterLink :to="{ path: '/admin/personal_managePage' }" v-if="isAdmin">
            <div class="sidebutton" @click="toggleHRInfo">
              <img src="@/assets/pics/hr_manage.png" alt="人事管理" width="20" height="20" style="margin: 0 8px 0 ">
              <p>人事管理</p>
              <img src="@/assets/pics/arrow-down.png" :class="{ 'arrow-icon-rotate': isHRInfoVisible }" alt="箭头"
                class="arrow-icon">
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/admin/customer_infoPage' }"  v-if="isAdmin">
            <div class="sidebutton" @click="toggleCustomerInfo">
              <img src="@/assets/pics/cus_info.png" alt="顾客信息" width="20" height="20" style="margin: 0 8px 0 ">
              <p>顾客信息</p>
              <img src="@/assets/pics/arrow-down.png" :class="{ 'arrow-icon-rotate': isCustomerInfoVisible }" alt="箭头"
                class="arrow-icon">
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/admin/pill_managePage' }"  v-if="isAdmin">
            <div class="sidebutton" @click="togglePillInfo">
              <img src="@/assets/pics/pill_manage.png" alt="医药管理" width="20" height="20" style="margin: 0 8px 0 ">
              <p>医药管理</p>
              <img src="@/assets/pics/arrow-down.png" :class="{ 'arrow-icon-rotate': isPillInfoVisible }" alt="箭头"
                class="arrow-icon">
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/admin/money_managePage' }"  v-if="isAdmin">
            <div class="sidebutton" @click="toggleMoneyInfo">
              <img src="@/assets/pics/money_manage.png" alt="收支管理" width="20" height="20" style="margin: 0 8px 0 ">
              <p>收支管理</p>
              <img src="@/assets/pics/arrow-down.png" :class="{ 'arrow-icon-rotate': isMoneyInfoVisible }" alt="箭头"
                class="arrow-icon">
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/admin/service_managePage' }" v-if="isAdmin">
            <div class="sidebutton" @click="toggleServiceInfo">
              <img src="@/assets/pics/service_manage.png" alt="服务管理" width="20" height="20" style="margin: 0 8px 0 ">
              <p>服务管理</p>
              <img src="@/assets/pics/arrow-down.png" :class="{ 'arrow-icon-rotate': isServiceInfoVisible }" alt="箭头"
                class="arrow-icon">
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/admin/branch_managePage' }"  v-if="isAdmin">
            <div class="sidebutton" @click="toggleHospitalInfo">
              <img src="@/assets/pics/hospital_manage.png" alt="分院管理" width="25" height="25"
                style="margin: 0 2px 0 8px ">
              <p>分院管理</p>
              <img src="@/assets/pics/arrow-down.png" :class="{ 'arrow-icon-rotate': isHospitalInfoVisible }" alt="箭头"
                class="arrow-icon">
            </div>
          </RouterLink>
        </li>
        <li>
          <RouterLink :to="{ path: '/admin/or_managePage' }"  v-if="isAdmin">
            <div class="sidebutton" @click="toggleORInfo">
              <img src="@/assets/pics/or_manage.png" alt="手术室管理" width="25" height="25" style="margin: 0 2px 0 8px ">
              <p>手术室管理</p>
              <img src="@/assets/pics/arrow-down.png" :class="{ 'arrow-icon-rotate': isORInfoVisible }" alt="箭头"
                class="arrow-icon">
            </div>
          </RouterLink>
        </li>
      </ul>
    </div>
    <div class="main-content1">
      <RouterView></RouterView>
    </div>
  </div>
</template>

<script>
import { get_role } from '@/identification';
export default {
  data() {
    return {
      isHomeInfoVisible: false,
      isHRInfoVisible: false,
      isCustomerInfoVisible: false,
      isPillInfoVisible: false,
      isMoneyInfoVisible: false,
      isServiceInfoVisible: false,
      isHospitalInfoVisible: false,
      isORInfoVisible: false,
      isAdmin: get_role() == "0"
    };
  },
  methods: {
    toggleHomeInfo() {
      this.isHomeInfoVisible = true;
      this.isHRInfoVisible = false;
      this.isCustomerInfoVisible = false;
      this.isPillInfoVisible = false;
      this.isMoneyInfoVisible = false;
      this.isServiceInfoVisible = false;
      this.isHospitalInfoVisible = false;
      this.isORInfoVisible = false;
    },
    toggleHRInfo() {
      this.isHomeInfoVisible = false;
      this.isHRInfoVisible = true;
      this.isCustomerInfoVisible = false;
      this.isPillInfoVisible = false;
      this.isMoneyInfoVisible = false;
      this.isServiceInfoVisible = false;
      this.isHospitalInfoVisible = false;
      this.isORInfoVisible = false;
    },
    toggleCustomerInfo() {
      this.isHomeInfoVisible = false;
      this.isHRInfoVisible = false;
      this.isCustomerInfoVisible = true;
      this.isPillInfoVisible = false;
      this.isMoneyInfoVisible = false;
      this.isServiceInfoVisible = false;
      this.isHospitalInfoVisible = false;
      this.isORInfoVisible = false;
    },
    togglePillInfo() {
      this.isHomeInfoVisible = false;
      this.isHRInfoVisible = false;
      this.isCustomerInfoVisible = false;
      this.isPillInfoVisible = true;
      this.isMoneyInfoVisible = false;
      this.isServiceInfoVisible = false;
      this.isHospitalInfoVisible = false;
      this.isORInfoVisible = false;
    },
    toggleMoneyInfo() {
      this.isHomeInfoVisible = false;
      this.isHRInfoVisible = false;
      this.isCustomerInfoVisible = false;
      this.isPillInfoVisible = false;
      this.isMoneyInfoVisible = true;
      this.isServiceInfoVisible = false;
      this.isHospitalInfoVisible = false;
      this.isORInfoVisible = false;
    },
    toggleServiceInfo() {
      this.isHomeInfoVisible = false;
      this.isHRInfoVisible = false;
      this.isCustomerInfoVisible = false;
      this.isPillInfoVisible = false;
      this.isMoneyInfoVisible = false;
      this.isServiceInfoVisible = true;
      this.isHospitalInfoVisible = false;
      this.isORInfoVisible = false;
    },
    toggleHospitalInfo() {
      this.isHomeInfoVisible = false;
      this.isHRInfoVisible = false;
      this.isCustomerInfoVisible = false;
      this.isPillInfoVisible = false;
      this.isMoneyInfoVisible = false;
      this.isServiceInfoVisible = false;
      this.isHospitalInfoVisible = true;
      this.isORInfoVisible = false;
    },
    toggleORInfo() {
      this.isHomeInfoVisible = false;
      this.isHRInfoVisible = false;
      this.isCustomerInfoVisible = false;
      this.isPillInfoVisible = false;
      this.isMoneyInfoVisible = false;
      this.isServiceInfoVisible = false;
      this.isHospitalInfoVisible = false;
      this.isORInfoVisible = true;
    }
  }
}
</script>

<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

html,
body {
  width: 100%;
  height: 100%;
}

#app {
  display: flex;
  width: 100%;
  height: 100%;
  overflow-y: auto;
}

.main-content1 {
  flex: 1;
  /* 占据剩余的空间 */
  overflow-y: auto;
}

/* 背景图 */
.container1 {
  width: 100%;
  background: linear-gradient(45deg,
      #3498db,
      rgb(46, 204, 204));
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  position: relative;
  overflow: hidden;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  min-height: 100vh;
  overflow-y: auto;
}

.container1::before {
  content: "";
  position: absolute;
  width: 100%;
  background-image: linear-gradient(90deg,
      rgba(255, 255, 255, 0.1) 1px,
      transparent 1px),
    linear-gradient(rgba(255, 255, 255, 0.1) 1px, transparent 1px);
  background-size: 20px 20px;
  pointer-events: none;
  z-index: -1;
  min-height: 100vh;
}

.sidebar {
  display: flex;
  width: 12%;
  background-color: #51abe4;
  flex-direction: column;
  box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
  flex: 0 0 240px;
  /* 固定宽度 */  
  min-height: 100vh;
}

.sidebar ul {
  list-style-type: none;
  padding: 0;
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
}

.sidebar ul li {
  margin: auto 0px;
}

.sidebar ul li a {
  text-decoration: none;
  color: #333;
  display: block;
  padding: 10px;
  border-radius: 4px;
  transition: background-color 0.3s;
  text-align: center;
  line-height: 50px;
}

.sidebar ul li a:hover {
  background-color: #ddd;
}

.showlogo {
  display: flex;
  border-bottom: 2px solid grey;
  height: 110px;
  justify-content: center;
  font-size: larger;
}

.showlogo img {
  width: 50%;
  height: auto;
}

.main-content1 {
  flex: 1;
  /* 占据剩余的空间 */
  overflow-y: auto;
  margin-left: 4px;
}

.sidebutton {
  display: flex;
  text-align: center;
  padding-left: 40px;
  align-items: center;
}

.arrow-icon {
  width: 20px;
  height: 20px;
  margin-left: 8px;
  transition: transform 0.3s;
}

.arrow-icon-rotate {
  transform: rotate(-90deg);
}
</style>
