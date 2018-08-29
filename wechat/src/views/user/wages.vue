<template>
  <div>
    <u-header :title="title" route="user">
    </u-header>
    <div style="height:1.2rem"></div>
    <div>
      <div v-show="is_order==true">
        <tab :line-width=2 v-model="w_qeury.status" active-color="#FF8010" bar-active-color="#FF8010">
          <tab-item class="vux-center" :selected="sed_index === index" v-for="(item, index) in list2" @on-item-click="change_tab_index(index)" :key="index">{{item.value}}</tab-item>
        </tab>
      </div>
      <!-- <div style="height:2rem">{{typeof w_qeury.status}}</div> -->
      <scroller lock-x @on-scroll-bottom="onScrollBottom" ref="scrollerBottom" :scroll-bottom-offst="200">

        <div class="item-list">
          <div class="item" v-for="item in list">
            <div class="n1">订单号：{{item.orderno}}</div>
            <div class="n1">开始地址：{{item.start_position}}</div>
            <div class="n1">结束地址：{{item.end_position}}</div>
            <div class="n1">金额：{{item.amount}}</div>
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
  </div>
</template>

<script>
// import { getOrderMyList } from '@/api/user.js'
import { Tab, TabItem } from 'vux'
const list = () => [
  { key: 0, value: '全部' },
  { key: 1, value: '昨日工资' },
  { key: 2, value: '今日工资' },
]
export default {
  components: {
    Tab,
    TabItem
  },
  data() {
    return {
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
        this.list.push({
          orderno: 'DX95546346546464161',
          start_position: '厦门市湖里区软件园二期',
          end_position: '厦门市集美区软件园三期',
          amount: 99
        })
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

<style scoped lang="scss">
.item-list {
  background: #fff;
  padding: 0 0.266667rem;
  font-size: 0.373333rem;
  .item {
    position: relative;
    border-bottom: 1px solid #ededed;
    padding: 0.266667rem 0;
    .status {
      position: absolute;
      right: 0;
      top: 0.266667rem;
      color: rgb(248, 143, 44);
    }
  }
}
</style>