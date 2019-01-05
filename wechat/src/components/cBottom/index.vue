<template>
  <div>
    <div class="footer-menu">
      <router-link :to="{name:'home'}" class="item">
        <div class="bg-img od-img" :class="{'od-img-s':route_name=='home'}"></div>
        <span :class="{'sed':route_name=='home'}">首页</span>
      </router-link>
      <!-- <router-link :to="{name:'order',query:{is_order:false,menu_order:true,title:'确认收款',tradetype:1,status:0}}" class="item">
        <div class="bg-img odr-img" :class="{'odr-img-s':route_name=='order'}"></div>
        <span :class="{'sed':route_name=='order'}">订单</span>
      </router-link> -->
      <router-link :to="{name:'user'}" class="item">
        <div class="bg-img us-img" :class="{'us-img-s':route_name=='user'}"></div>
        <span :class="{'sed':route_name=='user'}">我的</span>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      route_name: this.$route.name
    }
  }
}
</script>

<style scoped lang="scss">
.footer-menu {
  position: fixed;
  bottom: 0;
  height: 1.40rem;
  border-top: 1px solid #ededed;
  background: #fff;
  font-size: 0.43rem;
  width: 100%;
  text-align: center;
  .item {
    float: left;
    width: 50%;
    margin-top: 0.12rem;
    .bg-img {
      width: 0.6rem;
      height: 0.6rem;
      margin: 0 auto;
    }
    .od-img {
      background: url(../../assets/img/m_1.png);
      background-size: 100%;
    }
    .od-img-s {
      background: url(../../assets/img/m_1_s.png);
      background-size: 100%;
    }
    .us-img {
      background: url(../../assets/img/u_1.png);
      background-size: 100%;
    }
    .us-img-s {
      background: url(../../assets/img/u_1_s.png);
      background-size: 100%;
    }
    .odr-img {
      background: url(../../assets/img/od.png);
      background-size: 100%;
    }
    .odr-img-s {
      background: url(../../assets/img/sed_od.png);
      background-size: 100%;
    }
  }
  .sed {
    color: #f00;
  }
}
</style>