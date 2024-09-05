<template>
    <div v-if="show" class="modal">
        <div class="modal-content">
            <form @submit.prevent="submitForm">
                <div class="form-group">
                    <label>项目ID:</label>
                    <input type="text" id="proj_id" v-model="formData.proj_id">
                </div>
                <div class="form-group">
                    <label>项目名称:</label>
                    <input type="text" id="name" v-model="formData.name">
                </div>
                <div class="form-group">
                    <label>项目价格:</label>
                    <input type="text" id="price" v-model="formData.price">
                </div>
                <button type="submit" class="submit-button">提交</button>
                <button type="submit" class="submit-button">取消</button>
            </form>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            show: false,
            formData: {
                proj_id: '',
                name: '',
                price: ''
            }
        };
    },
    methods: {
        open() {
            this.show = true;
        },
        close() {
            this.show = false;
        },
        submitForm() {
            this.$emit('submit', this.formData);
            this.close();
        }
    }
};
</script>

<style scoped>
.modal {
    display: flex;
    justify-content: center;
    align-items: center;
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
}

.modal-content {
    background-color: #fff;
    padding: 30px;
    border-radius: 10px;
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
    width: 400px;
    position: relative;
}

.close {
    position: absolute;
    top: 15px;
    right: 15px;
    font-size: 20px;
    color: #aaa;
    cursor: pointer;
    transition: color 0.2s ease;
}

.close:hover {
    color: #000;
}

.form-group {
    margin-bottom: 15px;
}

label {
    display: block;
    font-weight: bold;
    margin-bottom: 5px;
}

input[type="text"] {
    width: 100%;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
    box-sizing: border-box;
    transition: border-color 0.2s ease;
}

input[type="text"]:focus {
    border-color: #007bff;
    outline: none;
}

.submit-button {
    width: 100%;
    padding: 10px;
    background-color: #007bff;
    color: #fff;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 16px;
    transition: background-color 0.2s ease;
    margin-bottom: 10px;
}

.submit-button:hover {
    background-color: #0056b3;
}
</style>
