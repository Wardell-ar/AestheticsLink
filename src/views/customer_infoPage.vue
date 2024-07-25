<template>
  <div class="title">
    顾客信息资料表
  </div>
  <div class="toolbar">
    <input type="text" v-model="agemin" placeholder="请输入年龄的下界">
    <p style="font-size:larger">~</p>
    <input type="text" v-model="agemax" placeholder="请输入年龄的上界">
    <input type="text" v-model="name" placeholder="请输入姓名">
    <input type="text" v-model="phone_num" placeholder="请输入手机号">
    <Dropdown_gender @updateGender="getGender" :clearData="clearData" @reset-clear-data="Reset" />
    <Dropdown_level @updateLevel="getLevel" :clearData="clearData" @reset-clear-data="Reset" />
    <div class="clear">
      <button class="clearbtn" @click="Clear">清空</button>
    </div>
    <div class="search">
      <button class="searchbtn" @click="Search">查询</button>
    </div>
  </div>
  <div class="customerTable">
    <el-table :data="tableData" style="width: 100%" :row-class-name="tableRowClassName" empty-text="暂无数据">
      <el-table-column prop="id" label="编号" width="180"></el-table-column>
      <el-table-column prop="selected" label="选择" width="150">
        <template #default="scope">
          <el-checkbox v-model="scope.row.selected" @change="() => handleSelectionChange(scope.row)"></el-checkbox>
        </template>
      </el-table-column>
      <el-table-column prop="name" label="姓名" width="200"></el-table-column>
      <el-table-column prop="customerID" label="顾客ID" width="220"></el-table-column>
      <el-table-column prop="gender" label="性别" width="150"></el-table-column>
      <el-table-column prop="phone" label="联系电话" width="270"></el-table-column>
      <el-table-column prop="age" label="年龄" width="150"></el-table-column>
      <el-table-column prop="membershipLevel" label="会员等级" width="200"></el-table-column>
      <el-table-column prop="accountBalance" label="账户余额" width="250"></el-table-column>
    </el-table>
    <p>{{ selectedRowIds }}</p>
  </div>

</template>

<script>
import Dropdown_gender from '@/components/Dropdown_gender.vue'
import Dropdown_level from '@/components/Dropdown_level.vue'
import { searchCustomerInfo } from '@/assets/http.js'

export default {
  props: ['tableData'],
  data() {
    return {
      agemin: "",
      agemax: "",
      name: "",
      phone_num: "",
      gender: "",
      viplevel: "",
      clearData: false,
      // 选中的行 ID 集合
      selectedRowIds: [],
      tableData: [
        { id: 1, selected: false, name: '张三', customerID: '225354', gender: '男', phone: '1307909', age: 21, membershipLevel: 4, accountBalance: 3000 },
        { id: 2, selected: false, name: '李四', customerID: '27392', gender: '女', phone: '4131313', age: 88, membershipLevel: 5, accountBalance: 50000 },
        { id: 1, selected: false, name: '张三', customerID: '225354', gender: '男', phone: '1307909', age: 21, membershipLevel: 4, accountBalance: 3000 },
        { id: 2, selected: false, name: '李四', customerID: '27392', gender: '女', phone: '4131313', age: 88, membershipLevel: 5, accountBalance: 50000 },
        { id: 3, selected: false, name: '', customerID: '', gender: '', phone: '', age: '', membershipLevel: '', accountBalance: '' },
      ]
    };
  },
  components: {
    Dropdown_gender,
    Dropdown_level
  },
  methods: {
    getGender(data) {
      this.gender = data;
    },
    getLevel(data) {
      this.viplevel = data;
    },
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
    Search() {
      const dataToSend = {
        // ...(this.agemin && { agemin: this.agemin }),
        // ...(this.agemax && { agemax: this.agemax }),
        // ...(this.name && { name: this.name }),
        // ...(this.phone_num && { phone_num: this.phone_num }),
        // ...(this.gender && { gender: this.gender }),
        // ...(this.viplevel && { viplevel: this.viplevel }),
        agemin: 20
      }
      searchCustomerInfo(dataToSend, this.displayTable);
    },
    displayTable(response) {
      console.log(response);
    },



    
    handleSelectionChange(row) {
      if (row.selected) {
        this.selectedRowIds.push(row.id);
      } else {
        const index = this.selectedRowIds.indexOf(row.id);
        if (index > -1) {
          this.selectedRowIds.splice(index, 1);
        }
      }
    },
    tableRowClassName({ row, rowIndex }) {
      if (row.selected) {
        return "highlight-row";
      }
      if (rowIndex % 2 == 0) {
        return "warning-row";
      }
      else {
        return "";
      }
    }
  }
}

</script>

<style scoped>
.title {
  background-color: whitesmoke;
  text-align: center;
  line-height: 60px;
  height: 6%;
  margin-bottom: 4px;
  box-shadow: 0 2px 2px rgba(0, 0, 0, 0.2);
}

.toolbar {
  display: flex;
  margin-bottom: 20px;
}

.toolbar input {
  border-radius: 10px;
  font-size: medium
}

.showlogo img {
  width: 40%;
  height: auto;
}

.showlogo p {
  align-content: center;
  font-size: 25px;
}

.display-table {
  display: flex;
}

.clear {
  margin-left: auto;
  /* 将按钮推到最右边 */
}

.clearbtn {
  background-color: rgb(54, 63, 198);
  height: 100%;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: 1px solid grey;
  cursor: pointer;
}

.clearbtn:active {
  background-color: black;
  transform: scale(0.98);
}

.searchbtn {
  background-color: rgb(54, 63, 198);
  height: 100%;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: 1px solid grey;
  cursor: pointer;
}

.searchbtn:active {
  background-color: black;
  transform: scale(0.98);
}









.customerTable {
  display: flex;
  table-layout: fixed;
  width: 100%;
  z-index: 0;
}

::v-deep .el-table__row.warning-row {
  background: whitesmoke;
}

::v-deep .el-table__row.highlight-row {
  background: rgb(0, 153, 255);
  /* 高亮行的背景颜色 */
}

::v-deep .el-table th,
::v-deep .el-table tr,
::v-deep .el-table td {
  background-color: transparent;
  border: 0px;
  font-family: Source Han Sans CN Normal, Source Han Sans CN Normal-Normal;
}

.el-table::before {
  height: 0px;
}
</style>
