<template>
    <div v-if="show" class="modal">
        <div class="modal-content">
            <form @submit.prevent="submitForm">
                <div class="form-group">
                    <label for="ser_id">员工ID</label>
                    <input type="text" v-model="form.ser_id" id="ser_id" required/>
                </div>

                <div class="form-group">
                    <label for="joined_date">加入日期</label>
                    <input type="date" v-model="form.joined_date" id="joined_date" required/>
                </div>

                <div class="form-group">
                    <label for="name">姓名</label>
                    <input type="text" v-model="form.name" id="name" required/>
                </div>

                <div class="form-group">
                    <label for="position">职位</label>
                    <input type="text" v-model="form.position" id="position" required/>
                </div>

                <div class="form-group">
                    <label for="phone_num">电话号码</label>
                    <input type="tel" v-model="form.phone_num" id="phone_num" required/>
                </div>

                <div class="form-group">
                    <label for="salary">薪水</label>
                    <input type="number" v-model="form.salary" id="salary" required/>
                </div>

                <div class="form-group">
                    <label for="hos_name">医院名</label>
                    <input type="text" v-model="form.hos_name" id="hos_name" required/>
                </div>

                <div class="form-group">
                    <label for="dep_name">部门名</label>
                    <input type="text" v-model="form.dep_name" id="dep_name" required/>
                </div>

                <div class="form-group">
                    <label for="birthday">生日</label>
                    <input type="date" v-model="form.birthday" id="birthday" required/>
                </div>

                <div class="form-group">
                    <label for="work_time_id">工作时间ID</label>
                    <input type="text" v-model="form.work_time_id" id="work_time_id" required/>
                </div>

                <div class="form-group">
                    <label for="gender">性别</label>
                    <select v-model="form.gender" id="gender" required>
                        <option value="male">男</option>
                        <option value="female">女</option>
                    </select>
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
            form: {
                ser_id: '',
                joined_date: '',
                name: '',
                position: '',
                phone_num: '',
                salary: '',
                hos_name: '',
                dep_name: '',
                birthday: '',
                work_time_id: '',
                gender: 'male'
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
            this.$emit('submit', this.form);
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
