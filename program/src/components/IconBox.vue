<!-- IconBox.vue -->
<template>
    <div :class="['icon-box',additionalClass]">
        <img :src="iconSrc" alt="img" class="icon-box-font">
        <div class="icon-box-content">
            <h6 class="icon-box-title">{{ title }}</h6>
            <p class="icon-box-description">{{ description }}</p>
        </div>
    </div>
</template>
  
<script>
export default {
  name: 'IconBox',
  props: {
    iconSrc: {
      type: String,
      required: true
    },
    title: {
      type: String,
      required: true
    },
    description: {
      type: String,
      required: true
    },
    additionalClass: {
    type: String,
    required: false,
    default: ''
    }
  }
};
</script>
  
<style lang="scss" scoped>
  .icon-box {
    display: flex;
    align-items: center;
    margin-bottom: 30px;
    width:280px;
    height:150px;
    padding:15px;
    border:1px solid #ececec;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    position:relative;
    background-color:white;
  
    &-font {
      margin-right: 15px;
      width: 50px;
      height: 50px;
      flex-shrink:0;
    }
  
    &-content {
      flex: 1;
    }
  
    &-title {
      font-size: 16px;
      font-weight: bold;
      margin-bottom: 5px;
    }
  
    &-description {
      font-size: 16px;
      line-height:20px;
      color: #555;
    }
  }
</style>