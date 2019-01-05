<template>
  <div>
    <u-header :title="title" :route="menu_route">
    </u-header>
    <div style="height:1.2rem"></div>
    <div>
      <scroller lock-x @on-scroll-bottom="onScrollBottom" ref="scrollerBottom" :scroll-bottom-offst="200">
        <div class="item-list">
          <div class="item">
            <section v-for="item in 10">
              <div class="n1"></div>
              <div class="n1">路线：厦门市北海大道口-厦门市第一码头</div>
            </section>
          </div>

          <load-more tip="加载中" v-if="!onFetching"></load-more>
          <div class="no-more-data" v-if="noData">
            <span> 我是有底线的~
            </span>
          </div>
        </div>

      </scroller>
    </div>
    <c-bottom v-if="menu_order" route_name="order"></c-bottom>
  </div>
</template>

<script>
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
  },
  data() {
    return {
      showScrollBox: false,
      showWorkRoute: false,
      showWhere: false,
      title: this.$route.query.title,
      index: 0,
      sed_index: 0,
      list2: list(),
      is_order: this.$route.query.is_order,
      menu_order: this.$route.query.menu_order,
      menu_route: this.$route.query.menu_route,
      w_qeury: {
        page: 1,
        pagesize: 10,
        status: this.$route.query.status,
        tradetype: this.$route.query.tradetype
      },
      list: [],
      onFetching: false,
      noData: false,
      vehicle_list: [
        { id: 1,name:'黄臣晓', code: '闽A12345', color: '红色' },
        { id: 2,name:'黄臣晓', code: '闽A12345', color: '白色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2,name:'黄臣晓', code: '闽A4564', color: '黑色' }
      ],
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
            work: '厦门市软件园二期何厝路口东路',
            tail_work:'厦门市杏林路杏林湾东二路',
            license_plate:'闽A123',
            name:'黄臣晓',
            addtime: '2018-9-29 10:52:24',
            status: '未确认'
          },
        ]
      }
    }
  }
}
</script>
<style>
.group-content .weui-cells {
  font-size: .43rem;
}
</style>
<style scoped lang="scss">
.item-list {
  background: #fff;
  font-size: .43rem;
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
  font-size: .43rem;
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
.bor-t {
  border-top: 1px solid #d9d9d9;
  -webkit-transform: scaleY(0.5);
  transform: scaleY(0.5);
}
.cus-item {
  position: relative;
  padding: 10px 15px;
  padding-left: 2.8rem;
  .lbl {
    position: absolute;
    width: 4.5em;
    text-align: right;
    margin-right: 2em;
    left: 15px;
  }
  .set-btn {
    border: 1px solid #f00;
    color: #f00;
    text-align: center;
    display: inline-block;
    border-radius: 3px;
    padding: 2px 5px;
  }
}
</style>