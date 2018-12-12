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
      <router-link :to="{name:'order',query:{title:'我的订单',menu_route:'admin-user'}}" class="item">
        <span>我的订单</span>
        <i class="next"></i>
      </router-link>
      <router-link :to="{name:'admin-head-tail'}" class="item">
        <span>订单统计数</span>
        <i class="next"></i>
      </router-link>
      <router-link :to="{name:'tu-wei'}" class="item">
        <span>土尾列表</span>
        <i class="next"></i>
      </router-link>
    </div>
    <div class="item-list" v-if="work_status==1" @click="change_work">
      <div class="item">
        <span>下班</span>
      </div>
    </div>
    <div style="height:1.5rem"></div>
    <admin-bottom route_name="admin-user"></admin-bottom>
  </div>
</template>

<script>
import adminBottom from '@/components/adminBottom'
import { BangDingJiLu } from '@/api/home.js'
export default {
  components: {
    adminBottom
  },
  data() {
    return {
      work_status: 0
    }
  },
  created() {
    this.work_status = this.$store.getters.user_info.status
  },
  methods: {
    change_work() {
      var _this = this
      this.$vux.confirm.show({
        title: '提示',
        content: '下班后将不能发布订单，是否继续？',
        onConfirm() {
          BangDingJiLu({
            iBangDingLeiXing: 0,
            cShangBanBianMa: _this.$store.getters.user_info.role_code
          }).then(res => {
            _this.work_status = 0
          })
        }
      })
    }
  }
}
</script>

<style scoped lang="scss">
.info {
  position: relative;
  background: #f00;
  padding: 0.533333rem 1.04rem;
  font-size: 0.373333rem;
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
  margin-top: 0.266667rem;
  background: #fff;
  .item {
    position: relative;
    font-size: 0.373333rem;
    padding: 0.373333rem 0.266667rem;
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
  .c9 {
    color: #999;
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