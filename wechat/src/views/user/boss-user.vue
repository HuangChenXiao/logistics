<template>
  <div>
    <n-header title="我的">
    </n-header>
    <div style="height:1.2rem"></div>
    <div class="info">
      <div class="img">
        <img v-lazy="$store.getters.user_info.headimgurl" alt="">
      </div>
      <div class="content">
        <div class="name">{{this.$store.getters.user_info.cXingMing}}</div>
        <!-- <div class="count">
          交易次数：{{user_info.tradeqty}}
        </div> -->
      </div>
    </div>
    <div class="item-list">
      <router-link :to="{name:'admin-head-tail'}" class="item">
        <span>订单统计数</span>
        <i class="next"></i>
      </router-link>
      <router-link :to="{name:'gong-di'}" class="item">
        <span>工地信息</span>
        <i class="next"></i>
      </router-link>
    </div>
    <!-- <div style="height:1.5rem"></div> -->
    <!-- <destination-bottom route_name="admin-user"></destination-bottom> -->
  </div>
</template>

<script>
import destinationBottom from '@/components/destinationBottom'
export default {
  components: {
    destinationBottom
  }
}
</script>

<style scoped lang="scss">
.info {
  position: relative;
  background: #f00;
  padding: 0.533333rem 1.04rem;
  font-size: .5rem;
  color: #fff;
  .name {
    height: 0.586667rem;
  }
  .img {
    height: 1.466667rem;
    width: 1.466667rem;
    border-radius: 1.466667rem;
    overflow: hidden;
  }
  .content {
    position: absolute;
    left: 2.8rem;
    top: 0.933333rem;
  }
}
.item-list {
  background: #fff;
  .item {
    position: relative;
    font-size: .5rem;
    padding: .5rem 0.266667rem;
    border-bottom: 1px solid #ededed;
    .next {
      position: absolute;
      right: 0.266667rem;
      top: 0.55rem;
      display: block;
      background: url(../../assets/img/next.png);
      background-size: 100%;
      width: 0.146667rem;
      height: 0.266667rem;
    }
  }
}
.btn-work {
  position: absolute;
  right: 0.4rem;
  top: 0.6rem;
  height: 1.333333rem;
  width: 1.333333rem;
  line-height: 1.333333rem;
  border-radius: 1.333333rem;
  text-align: center;
}
.btn-work.gre {
  background: #00aa00;
}
.btn-work.red {
  background: #f00;
}
</style>