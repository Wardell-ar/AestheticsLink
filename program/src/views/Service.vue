<template>
  <div class="main-container"  v-if="IsAdmin">
    <div class="title">
      服务信息资料表
    </div>
    <div class="toolbar">

      <div class="form__group field" style="margin: 10px">
        <input type="input" class="form__field" placeholder="Name" required="" v-model="proj_id">
        <label class="form__label">项目ID</label>
      </div>
      <div class="form__group field" style="margin: 10px">
        <input type="input" class="form__field" placeholder="Name" required="" v-model="name">
        <label class="form__label">项目名称</label>
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
        <el-table-column prop="index" label="编号" width="100"></el-table-column>
        <el-table-column prop="selected" label="选择" width="150">
          <template #default="scope">
            <el-checkbox v-model="scope.row.selected" @change="() => handleSelectionChange(scope.row)"></el-checkbox>
          </template>
        </el-table-column>
        <el-table-column prop="proj_id" label="项目ID"></el-table-column>
        <el-table-column prop="name" label="项目名称"></el-table-column>
        <el-table-column prop="price" label="项目价格">
          <template #default="scope">
            <span>{{ scope.row.price }}</span>
            <el-button type="text" @click="editPrice(scope.row)" style="margin-left: 15px">修改</el-button>
          </template>
        </el-table-column>
        <el-table-column prop="found_date" label="创建日期"></el-table-column>
      </el-table>
    </div>
    <div class="bottom">
      <div tabindex="0" class="plusButton" @click="openModal" style="margin-right: 2px;">
        <svg class="plusIcon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 30 30">
          <g mask="url(#mask0_21_345)">
            <path d="M13.75 23.75V16.25H6.25V13.75H13.75V6.25H16.25V13.75H23.75V16.25H16.25V23.75H13.75Z"></path>
          </g>
        </svg>
      </div>

      <multi-input-modal ref="multiInputModal" @submit="handleSubmit"></multi-input-modal>
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
import { searchService, deleteService, changeService, addService } from '../HTTP/http'
import { onMounted, getCurrentInstance } from 'vue';
import MultiInputModal from '../components/MultiInputModal_service.vue';
import { get_role } from '@/identification';

export default {
  components: {
    MultiInputModal
  },
  data() {
    return {
      proj_id: "",
      price: "",
      name: "",
      found_date: "",
      clearData: false,
      // 选中的ID集合
      selected_Ids: [],
      // 表单数据
      tableData: [],
      IsAdmin: get_role() == "0"
    };
  },
  methods: {
    // 清空筛选条件方法实现
    Clear() {
      this.proj_id = "",
        this.name = "",
        this.clearData = true;
    },
    Reset() {
      this.clearData = false;
    },

    // 查询方法实现
    Search() {
      const dataToSend = {
        proj_id: this.proj_id === "" ? "null" : this.proj_id,
        name: this.name === "" ? "null" : this.name
      }
      this.selected_Ids = [],
        searchService(dataToSend, this.displayTable);
    },
    displayTable(response) {
      this.tableData = response; // 将处理后的数据存储在 tableData 中
      this.deselectAll();
    },

    // 删除方法实现
    Delete() {
      console.log(this.selected_Ids);
      deleteService(this.selected_Ids, this.Search);
    },

    // 选中栏恢复未选择的状态方法实现
    deselectAll() {
      this.tableData.forEach(row => {
        row.selected = false;
      });
    },

    //选择框是否选择的方法实现
    handleSelectionChange(row) {
      if (row.selected) {
        this.selected_Ids.push(row.proj_id);
      } else {
        const index = this.selected_Ids.indexOf(row.proj_id);
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

    // 弹窗效果实现
    openModal() {
      this.$refs.multiInputModal.open({}); // 打开弹窗，不传入任何数据
    },

    // 提交后端实现
    handleSubmit(datatoSend) {
      console.log(datatoSend)
      addService(datatoSend,this.Search);
    },

    //修改价格
    editPrice(row) {
      this.$prompt('请输入新的项目价格', '修改价格', {
        confirmButtonText: '确认',
        cancelButtonText: '取消',
        inputPattern: /^[0-9]+(\.[0-9]{1,2})?$/, // 价格格式验证
        inputErrorMessage: '请输入有效的数字格式'
      }).then(({ value }) => {
        row.price = parseFloat(value);

        const datatoSend = {
          proj_id: row.proj_id, 
          price: row.price + ""
        };
        console.log(datatoSend);
        changeService(datatoSend,this.Search);
        this.$message({
          type: 'success',
          message: '价格修改成功'
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消修改'
        });
      });
    }
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

/* 增加按钮 */
.plusButton {
  /* Config start */
  --plus_sideLength: 2.8rem;
  --plus_topRightTriangleSideLength: 0.5rem;
  /* Config end */
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  width: var(--plus_sideLength);
  height: var(--plus_sideLength);
  background-color: #000000;
  overflow: hidden;
}

.plusButton::before {
  position: absolute;
  content: "";
  top: 0;
  right: 0;
  width: 0;
  height: 0;
  border-width: 0 var(--plus_topRightTriangleSideLength) var(--plus_topRightTriangleSideLength) 0;
  border-style: solid;
  border-color: transparent white transparent transparent;
  transition-timing-function: ease-in-out;
  transition-duration: 0.2s;
}

.plusButton:hover {
  cursor: pointer;
}

.plusButton:hover::before {
  --plus_topRightTriangleSideLength: calc(var(--plus_sideLength) * 2);
}

.plusButton:focus-visible::before {
  --plus_topRightTriangleSideLength: calc(var(--plus_sideLength) * 2);
}

.plusButton>.plusIcon {
  fill: white;
  width: calc(var(--plus_sideLength) * 0.7);
  height: calc(var(--plus_sideLength) * 0.7);
  z-index: 1;
  transition-timing-function: ease-in-out;
  transition-duration: 0.2s;
}

.plusButton:hover>.plusIcon {
  fill: black;
  transform: rotate(180deg);
}

.plusButton:focus-visible>.plusIcon {
  fill: black;
  transform: rotate(180deg);
}
</style>