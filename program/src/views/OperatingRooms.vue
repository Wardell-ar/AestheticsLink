<template>
    <div class="common-layout">
        <el-container>
            <el-header class="header">
                <div>{{ currentDate }} 手术室排班情况</div>
                <div class="select">
                    <el-select v-model="selectedHospitalName" placeholder="选择分院" @change="handleChange" size="large">
                        <el-option v-for="hospital in hospitals" :key="hospital.id" :label="hospital.name"
                            :value="hospital.name">
                        </el-option>
                    </el-select>
                    <el-button type="primary" plain size="large">确认</el-button>
                </div>
            </el-header>
            <el-main v-if="selectedHospital" class="main">
                <h2>{{ selectedHospital.name }} 手术室排班情况</h2>
                <div class="schedule">
                    <div class="timeslots">
                        <p class="border-bottom title">时间表</p>
                        <p class="border-bottom whitep">08:00AM-10:00AM</p>
                        <p class="border-bottom bluep">10:00AM-12:00AM</p>
                        <p class="border-bottom whitep">14:00PM-16:00PM</p>
                        <p class="bluep">16:00PM-18:00PM</p>
                    </div>
                    <div class="operating-rooms">
                        <div v-for="room in operatingRooms" :key="room.roomId" class="room">
                            <p class="roomName">手术室 {{ room.roomId }}</p>
                            <div>
                                <div v-for="(slot, index) in room.slots" :key="index" class="operations">
                                    <div v-if="slot.status === '1'" :class="getSlotClass(slot, index)">
                                        <span>手术信息:{{ slot.info }}</span>
                                        <span>主刀医生:{{ slot.doctor }}</span>
                                        <span>患者:{{ slot.customer }}</span>
                                    </div>
                                    <div v-else :class="getSlotClass(slot, index)">
                                        <span>手术信息:{{ slot.info }}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </el-main>
        </el-container>
    </div>
</template>


<script>
import { getHospitalNameReq, getRoomInfo } from "../HTTP/http"
export default {
    data() {
        return {
            currentDate: new Date().toLocaleDateString(),
            selectedHospital: null,
            selectedHospitalName: '',
            timeslotsEndTime: [
                "10:00",
                "12:00",
                "16:00",
                "18:00"
            ],
            hospitals: [],
            operatingRooms: []
        };
    },
    methods: {
        handleChange() {
            this.selectedHospital = this.hospitals.find(hospital => hospital.name === this.selectedHospitalName);
            getRoomInfo(this.selectedHospital.id, this.setRoomInfo);
            //console.log(this.selectedHospital.id);
        },
        getSlotClass(slot, index) {
            let currentTime = new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
            // let currentTime='10:01';
            return {
                'common': true,
                'available': slot.status === '0',
                'booked': slot.status === '1' && currentTime <= this.timeslotsEndTime[index],
                'ended': slot.status === '1' && currentTime > this.timeslotsEndTime[index],
            };
        },
        setHospitalname(res){
            for(const name of res){
                this.hospitals.push({
                    id: name,
                    name: name
                });
            }
        },
        setRoomInfo(res){
            this.operatingRooms = res;
        }
    },
    mounted(){
        getHospitalNameReq(this.setHospitalname);
    },
}
</script>


<style scoped>
.header {
    width: 100%;
    display: flex;
    align-items: center;
    background-color: #a0cfff;
    flex-direction: column;
    padding: 20px;
    height: auto;
    font-size: 20px;
}

.select {
    margin-top: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.main {
    display: flex;
    align-items: center;
    flex-direction: column;
    justify-content: space-around;
}

.el-select {
    width: 100px;
    margin-right: 10px;
}

.el-button {
    font-size: 18px;
    /* color:#a0cfff ; */
}

.schedule {
    display: flex;
    align-items: center;
    width: 100%;
    margin-top: 10px;
}

.timeslots {
    border-radius: 10px;
    text-align: center;
    height: 535px;
    width: 180px;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 10px 0;
    justify-content: space-between;
    background-color: rgb(255, 250, 235);
    box-shadow: 0px 0px 8px rgba(0, 0, 0, 0.2);
    margin-right:10px;
}

.title {
    font-size: 18px !important;
    color: rgba(25, 46, 236, 0.75);
    position: relative;
}

.timeslots p {
    height: 100px;
    display: flex;
    align-items: center;
    flex-direction: column;
    justify-content: space-around;
    margin-bottom: 3px;
    font-size: 16px;
    width: 100%;
    position: relative;
}

.border-bottom::before {
    content: '';
    position: absolute;
    left: 0;
    bottom: -3px;
    right: 0;
    height: 3px;
    background-color: rgba(76, 199, 230, 0.75);
}

.whitep {
    background-color: #fff;
}

.bluep {
    background-color: #d9ecff;
}

.operating-rooms {
    display: flex;
    align-items: center;
    flex-direction: row;
    justify-content: center;
    width: 100%;
    position: relative;
}

.room {
    box-shadow: 0px 0px 8px rgba(0, 0, 0, 0.2);
    border-radius: 10px;
    text-align: center;
    height: 535px;
    width: 200px;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 10px 0;
    justify-content: space-between;
    margin-right: 3px;
    position: relative;
    background-color: rgb(255, 250, 235);

}

.room:not(:last-child)::after {
    content: '';
    position: absolute;
    top: 10px;
    bottom: 10px;
    right: -3px;
    width: 3px;
    background-color: rgba(25, 46, 236, 0.75);
}

.common {
    height: 100px;
    display: flex;
    align-items: center;
    flex-direction: column;
    justify-content: space-around;
    margin: auto;
    width: 200px;
    font-size: 16px;
    margin-bottom: 3px;
    position: relative;
}

.operations {
    position: relative;
}

.operations:not(:last-child)::before {
    content: '';
    position: absolute;
    left: 0;
    right: 0;
    bottom: -3px;
    height: 3px;
    background-color: rgba(25, 46, 236, 0.75);
}

.available {
    background-color: #d1edc4;
}

.booked {
    background-color: #fcd3d3;
}

.ended {
    background-color: #c6e2ff;
}

.roomName {
    height: 100px;
    margin: auto;
    width: 100%;
    font-size: 18px;
    color: black;
    display: flex;
    align-items: center;
    flex-direction: column;
    justify-content: space-around;
    margin: 0 3px 0 0;
    position: relative;
}

.roomName::before {
    content: '';
    position: absolute;
    left: 0;
    right: 0;
    bottom: -3px;
    height: 3px;
    background-color: rgba(25, 46, 236, 0.75);
}
</style>