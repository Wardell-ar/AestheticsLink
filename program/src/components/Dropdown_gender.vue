<template>
  <div>
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
      selectedOption: "",
      // 选项列表
      options: [
        { text: "男", value: "男" },
        { text: "女", value: "女" },
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
      this.selectedOption = "";
    }
  }
}

</script>

<style scoped>
select {
  width: 200px;
  height: 40px;
  padding: 5px 10px;
  border-radius: 10px;
  font-size: 16px;
  font-weight: 500;
  color: #333;
  background-color: #f9f9f9;
  border: 1px solid #ccc;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
}

select:hover {
  border-color: #007bff;
  box-shadow: 0 2px 8px rgba(0, 123, 255, 0.2);
}

select:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 2px 8px rgba(0, 123, 255, 0.4);
}

option {
  font-weight: 400;
  color: #333;
  background-color: #fff;
}
</style>