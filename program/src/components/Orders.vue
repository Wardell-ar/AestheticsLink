<template>
  <div class="my-surgeries">
      <h3>我的手术</h3>
      <div class="filters">
          执行状态：
          <el-select v-model="filterStatus" placeholder="筛选订单执行状态">
              <el-option label="全部" value="all"></el-option>
              <el-option label="已完成" value="completed"></el-option>
              <el-option label="未完成" value="notcompleted"></el-option>
          </el-select>
      </div>
      <div class="grid">
          <table>
              <thead>
                  <tr>
                      <th>手术名称</th>
                      <th>手术医院</th>
                      <th>手术室</th>
                      <th>日期</th>
                      <th>开始时间</th>
                      <th>手术时长</th>
                      <th>结束时间</th>
                      <th>状态</th>
                      <th>操作</th>
                  </tr>
              </thead>
              <tbody>
                  <tr v-for="surgery in filteredSurgeries" :key="surgery.id">
                      <td>{{ surgery.name }}</td>
                      <td>{{ surgery.hospital }}</td>
                      <td>{{ surgery.room }}</td>
                      <td>{{ surgery.date }}</td>
                      <td>{{ surgery.startTime }}</td>
                      <td>{{ surgery.duration }}</td>
                      <td>{{ surgery.endTime }}</td>
                      <td>{{ surgery.status }}</td>
                      <td>
                          <template v-if="surgery.status === '未完成'">
                              <el-button type="primary" @click="showRescheduleModal(surgery)">推迟</el-button>
                          </template>
                          <template v-else>
                              无法操作
                          </template>
                      </td>
                  </tr>
              </tbody>
          </table>
      </div>

      <!-- Reschedule Modal -->
      <el-dialog title="推迟手术" v-model="isRescheduleModalVisible" width="500">
          <div>
              是否确认推迟该手术？
          </div>
          <br>
          <span slot="footer" class="dialog-footer">
              <el-button @click="isRescheduleModalVisible = false">取消</el-button>
              <el-button type="primary" @click="confirmReschedule">确认</el-button>
          </span>
      </el-dialog>

      <!-- Confirmation Dialog -->
      <el-dialog title="手术推迟确认" v-model="isConfirmationDialogVisible" width="500">
          <div>
              手术已推迟，时间更改为：{{ confirmationMessage }}
          </div>
          <br>
          <span slot="footer" class="dialog-footer">
              <el-button @click="isConfirmationDialogVisible = false">关闭</el-button>
          </span>
      </el-dialog>
  </div>
</template>

<script>
import { getOperationInfo, OperationDelay } from "../HTTP/http"
import { get_id } from "../identification";
import { ElMessage } from 'element-plus'

export default {
  name: 'MySurgeries',
  data() {
      return {
          filterStatus: 'all',
          surgeries: [
              { id: '1', name: '割双眼皮', hospital: '医院一', room: '101', date: '2024-08-20', startTime: '10:00', duration: '1小时', endTime: '11:00', status: '未完成' },
              { id: '2', name: '隆鼻', hospital: '医院二', room: '102', date: '2024-08-21', startTime: '12:00', duration: '1小时', endTime: '13:00', status: '已完成' },
          ],  //需要后端的数据，显示手术的信息
          isRescheduleModalVisible: false,
          isConfirmationDialogVisible: false,
          selectedSurgery: null,
          confirmationMessage: '',
      };
  },
  mounted() {
      // 获取手术信息
      getOperationInfo(get_id(), this.setdata);
  },
  computed: {
      filteredSurgeries() {
          return this.surgeries.filter(surgery => {
              if (this.filterStatus === 'all') {
                  return true;
              }
              return this.filterStatus === 'completed' ? surgery.status === '已完成' : surgery.status === '未完成';
          });
      }
  },
  methods: {
      showRescheduleModal(surgery) {
          this.selectedSurgery = surgery;
          this.isRescheduleModalVisible = true;
      },
      // 更新时间
      confirmReschedule() {
          // 获取更新后的时间...
          OperationDelay(get_id(), this.selectedSurgery.billid, this.selectedSurgery.name, this.updateNewTime);
      },
      setdata(response) {
          this.surgeries = [];
          let i = 1;
          for (const item of response) {
              let obj = {
                  id: i + "",
                  billid: item.billid,
                  name: item.name,
                  hospital: item.hospital,
                  room: item.room,
                  date: item.year + "-" + item.month + "-" + item.day,
                  startTime: item.startTime,
                  duration: item.duration,
                  endTime: item.endTime,
                  status: (item.status == 0) ? "未完成" : "已完成"
              };
              this.surgeries.push(obj);
              i++;
          }
      },
      updateNewTime(response) {
          if (response == 0) {
              // 没有空闲时间，推迟失败
              this.isRescheduleModalVisible = false;
              ElMessage({
                  type: "warning",
                  message: "无空闲时间供推迟",
                  showClose: true
              });
          }
          else {
              console.log(response);
              // 修改新时间
              this.selectedSurgery.date = response.year + '-' + response.month + '-' + response.day;
              this.selectedSurgery.startTime = response.startTime;
              this.selectedSurgery.endTime = response.endTime;
              this.isRescheduleModalVisible = false;  // 推迟框消失

              // 推迟提示框
              this.confirmationMessage = `${this.selectedSurgery.date} ${this.selectedSurgery.startTime} - ${this.selectedSurgery.endTime}`;
              this.isConfirmationDialogVisible = true;
          }
      }
  }
};
</script>

<style scoped>
.my-surgeries {
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

  .dialog-footer {
      display: flex;
      justify-content: center;
      gap: 100px;
  }
}
</style>