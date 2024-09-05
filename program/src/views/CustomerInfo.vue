<template>
  <div class="main-container">
    <div class="title">
      顾客信息资料表
    </div>
    <div class="toolbar">

      <div class="form__group field" style="margin: 10px">
        <input type="input" class="form__field" placeholder="Name" required="" v-model="agemin">
        <label class="form__label">年龄下限</label>
      </div>
      <p style="font-size: larger; align-self: center;">~</p>
      <div class="form__group field" style="margin: 10px">
        <input type="input" class="form__field" placeholder="Name" required="" v-model="agemax">
        <label class="form__label">年龄上限</label>
      </div>

      <div class="form__group field" style="margin: 10px">
        <input type="input" class="form__field" placeholder="Name" required="" v-model="name">
        <label class="form__label">姓名</label>
      </div>

      <div class="form__group field" style="margin: 10px 150px 10px 10px">
        <input type="input" class="form__field" placeholder="Name" required="" v-model="phone_num">
        <label class="form__label">手机号</label>
      </div>

      <Dropdown_gender @updateGender="getGender" :clearData="clearData" @reset-clear-data="Reset"
        style="margin-right: 50px;" />
      <Dropdown_level @updateLevel="getLevel" :clearData="clearData" @reset-clear-data="Reset" />
      <div class="clear">
        <button type="submit" @click="Clear">清空</button>
      </div>
      <div class="search">
        <button type="submit" @click="Search">查找</button>
      </div>
    </div>
    <div class="customerTable">
      <el-table :data="tableData" style="width: 100%" :row-class-name="tableRowClassName" empty-text="暂无数据">
        <el-table-column prop="index" label="序号" width="100"></el-table-column>
        <el-table-column prop="selected" label="选择" width="150">
          <template #default="scope">
            <el-checkbox v-model="scope.row.selected" @change="() => handleSelectionChange(scope.row)"></el-checkbox>
          </template>
        </el-table-column>
        <el-table-column prop="name" label="姓名"></el-table-column>
        <el-table-column prop="cus_id" label="顾客ID"></el-table-column>
        <el-table-column prop="gender" label="性别"></el-table-column>
        <el-table-column prop="phone_num" label="联系电话"></el-table-column>
        <el-table-column prop="age" label="年龄"></el-table-column>
        <el-table-column prop="viplevel" label="会员等级"></el-table-column>
        <el-table-column prop="balance" label="账户余额"></el-table-column>
      </el-table>
    </div>
    <div class="bottom">
      <button class="delbtn" @click="Delete">
        <svg viewBox="0 0 448 512" class="svgIcon">
          <path
            d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z">
          </path>
        </svg>
      </button>
    </div>
  </div>
</template>

<script>
import Dropdown_gender from '@/components/Dropdown_gender.vue'
import Dropdown_level from '@/components/Dropdown_level.vue'
import { searchCustomerInfo, deleteCustomerInfo } from '../HTTP/http'
import { onMounted, getCurrentInstance } from 'vue';

export default {
  data() {
    return {
      agemin: "",
      agemax: "",
      name: "",
      phone_num: "",
      gender: "",
      viplevel: "",
      clearData: false,
      // 选中的顾客ID集合
      selectedCus_Ids: [],
      // 表单数据
      tableData: []
    };
  },
  components: {
    Dropdown_gender,
    Dropdown_level
  },
  methods: {
    getGender(selectedOption) {
      this.gender = selectedOption
    },
    getLevel(selectedOption) {
      this.viplevel = selectedOption
    },

    // 清空筛选条件方法实现
    Clear() {
      this.agemin = "",
        this.agemax = "",
        this.name = "",
        this.phone_num = "",
        this.gender = "",
        this.viplevel = "",
        this.clearData = true;
    },
    Reset() {
      this.clearData = false;
    },

    // 查询方法实现
    Search() {
      const dataToSend = {
        ...(this.agemin && { agemin: parseInt(this.agemin) }),
        ...(this.agemax && { agemax: parseInt(this.agemax) }),
        ...(this.name && { name: this.name }),
        ...(this.phone_num && { phone_num: this.phone_num }),
        ...(this.gender && { gender: this.gender }),
        ...(this.viplevel && { viplevel: this.viplevel }),
      }
      console.log(dataToSend)
      this.selectedRowIds = [],
        this.selectedCus_Ids = [],
        searchCustomerInfo(dataToSend, this.displayTable);
    },
    displayTable(response) {
      const processedData = response.map(item => {
        let viplevel = item.viplevel;
        if (viplevel === "Gold") {
          viplevel = "黄金";
        } else if (viplevel === "Silver") {
          viplevel = "白银";
        } else if (viplevel === "Copper") {
          viplevel = "青铜";
        }
        return {
          ...item,
          viplevel
        };
      });
      this.tableData = processedData; // 将处理后的数据存储在 tableData 中
      this.deselectAll();
    },

    // 删除方法实现
    Delete() {
      console.log(this.selectedCus_Ids);
      deleteCustomerInfo(this.selectedCus_Ids, this.Search);
    },

    // 选中栏恢复未选择的状态方法实现
    deselectAll() {
      this.tableData.forEach(row => {
        row.selected = false;
      });
    },

    handleSelectionChange(row) {
      if (row.selected) {
        this.selectedCus_Ids.push(row.cus_id);
      } else {
        const cusindex = this.selectedCus_Ids.indexOf(row.cus_id);
        if (cusindex > -1) {
          this.selectedCus_Ids.splice(cusindex, 1);
        }
      }
    },
    tableRowClassName({ row, rowIndex }) {
      row.index = rowIndex + 1;
      if (row.selected) {
        return "highlight-row";
      }
      if (rowIndex % 2 == 0) {
        return "warning-row";
      }
      else {
        return "";
      }
    },
  },
  setup() {
    const instance = getCurrentInstance();
    onMounted(() => {
      instance.proxy.Search();
    });
  },
  watch: {
    name(newValue) {
      console.log("当前输入的内容:", newValue);
      // 你可以在这里执行其他操作，如发送数据到服务器
    }
  }
}
</script>


