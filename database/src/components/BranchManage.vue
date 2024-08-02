<template>
    <div class="header">
         <div>分院管理</div>
    </div>
    <div class="branches">
        <div class="branch">
            <div class="content">
                <div class="icon-container">
                    <i class="fas fa-home icon"></i>
                </div>
                <span>上海分院</span>
                
            </div>

        </div>

    </div>
</template>

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
.branch{
    position: relative;
    height: 250px;
    text-align: center;
    border-radius: 10px;
    background: var(--bs-white);
    padding: 1.5rem !important;
    z-index:1;
}
.content{
    position: relative;
    z-index: 3;
}
.icon-container{
    margin-bottom: 1.5rem !important;
}
.icon{
    color: var(--blue--light);
    transition: 0.5s;
    font-size: 4em;
    font-weight: 900;
}
span{
    transition: 0.5s;
    margin-bottom: 1rem !important;
    z-index:4;
}
.content::after{
    content: "";
    position: absolute;
    width: 100%;
    height: 0;
    bottom: 0;
    left: 0;
    border-radius: 10px;
    background: var(--blue--light);
    transition: 0.5s;
    z-index: 2;
}
.content:hover::after{
    height: 100%;
    z-index: 0;
}
</style>