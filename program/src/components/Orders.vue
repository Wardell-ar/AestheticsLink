<template>
    <div class="orders">
      <h3>我的订单</h3>
      <div class="filters">
        执行状态：
        <el-select v-model="filterStatus" placeholder="筛选订单执行状态">
          <el-option label="全部" value="all"></el-option>
          <el-option label="已执行" value="executed"></el-option>
          <el-option label="未执行" value="not-executed"></el-option>
        </el-select>
        支付状态：
        <el-select v-model="filterPaymentStatus" placeholder="筛选支付状态">
          <el-option label="全部" value="all"></el-option>
          <el-option label="已支付" value="paid"></el-option>
          <el-option label="未支付" value="unpaid"></el-option>
          <el-option label="已退款" value="refunded"></el-option>
        </el-select>
      </div>
      <div class="grid">
        <table>
          <thead>
            <tr>
              <th>订单编号</th>
              <th>订单名称</th>
              <th>订单创建日期</th>
              <th>订单支付金额</th>
              <th>支付状态</th>
              <th>订单执行状态</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="order in filteredOrders" :key="order.id">
              <td>{{ order.id }}</td>
              <td>{{ order.name }}</td>
              <td>{{ order.creationDate }}</td>
              <td>{{ order.amount }}</td>
              <td>{{ order.paymentStatus }}</td>
              <td>{{ order.executionStatus }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'Orders',
    data() {
      return {
        filterStatus: 'all',
        filterPaymentStatus: 'all',
        orders: [
          { id: '123456', name: '割双眼皮', creationDate: '2023-07-10', amount: 100.0, paymentStatus: '已支付', executionStatus: '已执行' },
          { id: '123457', name: '隆鼻', creationDate: '2023-07-11', amount: 200.0, paymentStatus: '未支付', executionStatus: '未执行' },
          { id: '123458', name: '减脂塑性', creationDate: '2023-07-12', amount: 300.0, paymentStatus: '已退款', executionStatus: '未执行' },
          { id: '123459', name: '祛斑祛痘', creationDate: '2023-07-13', amount: 400.0, paymentStatus: '已支付', executionStatus: '已执行' },
        ],
      };
    },
    computed: {
      filteredOrders() {
        let orders = this.orders;
        
        if (this.filterStatus !== 'all') {
          orders = orders.filter(order => 
            this.filterStatus === 'executed' ? order.executionStatus === '已执行' : order.executionStatus === '未执行'
          );
        }
  
        if (this.filterPaymentStatus !== 'all') {
          orders = orders.filter(order => 
            this.filterPaymentStatus === 'paid' ? order.paymentStatus === '已支付' : 
            this.filterPaymentStatus === 'unpaid' ? order.paymentStatus === '未支付' : 
            order.paymentStatus === '已退款'
          );
        }
  
        return orders;
      },
    },
  };
  </script>
  
  <style lang="scss" scoped>
  .orders {
    padding: 20px;
    background-color: #f9f9f9;
  
    h3 {
      margin-bottom: 30px;
      color: #007bff;
    }
  
    .filters {
      margin-bottom: 20px;
  
      .el-select {
        width: 200px;
        margin-right: 10px;
      }
    }
  
    .grid {
      table {
        width: 100%;
        border-collapse: collapse;
  
        thead {
          background-color: #333;
          color: #fff;
  
          th {
            padding: 10px;
            border: 1px solid #007bff;
          }
        }
  
        tbody {
          tr {
            &:nth-child(even) {
              background-color: #f2f2f2;
            }
  
            &:hover {
              background-color: #e0e0e0;
            }
  
            td {
              padding: 10px;
              border: 1px solid #007bff;
              text-align: center;
            }
          }
        }
      }
    }
  }
  </style>