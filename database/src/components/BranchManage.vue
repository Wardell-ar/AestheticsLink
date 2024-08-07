<template>
    <div class="common-layout">
    <el-container>
      <el-header class="header">
        <div>分院管理</div>
        <div class="select">
        <el-select v-model="selectedHospitalName" placeholder="选择分院" @change="handleChange" class="selectHos" size="large">
          <el-option v-for="hospital in hospitals" :key="hospital.id" :label="hospital.name" :value="hospital.name" >
          </el-option>
        </el-select>
        <el-button type="primary" plain size="large">确认</el-button>
      </div>
      </el-header>
      <el-main v-if="selectedHospital" class="main">
        <el-card class="search-container" shadow="always">
          <template #header>
            <div class="card-header">
              <i class="fas fa-magnifying-glass icon"></i>
              搜索
            </div>
          </template>
          <el-form :inline="true" :model="searchForm" class="demo-form-inline">
            <el-form-item label="部门编号">
              <el-input v-model="searchForm.branchId" placeholder="请输入部门编号"></el-input>
            </el-form-item>
            <el-form-item label="部门名称">
              <el-input v-model="searchForm.branchName" placeholder="请输入部门名称"></el-input>
            </el-form-item>
            <el-form-item label="部门状态">
              <el-select v-model="searchForm.status" placeholder="请选择部门状态">
                <el-option label="启用" value="1"></el-option>
                <el-option label="禁用" value="0"></el-option>
              </el-select>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="onSearch"><i class="fas fa-magnifying-glass icon"></i>搜索</el-button>
              <el-button @click="onReset"><i class="fas fa-rotate-right icon"></i>重置</el-button>
            </el-form-item>
          </el-form>
        </el-card>
        <el-card class="table-container">
          <div slot="header" class="clearfix">
            <span>{{selectedHospitalName}}分院部门列表</span>
            <div class="buttonGroup">
              <el-button type="primary" @click="handleAdd"><i class="fas fa-plus icon"></i>新增</el-button>
              <el-button type="danger" @click="handleBatchDelete" :disabled="multipleSelection.length === 0"><i class="fas fa-trash icon"></i>批量删除</el-button>
              <el-button @click="onRefresh"><i class="fas fa-arrows-rotate icon"></i>刷新</el-button>
            </div>
          </div>
          <el-table :data="paginatedData" border style="width: 100%" @selection-change="handleSelectionChange" :default-sort="{ prop: 'branchId', order: 'ascending' }">
            <el-table-column type="selection" width="55"></el-table-column>
            <el-table-column prop="branchId" sortable label="部门编号" ></el-table-column>
            <el-table-column prop="branchName" label="部门名称"></el-table-column>
            <el-table-column prop="status" label="部门状态">
              <template  #default="scope">
                <el-tag :type="scope.row.status === '1' ? 'success' : 'warning'">{{ scope.row.status === '1' ? '启用' : '禁用' }}</el-tag>
              </template>
              <template #header>
                <div class="header-wrapper">
                  <span class="header-label">部门状态</span>
                </div>
              </template>
            </el-table-column>
            <el-table-column prop="expense" sortable label="部门支出" ></el-table-column>
            <el-table-column prop="income" sortable label="部门收入" ></el-table-column>
            <el-table-column label="操作" width="180">
              <template  #default="scope">
                <el-button type="primary" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                <el-button type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination  class="custom-pagination" @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[5, 10, 30, 40]" :page-size="pageSize" layout="total, prev, pager, next,  jumper,sizes" :total="tableData.length">
          </el-pagination>
        </el-card>
        <el-drawer
          :title="operateType === 'add' ? '新增部门' : '编辑部门'"
          v-model="visible"
          :destroy-on-close="true"
          size="35%"
        >
          <el-form :model="newData" label-width="100px">
            <el-form-item label="部门编号">
              <el-input v-model="newData.branchId" />
            </el-form-item>
            <el-form-item label="部门名称">
              <el-input v-model="newData.branchName" />
            </el-form-item>
            <el-form-item label="部门收入">
              <el-input v-model="newData.income" />
            </el-form-item>
            <el-form-item label="部门支出">
              <el-input v-model="newData.expense" />
            </el-form-item>
            <el-form-item label="部门状态">
              <el-select v-model="newData.status">
                <el-option
                  v-for="option in enableStatusRecord"
                  :key="option.value"
                  :label="option.label"
                  :value="option.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="handleSubmit">保存</el-button>
              <el-button type="primary" @click="handleCancel">取消</el-button>
            </el-form-item>
          </el-form>
        </el-drawer>
      </el-main>
    </el-container>
  </div>
