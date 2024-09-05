<template>
    <div class="main-container">
      <div class="title">
        员工信息资料表
      </div>
      <div class="toolbar">
        <input type="text" v-model="salarymin" placeholder="请输入薪水的下界">
      <p style="font-size: larger; align-self: center;">~</p>
      <input type="text" v-model="salarymax" placeholder="请输入薪水的上界" style="margin-right: 10px;">
      <input type="text" v-model="emp_id" placeholder="请输入员工ID" style="margin-right: 10px;">
      <input type="text" v-model="name" placeholder="请输入姓名" style="margin-right: 10px;">
      <input type="text" v-model="phone_num" placeholder="请输入手机号" style="margin-right: 10px;">
        <div class="clear">
          <button class="clearbtn" @click="Clear">
            <img src="@/assets/pics/clear.png" alt="清空">
            <p>清空</p>
          </button>
        </div>
        <div class="search">
          <button class="searchbtn" @click="Search">
            <img src="@/assets/pics/search.png" alt="查询">
            <p>查询</p>
          </button>
        </div>
      </div>
      <div class="customerTable">
        <el-table :data="tableData" style="width: 100%" :row-class-name="tableRowClassName" empty-text="暂无数据">
          <el-table-column prop="index" label="编号" width="100"></el-table-column>
          <el-table-column prop="selected" label="选择" width="150">
            <template #default="scope">
              <el-checkbox v-model="scope.row.selected" @change="() => handleSelectionChange(scope.row)"></el-checkbox>
            </template>
          </el-table-column>
          <el-table-column prop="ser_id" label="员工ID"></el-table-column>
          <el-table-column prop="joined_date" label="员工入职日期"></el-table-column>
          <el-table-column prop="name" label="员工姓名"></el-table-column>
          <el-table-column prop="phone_num" label="员工联系电话"></el-table-column>
          <el-table-column prop="salary" label="员工薪水"></el-table-column>
          <el-table-column prop="position" label="员工职位"></el-table-column>
        </el-table>
      </div>
      <div class="bottom">
        <button class="delbtn" @click="Delete">
          <img src="@/assets/pics/delete.png" alt="删除" style="margin-bottom: 2px">
          <p>删除</p>
        </button>
      </div>
    </div>
  </template>
  
  <script>
  import { searchCustomerInfo, deleteCustomerInfo } from '../HTTP/http'
  import { onMounted, getCurrentInstance } from 'vue';
  
  export default {
    data() {
      return {
        // 筛选条件
        salarymin: "", // 最小薪水
        salarymax: "", // 最大薪水
        emp_id: "", // 员工ID
        name: "", // 姓名
        phone_num: "", // 手机号码
        clearData: false, // 是否清空数据标志

        // 选中的行ID集合
        selectedRowIds: [],

        // 选中的员工ID集合
        selectedCus_Ids: [],

        // 表单数据
        tableData: []
      };
    },
    methods: {
      // 清空筛选条件方法实现
      Clear() {
        this.salarymin = "";
        this.salarymax = "";
        this.emp_id = "";
        this.name = "";
        this.phone_num = "";
        this.clearData = true;
      },
      Reset() {
        this.clearData = false;
      },
  
      // 查询方法实现
      Search() {
      const dataToSend = {
        ...(this.salarymin && { salarymin: parseInt(this.salarymin) }),
        ...(this.salarymax && { salarymax: parseInt(this.salarymax) }),
        ...(this.emp_id && { emp_id: this.emp_id }),
        ...(this.name && { name: this.name }),
        ...(this.phone_num && { phone_num: this.phone_num }),
      };
      this.selectedRowIds = [];
      this.selectedCus_Ids = [];
      searchCustomerInfo(dataToSend, this.displayTable);
      },
      // 显示查询结果
      displayTable(response) {
        this.tableData = response;
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
  
      // 处理选中状态变化
      handleSelectionChange(row) {
        if (row.selected) {
          this.selectedRowIds.push(row.index);
          this.selectedCus_Ids.push(row.cus_id);
        } else {
          const rowindex = this.selectedRowIds.indexOf(row.index);
          const cusindex = this.selectedCus_Ids.indexOf(row.cus_id);
          if (rowindex > -1) {
            this.selectedRowIds.splice(rowindex, 1);
          }
          if (cusindex > -1) {
            this.selectedCus_Ids.splice(cusindex, 1);
          }
        }
      },

      // 表格行样式
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
    font-size: medium;
  }
  
  .clear {
    margin-left: auto;
    /* 将按钮推到最右边 */
  }
  
  .clearbtn,
  .searchbtn,
  .delbtn {
    display: flex;
    background-color: rgb(54, 63, 198);
    color: white;
    padding: 16px;
    font-size: 16px;
    border: 1px solid grey;
    cursor: pointer;
    align-items: center;
    margin-right: 10px;
    border-radius: 10px;
  }
  
  .clearbtn img,
  .searchbtn img,
  .delbtn img {
    width: 30px;
    height: 30px;
  }
  
  .clearbtn:active,
  .searchbtn:active,
  .delbtn:active {
    background-color: black;
    transform: scale(0.98);
  }
  
  .customerTable {
    flex-grow: 1;
    /* 填充中间部分的剩余空间 */
  }
  
  .bottom {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    padding: 10px;
  }
  
  /* 表格样式 */
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