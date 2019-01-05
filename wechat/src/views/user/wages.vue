<template>
  <div>
    <u-header :title="title" route="user">
      <!-- <div class="s-where" @click="showWhere=true">
        查询
      </div> -->
    </u-header>
    <div style="height:1.2rem"></div>
    <div>
      <!-- <div v-show="is_order==true">
        <tab :line-width=2 v-model="w_qeury.status" active-color="#FF8010" bar-active-color="#FF8010">
          <tab-item class="vux-center" :selected="sed_index === index" v-for="(item, index) in list2" @on-item-click="change_tab_index(index)" :key="index">{{item.value}}</tab-item>
        </tab>
      </div> -->
      <!-- <div style="height:2rem">{{typeof w_qeury.status}}</div> -->
      <scroller lock-x @on-scroll-bottom="onScrollBottom" ref="scrollerBottom" :scroll-bottom-offst="200">

        <div class="item-list">
          <div class="item" v-for="item in list">
            <section>
              <div class="n1">订单号：{{item.orderno}}</div>
              <div class="n1">工地：{{item.work}}</div>
              <div class="n1">金额：{{item.amount}}</div>
              <div class="n1">开始地址：{{item.start_position}}</div>
              <div class="n1">结束地址：{{item.end_position}}</div>
              <div class="n1">状态：{{item.status}}</div>
            </section>
            <div class="op-btn">
              <div class="confirm-btn">
                确认工资
              </div>
            </div>
          </div>

          <load-more tip="加载中" v-if="!onFetching"></load-more>
          <div class="no-more-data" v-if="noData">
            <span> 我是有底线的~
            </span>
          </div>
          <!-- <div style="height:2.5rem" v-if="list.length"></div> -->
        </div>

      </scroller>
    </div>
    <c-bottom v-if="menu_order" route_name="order"></c-bottom>
    <div class="mask" v-show="showWhere">
      <div class="mask-content">
        <div class="title">查询条件</div>
        <group label-width="4.5em" label-margin-right="2em" label-align="right" class="group-content">
          <x-input title="订单号" placeholder="请输入订单号"></x-input>
          <selector title="工地" :options="['工艺技术', '其他']" v-model="value2"></selector>
          <selector title="开始路线" :options="['高新技术园', '五缘湾']" v-model="value2"></selector>
          <selector title="结束路线" :options="['莲花新城', '中美新天地']" v-model="value2"></selector>
          <datetime title="开始日期" v-model="time1" value-text-align="left"></datetime>
          <datetime title="结束日期" v-model="time2" value-text-align="left"></datetime>
          <div class="wk-btn">
            <div class="reset-btn" @click="showWhere=false">取消</div>
            <div class="ok-btn">确定</div>
          </div>
        </group>
      </div>
    </div>
  </div>
</template>

<script>
// import { getOrderMyList } from '@/api/user.js'
import { Tab, TabItem, XDialog, Datetime } from 'vux'
const list = () => [
  { key: 0, value: '全部' },
  { key: 1, value: '昨日订单' },
  { key: 2, value: '今日订单' },
  { key: 3, value: '已取消' }
]
export default {
  components: {
    Tab,
    TabItem,
    XDialog,
    Datetime
  },
  data() {
    return {
      showWhere: false,
      title: this.$route.query.title,
      index: 0,
      sed_index: 0,
      list2: list(),
      is_order: this.$route.query.is_order,
      menu_order: this.$route.query.menu_order,
      w_qeury: {
        page: 1,
        pagesize: 10,
        status: this.$route.query.status,
        tradetype: this.$route.query.tradetype
      },
      list: [],
      onFetching: false,
      noData: false
    }
  },
  created() {
    this.onScrollBottom()
  },
  mounted() {},
  methods: {
    init_query() {
      this.list = []
      this.onFetching = false
      this.w_qeury.page = 1
      this.$nextTick(() => {
        this.$refs.scrollerBottom.reset({ top: 0 })
      })
    },
    orderBuyType(type) {
      this.w_qeury.tradetype = type
      this.init_query()
      this.onScrollBottom()
    },
    change_tab_index(index) {
      this.w_qeury.status = index
      this.init_query()
      this.onScrollBottom()
    },
    goPayment(item) {
      if (item.status == 0) {
        this.$router.push({
          name: 'purchase-details_payment',
          query: {
            orderno: item.orderno,
            tradetype: this.w_qeury.tradetype
          }
        })
      }
    },
    onScrollBottom() {
      if (this.onFetching) {
        // do nothing
      } else {
        this.onFetching = true
        this.list = []
        this.list = [
          {
            orderno: 'DX95546346546464161',
            work: '厦门市',
            start_position: '厦门市湖里区软件园二期',
            end_position: '厦门市集美区软件园三期',
            amount: '99',
            status: '未确认'
          },
          {
            orderno: 'DX95546346546464161',
            work: '厦门市',
            start_position: '厦门市湖里区软件园二期',
            end_position: '厦门市集美区软件园三期',
            amount: '99',
            status: '未确认'
          },
          {
            orderno: 'DX95546346546464161',
            work: '厦门市',
            start_position: '厦门市湖里区软件园二期',
            end_position: '厦门市集美区软件园三期',
            amount: '99',
            status: '未确认'
          }
        ]
        // getOrderMyList(this.w_qeury).then(res => {
        //   if (res.data.length) {
        //     this.list = this.list.concat(res.data)
        //     // this.$nextTick(() => {
        //     //   this.$refs.scrollerBottom.reset()
        //     // })
        //     this.w_qeury.page++
        //     if (res.data.length < 10) {
        //       this.onFetching = true
        //       this.noData = true
        //     } else {
        //       this.onFetching = false
        //     }
        //   } else {
        //     this.onFetching = true
        //     this.noData = true
        //   }
        // })
      }
    }
  }
}
</script>
<style>
.group-content .weui-cells {
  font-size: .5rem;
}
</style>
<style scoped lang="scss">
.item-list {
  background: #fff;
  font-size: .5rem;
  .item {
    position: relative;
    border-bottom: 1px solid #ededed;
    section {
      padding: 0.266667rem;
    }
    .status {
      position: absolute;
      right: 0;
      top: 0.266667rem;
      color: rgb(248, 143, 44);
    }
    .op-btn {
      border-top: 1px solid #ededed;
      padding: 0.266667rem;
      text-align: right;
      .confirm-btn {
        display: inline-block;
        color: #fff;
        background: #f00;
        padding: 0.08rem 0.133333rem;
        border-radius: 3px;
      }
    }
  }
}
.s-where {
  color: #f00;
}
.mask-content {
  padding-bottom: 0.333333rem;
  font-size: .5rem;
  .title {
    text-align: center;
    height: 1.226667rem;
    line-height: 1.226667rem;
    border-bottom: 1px solid #ededed;
    margin-bottom: -1px;
    font-size: 0.46rem;
  }
  .wk-btn {
    text-align: center;
    border-top: 1px solid #ededed;
    padding-top: 0.266667rem;
    margin-bottom: 0.266667rem;
    .reset-btn {
      color: #f00;
      border: 1px solid #f00;
      display: inline-block;
      padding: 0.053333rem 0.2rem;
      border-radius: 3px;
      margin: 0 0.266667rem;
    }
    .ok-btn {
      background: #f00;
      color: #fff;
      display: inline-block;
      padding: 0.053333rem 0.2rem;
      border-radius: 3px;
      margin: 0 0.266667rem;
      font-size: 0.426667rem;
    }
  }
}
</style>