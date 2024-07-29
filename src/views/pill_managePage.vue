<template>
    <div class="main-container">
      <div class="title">
        医药信息资料表
      </div>
      <div class="toolbar">
        <input type="text" v-model="agemin" placeholder="请输入年龄的下界">
        <p style="font-size: larger; align-self: center;">~</p>
        <input type="text" v-model="agemax" placeholder="请输入年龄的上界" style="margin-right: 10px;">
        <input type="text" v-model="name" placeholder="请输入姓名" style="margin-right: 10px;">
        <input type="text" v-model="phone_num" placeholder="请输入手机号" style="margin-right: 10px;">
        <Dropdown_gender @updateGender="getGender" :clearData="clearData" @reset-clear-data="Reset" style="margin-right: 10px;"/>
        <Dropdown_level @updateLevel="getLevel" :clearData="clearData" @reset-clear-data="Reset" />
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
          <el-table-column prop="g_id" label="药品ID"></el-table-column>
          <el-table-column prop="hos_id" label="医院ID"></el-table-column>
          <el-table-column prop="name" label="药品名称"></el-table-column>
          <el-table-column prop="price" label="药品价格"></el-table-column>
          <el-table-column prop="producer" label="生产商家"></el-table-column>
          <el-table-column prop="storage" label="药品库存"></el-table-column>
          <el-table-column prop="sell_by_date" label="到期日期"></el-table-column>
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
  import Dropdown_gender from '@/components/Dropdown_gender.vue'
  import Dropdown_level from '@/components/Dropdown_level.vue'
  import { searchCustomerInfo, deleteCustomerInfo } from '@/assets/http.js'
  import { onMounted, getCurrentInstance } from 'vue';
  
  export default {
    data() {
      return {
        // 筛选条件
        agemin: "",
        agemax: "",
        name: "",
        phone_num: "",
        gender: "",
        viplevel: "",
        clearData: false,
        // 选中的行ID集合
        selectedRowIds: [],
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
        this.gender = selectedOption;
      },
      getLevel(selectedOption) {
        this.viplevel = selectedOption;
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
  