</template>
<script>
import { ref } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
export default {
  setup() {
    const selectedHospital = ref(null);
    const selectedHospitalName = ref('');
    const enableStatusRecord=[
      {
      value: '1',
      label: '启用',
      },
      {
      value: '0',
      label: '禁用',
      },
    ];
    const hospitals = ref([
      { id: '1', name: '医院 A' },
      { id: '2', name: '医院 B' },
      { id: '3', name: '医院 C' }
    ]);
    const searchForm = ref({
      branchName: '',
      branchId: '',
      status: '',
    });
    const newData = ref({
      branchName: '',
      branchId: '',
      expense:'',
      income:'',
      status: '1',
    });
    const handleChange = () => {
      selectedHospital.value = hospitals.value.find(hospital => hospital.name === selectedHospitalName.value);
    };
    const tableData=ref([
    {
      "branchId": "101",
      "branchName": "部门A",
      "status": '1',
      "income": '150000',
      "expense": '100000'
    },
    {
      "branchId": "102",
      "branchName": "部门B",
      "status": '0',
      "income": '200000',
      "expense": '180000'
    },
    {
      "branchId": "103",
      "branchName": "部门C",
      "status": '1',
      "income": '250000',
      "expense": '120000'
    },
    {
      "branchId": "104",
      "branchName": "部门D",
      "status": '1',
      "income": '300000',
      "expense": '150000'
    },
    {
      "branchId": "105",
      "branchName": "部门E",
      "status": '0',
      "income": '180000',
      "expense": '130000'
    },
    {
      "branchId": "106",
      "branchName": "部门F",
      "status": '1',
      "income": '220000',
      "expense": '160000'
    },
    {
      "branchId": "107",
      "branchName": "部门G",
      "status": '1',
      "income": '270000',
      "expense": '190000'
    },
    {
      "branchId": "108",
      "branchName": "部门H",
      "status": '0',
      "income": '240000',
      "expense": '170000'
    },
    {
      "branchId": "109",
      "branchName": "部门I",
      "status": '1',
      "income": '260000',
      "expense": '140000'
    },
    {
      "branchId": "110",
      "branchName": "部门J",
      "status": '0',
      "income": '230000',
      "expense": '110000'
    }
  ]);
    const visible=ref(false);
    const filteredData = ref([]);
    const operateType=ref('');
    const multipleSelection = ref([]);
    const currentPage = ref(1);
    const pageSize = ref(5);
    const onSearch = () => {
      const { branchId, branchName, status } = searchForm.value;
      filteredData.value = tableData.value.filter(item => {
        return (
          (!branchId || item.branchId.includes(branchId)) &&
          (!branchName || item.branchName.includes(branchName)) &&
          (!status || item.status === status)
        );
      });
      if (filteredData.value.length === 0) {
        ElMessage.warning('未找到符合条件的记录');
      } else {
        updatePaginatedData();
        ElMessage.success('搜索成功');
      }
    };
    const onReset = () => {
      searchForm.value.branchId = '';
      searchForm.value.branchName = '';
      searchForm.value.status = '';
      filteredData.value = []; 
      updatePaginatedData(); 
    };
    const handleSelectionChange = (val) => {
      multipleSelection.value = val;
    };
    const handleAdd = () => {
      operateType.value='add';
      visible.value=true;
    };
    const handleBatchDelete = () => {
      if (multipleSelection.value.length === 0) {
        ElMessage.warning('请选择要删除的项');
        return;
      }
      ElMessageBox.confirm(
        '确定要删除选中的项吗？',
        '警告',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }
      ).then(() => {
        const idsToDelete = multipleSelection.value.map(item => item.branchId);
        tableData.value = tableData.value.filter(item => !idsToDelete.includes(item.branchId));
        multipleSelection.value = [];
        updatePaginatedData();
        ElMessage.success('删除成功');
      }).catch(() => {
        ElMessage.info('已取消删除操作');
      });
    };
    const onRefresh = () => {
      ElMessage.success('刷新功能未实现');
    };
    const handleEdit = (index, row) => {
      operateType.value = 'edit'; 
      newData.value = { ...row };
      visible.value = true;
    };
    const handleDelete = (index, row) => {
      ElMessageBox.confirm(
        '确定要删除这个部门吗？',
        '警告',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }
      ).then(() => {
        tableData.value = tableData.value.filter(item => item.branchId !== row.branchId);
        updatePaginatedData();
        ElMessage.success('删除成功');
      }).catch(() => {
        ElMessage.info('已取消删除操作');
      });
    };
    const handleSizeChange = (size) => {
      pageSize.value = size;
      updatePaginatedData();
    };
    const handleCurrentChange = (page) => {
      currentPage.value = page;
      updatePaginatedData();
    };
    const handleSubmit = () => {
      if (!newData.value.branchName || !newData.value.branchId) {
        ElMessage.warning('部门名称和部门编号不能为空');
        return;
      }
      if (operateType.value === 'edit') {
        const index = tableData.value.findIndex(item => item.branchId === newData.value.branchId);
        if (index !== -1) {
          tableData.value[index] = { ...newData.value };
          ElMessage.success('编辑成功');
        } else {
          ElMessage.error('未找到要编辑的记录');
        }
      } else if (operateType.value === 'add') {
        tableData.value.push({ ...newData.value });
        updatePaginatedData();
        ElMessage.success('新增成功');
      }
      visible.value = false;
      newData.value = {
        branchName: '',
        branchId: '',
        expense: 0,
        income: 0,
        status: 0,
      };
    };
    const handleCancel = () => {
      visible.value = false;
      newData.value = {
        branchName: '',
        branchId: '',
        expense: 0,
        income: 0,
        status: 0,
      };
    };
    const paginatedData = ref([]);
    const updatePaginatedData = () => {
      const start = (currentPage.value - 1) * pageSize.value;
      const end = start + pageSize.value;
      paginatedData.value = filteredData.value.length > 0
                              ? filteredData.value.slice(start, end)
                              : tableData.value.slice(start, end);
    };
    updatePaginatedData();
    return {
      selectedHospital,
      selectedHospitalName,
      hospitals,
      searchForm,
      handleChange,
      tableData,
      multipleSelection,
      currentPage,
      pageSize,
      onSearch,
      onReset,
      handleAdd,
      handleBatchDelete,
      handleChange,
      handleCurrentChange,
      handleDelete,
      handleEdit,
      handleSelectionChange,
      handleSizeChange,
      onRefresh,
      visible,
      operateType,
      newData,
      handleSubmit,
      handleCancel,
      enableStatusRecord,
      paginatedData,
      updatePaginatedData,
      filteredData,
    };
  },
}
</script>
<style scoped>
.header{
  width:100%;
  display: flex;
  align-items: center;
  background-color: #a0cfff;
  flex-direction:column ;
  padding:20px ;
  height: auto ;
  font-size: 20px;
}
.select{
  margin-top: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.main{
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: space-around;
  font-size: 18px;
}
.el-select{
  width:100px ;
  margin-right: 10px;
}
.header-wrapper{
  display: flex;
  align-items: center;
  justify-content: space-between;
}
:deep(button:focus) {
  outline: none !important;
}
.search-container,.table-container{
  background-color: white;
  border-radius: 10px;
  width:90%;
  margin-bottom: 10px;
}
.card-header{
  border-bottom: 0;
  background-color: white;
  height:60px;
  margin: 0 auto;
}
::v-deep .el-card__header{
  height:60px !important;
  padding:0px !important;
  border-bottom: 1px solid rgba(0, 0, 0, .125) !important;
}
.icon{
  margin-right: 0.5rem;
}
.clearfix{
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 10px;
}
.buttonGroup{
  margin-left:auto;
}
.selectHos{
  width:150px;
  font-size: 18px;
}
.custom-pagination {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 10px;
}

.custom-pagination .el-pagination__total {
  margin-right: auto; 
}

.custom-pagination .el-pagination__sizes {
  margin-left: auto;
}

.custom-pagination .el-pagination__sizes,
.custom-pagination .el-pagination__prev,
.custom-pagination .el-pagination__pager,
.custom-pagination .el-pagination__next,
.custom-pagination .el-pagination__jumper {
  display: flex;
  align-self: center;
}

.custom-pagination .el-pagination__prev,
.custom-pagination .el-pagination__pager,
.custom-pagination .el-pagination__next,
.custom-pagination .el-pagination__jumper {
  margin: 0 2px; 
}

.custom-pagination .el-pagination__jumper .el-input__inner {
  text-align: right; 
}

.custom-pagination .el-pagination__jumper .el-pagination__jump {
  display: flex;
  align-items: center;
}

.custom-pagination .el-pagination__jumper .el-pagination__jump:before {
  content: '跳转'; 
  margin-right: 5px;
}
.el-button:focus {
      outline: none !important;
}
button:focus{
  outline:0 !important;
}
</style>