<style scoped>
.main-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  /* 确保容器至少和视口一样高 */
}

.title {
  background-color: #51abe4;
  text-align: center;
  line-height: 60px;
  height: 6%;
  margin-bottom: 4px;
  box-shadow: 0 2px 2px rgba(0, 0, 0, 0.2);
}

.toolbar {
  display: flex;
  margin-top: 10px;
  margin-bottom: 15px;
  align-items: center;
  height: 50px;
}

/* 输入框 */
.form__group {
  position: relative;
  padding: 20px 0 0;
  width: 100%;
  max-width: 180px;
}

.form__field {
  width: 100%;
  border: none;
  border-bottom: 2px solid black;
  outline: 0;
  font-size: 17px;
  color: #fff;
  padding: 7px 0;
  background: transparent;
  transition: border-color 0.2s;
}

.form__field::placeholder {
  color: transparent;
}

.form__field:placeholder-shown~.form__label {
  font-size: 17px;
  cursor: text;
  top: 20px;
}

.form__label {
  position: absolute;
  top: 0;
  display: block;
  transition: 0.2s;
  font-size: 17px;
  color: black;
  pointer-events: none;
}

.form__field:focus {
  padding-bottom: 6px;
  font-weight: 700;
  border-width: 3px;
  border-image: linear-gradient(to right, #116399, #38caef);
  border-image-slice: 1;
}

.form__field:focus~.form__label {
  position: absolute;
  top: 0;
  display: block;
  transition: 0.2s;
  font-size: 17px;
  color: #38caef;
  font-weight: 700;
}

/* reset input */
.form__field:required,
.form__field:invalid {
  box-shadow: none;
}

.clear {
  margin-left: auto;
  align-self: center;
  justify-content: center;
  /* 将按钮推到最右边 */
}

.search {
  align-self: center;
  justify-content: center;
}

/* 清空按钮 */
.clear button[type="submit"] {
  background-color: #4e99e9;
  border: none;
  color: #fff;
  cursor: pointer;
  padding: 10px 20px;
  border-radius: 20px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  top: 0;
  right: 0;
  transition: .3s ease;
  margin-right: 20px;
}

.clear button[type="submit"]:hover {
  transform: scale(1.1);
  color: rgb(255, 255, 255);
  background-color: blue;
}

/* 查找按钮 */
.search button[type="submit"] {
  background-color: #4e99e9;
  border: none;
  color: #fff;
  cursor: pointer;
  padding: 10px 20px;
  border-radius: 20px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  top: 0;
  right: 0;
  transition: .3s ease;
  margin-right: 20px;
}

.search button[type="submit"]:hover {
  transform: scale(1.1);
  color: rgb(255, 255, 255);
  background-color: blue;
}

/* 删除按钮 */
.delbtn {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  background-color: rgb(20, 20, 20);
  border: none;
  font-weight: 600;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.164);
  cursor: pointer;
  transition-duration: .3s;
  overflow: hidden;
  position: relative;
}

.svgIcon {
  width: 12px;
  transition-duration: .3s;
}

.svgIcon path {
  fill: white;
}

.delbtn:hover {
  width: 140px;
  border-radius: 50px;
  transition-duration: .3s;
  background-color: rgb(255, 69, 69);
  align-items: center;
}

.delbtn:hover .svgIcon {
  width: 50px;
  transition-duration: .3s;
  transform: translateY(60%);
}

.delbtn::before {
  position: absolute;
  top: -20px;
  content: "删除";
  color: white;
  transition-duration: .3s;
  font-size: 2px;
}

.delbtn:hover::before {
  font-size: 13px;
  opacity: 1;
  transform: translateY(30px);
  transition-duration: .3s;
}

.bottom {
  display: flex;
  justify-content: flex-end;
  align-items: center;
  padding: 10px;
}

/* 表格样式 */
.customerTable {
  flex-grow: 1;
  display: flex;
  table-layout: fixed;
  width: 100%;
  z-index: 0;
  height: 200px; /* 设置固定高度 */
  overflow: auto; /* 自动处理滚动条 */
}

:deep(.el-table__row.warning-row) {
  background: whitesmoke;
}

:deep(.el-table__row.highlight-row) {
  background: rgb(0, 153, 255);
  /* 高亮行的背景颜色 */
}

:deep(.el-table th)， :deep(.el-table tr)， :deep(.el-table td) {
  background-color: transparent;
  border: 0px;
  font-family: Source Han Sans CN Normal, Source Han Sans CN Normal-Normal;
}

.el-table::before {
  height: 0px;
}
</style>
