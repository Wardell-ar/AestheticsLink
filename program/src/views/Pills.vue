<template>
    <div class="main-container">
        <div class="title">
            医药信息资料表
        </div>
        <div class="toolbar">

            <div class="form__group field" style="margin: 10px">
                <input type="input" class="form__field" placeholder="Hos_id" required="" v-model="hos_id">
                <label class="form__label">医院ID</label>
            </div>
            <div class="form__group field" style="margin: 10px">
                <input type="input" class="form__field" placeholder="Name" required="" v-model="name">
                <label class="form__label">药品名称</label>
            </div>

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
                        <el-checkbox v-model="scope.row.selected"
                            @change="() => handleSelectionChange(scope.row)"></el-checkbox>
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
            <div class="search">
                <button type="submit" @click="Replenish">一键补药</button>
            </div>
        </div>
    </div>
</template>

<script>
import { searchPills, ReplenishPills } from '../HTTP/http'
import { onMounted, getCurrentInstance } from 'vue';

export default {
    data() {
        return {
            g_id: "",
            hos_id: "",
            name: "",
            price: "",
            producer: "",
            storage: "",
            sell_by_date: "",
            clearData: false,
            // 选中的ID集合
            selected_Ids: [],
            // 表单数据
            tableData: []
        };
    },
    methods: {
        // 清空筛选条件方法实现
        Clear() {
            this.g_id = "",
                this.hos_id = "",
                this.name = "",
                this.price = "",
                this.producer = "",
                this.storage = "",
                this.sell_by_date = "",
                this.clearData = true
        },
        Reset() {
            this.clearData = false
        },

        // 查询方法实现
        Search() {
            const dataToSend = {
                hos_id: this.hos_id === "" ? "null" : this.hos_id,
                name: this.name === "" ? "null" : this.name
            }
            console.log(dataToSend)
            this.selected_Ids = [],
                searchPills(dataToSend, this.displayTable);
        },
        displayTable(response) {
            this.tableData = response; // 将处理后的数据存储在 tableData 中
            this.deselectAll();
        },

        // 选中栏恢复未选择的状态方法实现
        deselectAll() {
            this.tableData.forEach(row => {
                row.selected = false;
            });
        },

        // 一键补药的方法实现
        Replenish() {
            ReplenishPills()
        },

        //选择框是否选择的方法实现
        handleSelectionChange(row) {
            if (row.selected) {
                this.selected_Ids.push({
                    g_id: row.g_id,
                    hos_id: row.hos_id
                });
            } else {
                const index = this.selected_Ids.findIndex(item => item.g_id === row.g_id && item.hos_id === row.hos_id);
                if (index > -1) {
                    this.selected_Ids.splice(index, 1);
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

:deep(.el-table th)，:deep(.el-table tr)，:deep(.el-table td) {
    background-color: transparent;
    border: 0px;
    font-family: Source Han Sans CN Normal, Source Han Sans CN Normal-Normal;
}

.el-table::before {
    height: 0px;
}
</style>