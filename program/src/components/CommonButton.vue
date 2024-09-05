<template>
    <el-button round :type="type" :size="size" :class="computedClass" 
    @click="onClick" :style="computedStyle">
        <slot></slot>
    </el-button>
</template>
  
<script>
export default {
    props: {
        type: {
            type: String,
            default: 'primary'
        },
        size: {
            type: String,
            default: 'large'
        },
        color:{
            type:String,
            default:''
        },
        hoverColor:{
            type:String,
            default:''
        }
    },
    computed: {
        computedClass() {
            return this.color ? 'custom-color' : '';
        },
        computedStyle() {
            return this.color? {
            '--custom-color': this.color,
            '--custom-hover-color': this.hoverColor
            } : {};
        }
    },
    methods: {
        onClick() {
            this.$emit('click');
        }
    }
};
</script>
  
<style scoped>
.common-btn {
    padding: 25px 5%;
    font-size: 16px;
}

.custom-color {
    background-color: var(--custom-color);
    color: white;
    &:hover {
        background-color: var(--custom-hover-color);
    }
}
</style>