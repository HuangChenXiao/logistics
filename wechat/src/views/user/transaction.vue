<template>
    <div>
        <u-header title="交易方式">
        </u-header>
        <div style="height:1.2rem"></div>

        <div class="item-list">
            <router-link :to="{name:'transaction-setting',query:{paytype:2}}" class="item">
                <span>支付宝</span>
                <div class="r-txt" :class="{'sus':paytype.isalipay}">{{paytype.isalipay?'已绑定':'未绑定'}}</div>
                <i class="next"></i>
            </router-link>
            <router-link :to="{name:'transaction-setting',query:{paytype:1}}" class="item">
                <span>微信</span>
                <div class="r-txt" :class="{'sus':paytype.iswechat}">{{paytype.iswechat?'已绑定':'未绑定'}}</div>
                <i class="next"></i>
            </router-link>
        </div>
    </div>
</template>


<script>
import { userTradetype } from '@/api/user.js'
export default {
  data(){
    return{
      paytype:{},
      
    }
  },
  created(){
    this.getList()
  },
  methods:{
    getList(){
      userTradetype().then(res=>{
        this.paytype=res.data
      })
    }
  }
}
</script>

<style scoped lang="scss">
.item-list {
  background: #fff;
  .item {
    position: relative;
    font-size: .43rem;
    padding: .43rem 0.266667rem;
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
    .r-txt{
        position: absolute;
        right: .8rem;
        top: 0.383333rem;
        color: rgb(153, 153, 153)
    }
    .sus{
        color: rgb(248, 143, 44)
    }
  }
}
</style>