<template>
  <div>
    <!-- 下拉菜单 -->
    <select v-model="selectedOption" @change="handleChange">
      <option disabled value="">性别</option>
      <option v-for="option in options" :key="option.value" :value="option.value">
        {{ option.text }}
      </option>
    </select>
  </div>
</template>

<script>
export default {
  props: {
    clearData: {
      type: Boolean,
      default: false,
    }
  },
  data() {
    return {
      // 初始选中的选项
      selectedOption: '',
      // 选项列表
      options: [
        { text: '男', value: '男' },
        { text: '女', value: '女' },
      ]
    };
  },
  watch: {
    clearData(newValue) {
      if (newValue) {
        this.Clear();
        this.$emit('reset-clear-data');
      }
    }
  },
  methods: {
    handleChange() {
      this.$emit('updateGender', this.selectedOption);
    },
    Clear() {
      // 清空子组件的数据
      this.selectedOption = '';
    }
  }
}

</script>

<style scoped>
select {
  width: 200px;
  height: 100%;
  border-radius: 10px;
  font-size: medium
}
</style>