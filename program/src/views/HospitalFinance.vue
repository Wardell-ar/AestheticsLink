<template>
    <div class="hospital-finance"  v-if="IsAdmin">
        <div class="title">
            收支统计
            <!-- <img src="../assets/UI/收支管理.png" alt="img" width="50px" height="50px" position="absolute"
        transform="translate(-50%,-50%)" > -->
        </div>
        <div class="header">
            <el-date-picker v-model="date" type="month" placeholder="选择年份和月份" style="margin-right: 10px;">
            </el-date-picker>
            <el-select v-model="selectedHospital" placeholder="选择医院" style="margin-right: 10px;">
                <el-option v-for="hospital in hospitals" :key="hospital.id" :label="hospital.label"
                    :value="hospital.value">
                </el-option>
            </el-select>
            <el-button type="primary" @click="SearchData">查询</el-button>
            <!-- <img src="../assets/UI/查询.png" alt="img" @click="fetchData" class="query"> -->
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
import { getHospitalNameReq, getFinanceReq } from "../HTTP/http"
import { get_role } from '@/identification';
export default {
    name: 'HospitalFinance',
    data() {
        return {
            date: '',
            selectedHospital: '',
            hospitalname: "",
            hospitals: [],  //需要从后端获取医院名称的数据
            tableData: [
                {
                    time: "",
                    hospitalName: "",
                    income: "",
                    expense: "",
                    profit: ""
                },
            ],  //需要从后端获取具体的收支，profit从income和expense计算出来的
            IsAdmin: get_role() == "0"
        };
    },
    methods: {
        SearchData() {
            if (this.date == "" || this.date == null || this.selectedHospital == "" || this.selectedHospital == null) {
                alert("输入内容不能为空");
            }
            else {
                const year = this.date.getFullYear() + "";
                let temp = this.date.getMonth() + 1;
                const month = temp < 10 ? "0" + temp : temp + "";
                for (const item of this.hospitals) {  // 寻找是在查询哪一个医院
                    if (this.selectedHospital == item.value) {
                        this.hospitalname = item.label;
                    }
                }
                getFinanceReq(year, month, this.hospitalname, this.setResult);
            }
        },
        setHospitalName(response) {  // 处理后端传来的医院名称
            console.log(response);
            for (const name of response) {
                const len = this.hospitals.length;
                if (len == 0) {
                    let obj = {
                        id: 1,
                        value: "hospital1",
                        label: name
                    };
                    this.hospitals.push(obj);
                }
                else {
                    let lastid = this.hospitals[len - 1].id;
                    let obj = {
                        id: lastid + 1,
                        value: "hospital" + (lastid + 1),
                        label: name
                    };
                    this.hospitals.push(obj);
                }
            }
        },
        setResult(response) {  // 处理后端传来的查询结构
            const year = this.date.getFullYear() + "";
            let temp = this.date.getMonth() + 1;
            const month = temp < 10 ? "0" + temp : temp + "";
            this.tableData[0].time = year + "-" + month;
            this.tableData[0].hospitalName = this.hospitalname;
            if (response.income == -1) {
                this.tableData[0].income = "暂无结果";
                this.tableData[0].expense = "暂无结果";
                this.tableData[0].profit = "暂无结果";
                return;
            }
            this.tableData[0].income = response.income;
            this.tableData[0].expense = response.payout;
            this.tableData[0].profit = response.income - response.payout;
        }
    },
    beforeMount() {
        getHospitalNameReq(this.setHospitalName);
    },
};
</script>

<style lang="scss" scoped>
.hospital-finance {
    padding: 20px;
    margin: 20px;
    background-color: #ffffff;
    border-radius: 10px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    flex-grow: 1;
    display: flex;
    flex-direction: column;

    .title {
        background-color: #fff;
        height: 10%;
        font-size: 24px;
        margin-bottom: 20px;
        color: black;
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
        flex-grow: 1;

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
            color: black;
        }

        .el-table__row:hover>td {
            background-color: #e6f7ff;
        }
    }

    // .query {
    //   width: 40px;
    //   height: 40px;
    // }
}
</style>