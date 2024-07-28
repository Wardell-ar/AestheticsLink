<!-- HospitalFinance.vue -->
<template>
  <div class="hospital-finance">
    <div class="title">
      收支统计
    </div>
    <div class="header">
      <el-date-picker
        v-model="date"
        type="month"
        placeholder="选择年份和月份"
        style="margin-right: 10px;">
      </el-date-picker>
      <el-select v-model="selectedHospital" placeholder="选择医院" style="margin-right: 10px;">
        <el-option v-for="hospital in hospitals"
          :key="hospital.id"
          :label="hospital.label"
          :value="hospital.value">
        </el-option>
      </el-select>
      <el-button type="primary" @click="fetchData">查询</el-button>
    </div>
    <el-table :data="tableData" style="width: 100%; margin-top: 20px;" stripe="true" height="540">
      <el-table-column prop="time" label="时间" width="150"></el-table-column>
      <el-table-column prop="hospitalName" label="院名" width="600"></el-table-column>
      <el-table-column prop="income" label="收入" width="150"></el-table-column>
      <el-table-column prop="expense" label="支出" width="150"></el-table-column>
      <el-table-column prop="profit" label="利润" width="150"></el-table-column>
    </el-table>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'HospitalFinance',
  data() {
    return {
      date: '',
      selectedHospital: '',
      hospitals: [
        { id:1, value: 'hospital1', label: '医院1' },
        { id:2, value: 'hospital2', label: '医院2' },
        { id:3, value: 'hospital3', label: '医院3' },
      ],
      tableData: [
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
          {time: 2022.7,hospitalName: "医院1",income: 600,expense: 200,profit: 400},
        ],
    };
  },
  methods: {
    async fetchData() {
      if (!this.date || !this.selectedHospital) {
        this.$message.error('请选择月份和医院');
        return;
      }

      try {
        const [ordersResponse, salariesResponse] = await Promise.all([
          axios.get(`/api/orders?hospital=${this.selectedHospital}&date=${this.date}`),
          axios.get(`/api/salaries?hospital=${this.selectedHospital}&date=${this.date}`)
        ]);

        const orders = ordersResponse.data;
        const salaries = salariesResponse.data;

        const income = orders.reduce((sum, order) => sum + order.amount, 0);
        const expense = salaries.reduce((sum, salary) => sum + salary.amount, 0);
        const profit = income - expense;

        this.tableData = [{
          time: this.date,
          hospitalName: this.hospitals.find(h => h.value === this.selectedHospital).label,
          income: income,
          expense: expense,
          profit: profit
        }];
      } catch (error) {
        console.error('Failed to fetch data', error);
        this.$message.error('获取数据失败');
      }
    }
  }
};
</script>

<style lang="scss" scoped>
.hospital-finance {
  padding: 20px;
  margin: 20px; 
  background-color: #ffffff;
  border-radius: 10px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  flex-grow:1;

  .title {
    background-color: fff;
    height: 10%;
    font-size: 24px;
    margin-bottom: 20px; 
    color:black;
  }

  .header {
    display: flex;
    align-items: center;
    margin-bottom: 20px;

    .el-date-picker,
    .el-select {
      margin-right: 10px;
    }

    .el-button {
      background-color: #007bff;
      border-color: #007bff;
      color: #fff;

      &:hover {
        background-color: #0056b3;
        border-color: #0056b3;
      }
    }
  }

  .el-table {
    .el-table__header-wrapper {
      background-color: #007bff;
    }

    .el-table__header th {
      color: #fff;
      background-color: #007bff;
      border-bottom: 1px solid #d9ecff;
    }

    .el-table__body td {
      background-color: #f4f4f4;
      border-bottom: 1px solid rgba(0, 0, 0, 0.1);
      color:black;
    }

    .el-table__row:hover > td {
      background-color: #e6f7ff;
    }
  }
}
</style>