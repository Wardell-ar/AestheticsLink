<!-- <template>
  <div class="customerTable">
    <el-table :data="tableData" style="width: 100%" :row-class-name="tableRowClassName">
      <el-table-column prop="id" label="编号" width="180"></el-table-column>
      <el-table-column label="选择" width="150">
        <template #default="scope">
          <el-checkbox v-model="scope.row.selected" @change="handleSelectionChange(scope.row)"></el-checkbox>
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
  </div>
</template>

<script>
export default {
  props:['tableData'],
  data() {
    return {
      selectedRowIds: [],
      tableData: [
        { id: 1, selected: false, name: '张三', customerID: '225354', gender: '男', phone: '1307909', age: 21, membershipLevel: 4, accountBalance: 3000 },
        { id: 2, selected: false, name: '李四', customerID: '27392', gender: '女', phone: '4131313', age: 88, membershipLevel: 5, accountBalance: 50000 },
        { id: 3, selected: false, name: '', customerID: '', gender: '', phone: '', age: '', membershipLevel: '', accountBalance: '' },
      ]
    };
  },
  method:{
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
    tableRowClassName({ row }) {
      // 判断行是否被选中
      return this.selectedRowIds.includes(row.id) ? 'highlight-row' : '';
    }
  }
};
</script>

<style scoped>
.customerTable {
  table-layout: fixed;
  width: 100%;
  z-index: 0;
}

.highlight-row {
  background-color:blue; /* 高亮行的背景颜色 */
}
</style> -->

<template>
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
export default {
  props: ['tableData'],
  data() {
    return {
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
  methods: {
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
.customerTable {
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
