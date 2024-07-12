<!-- frontPage.vue -->
<template>
    <Advertise/>
    <Advantages/>
</template>
<script>
//import { useRouter } from 'vue-router';
import Advertise from '@/components/Advertise.vue';
import Advantages from'@/components/Advantages.vue';
export default{
    name:'frontPage',
    components:{
        Advertise,
        Advantages
    }
}
//const router = useRouter()
</script>
