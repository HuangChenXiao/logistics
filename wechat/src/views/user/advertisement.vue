<template>
  <div>
    <u-header title="我的广告">
    </u-header>
    <div style="height:1.2rem"></div>
    <tab :line-width=2 v-model="w_qeury.status" active-color="#FF8010" bar-active-color="#FF8010">
      <tab-item class="vux-center" :selected="w_qeury.status === index" v-for="(item, index) in status_list" @on-item-click="change_tab_index(index)" :key="index">{{item.value}}</tab-item>
    </tab>
    <scroller lock-x @on-scroll-bottom="onScrollBottom" ref="scrollerBottom" :scroll-bottom-offst="200">
      <div class="item-list">
        <div class="item" v-for="(item,index) in list">
          <!-- <div class="type">卖出 GS</div> -->
          <div class="t1">编号：{{item.preorderno}}</div>
          <div class="t1">发布时间：{{item.addtime}}</div>
          <div class="t1">浮动单价：{{item.price}}CNY</div>
          <div class="t1">交易金额：{{item.amount}}CNY</div>
          <!-- <div class="reset-btn" @click="closeAdvert(item,index)" v-if="item.status==0">关闭广告</div> -->
        </div>
        <load-more tip="加载中" v-if="!onFetching"></load-more>
        <div class="no-more-data" v-if="noData">
          <span> 我是有底线的~</span>
        </div>
        <div style="height:2.5rem" v-if="list.length"></div>
      </div>
    </scroller>
  </div>
</template>

<script>
import { getAdMyList, adUpdate } from '@/api/user.js'
import { Tab, TabItem } from 'vux'
const status_list = () => [
  { key: 0, value: '发布中' },
  { key: 1, value: '未付款' },
  { key: 2, value: '冻结中' },
  { key: 3, value: '交易完成' },
  { key: 4, value: '广告关闭' }
]
export default {
  components: {
    Tab,
    TabItem
  },
  data() {
    return {
      w_qeury: {
        page: 1,
        pagesize: 10,
        status: 0
      },
      list: [],
      onFetching: false,
      noData: false,
      status_list: status_list()
    }
  },
  created() {
    this.onScrollBottom()
  },
  methods: {
    init_query() {
      this.list = []
      this.onFetching = false
      this.w_qeury.page = 1
      this.$nextTick(() => {
        this.$refs.scrollerBottom.reset({ top: 0 })
      })
    },
    change_tab_index(index) {
      this.w_qeury.status = index
      this.init_query()
      this.onScrollBottom()
    },
    onScrollBottom() {
      if (this.onFetching) {
        // do nothing
      } else {
        this.onFetching = true
        getAdMyList(this.w_qeury).then(res => {
          if (res.data.length) {
            this.list = this.list.concat(res.data)
            this.$nextTick(() => {
              this.$refs.scrollerBottom.reset()
            })
            this.w_qeury.page++
            if (res.data.length < 10) {
              this.onFetching = true
              this.noData = true
            } else {
              this.onFetching = false
            }
          } else {
            this.onFetching = true
            this.noData = true
          }
        })
      }
    },
    closeAdvert(item,index) {
      var _this = this
      this.$vux.confirm.show({
        title: '系统提示',
        content: '是否关闭广告',
        onConfirm() {
          adUpdate({ preid: item.preid, amount: item.amount })
            .then(res => {
              _this.$vux.toast.show({
                text: '关闭成功'
              })
              _this.list.splice(index,1)
            })
            .catch(res => {
              _this.$vux.alert.show({
                title: '系统提示',
                content: res.message
              })
            })
        }
      })
    }
  }
}
</script>

<style scoped lang="scss">
.item-list {
  background: #fff;
  padding: 0 0.266667rem;
  .item {
    position: relative;
    border-bottom: 1px solid #ededed;
    font-size: .43rem;
    padding: .43rem 0;
    .type {
      background: rgb(240, 123, 20);
      padding: 0.08rem 0.213333rem;
      display: inline-block;
      color: #fff;
      border-radius: 0.133333rem;
      margin-bottom: 0.133333rem;
    }
    .reset-btn {
      position: absolute;
      right: 0.266667rem;
      bottom: 0.4rem;
      padding: 0.08rem 0.133333rem;
      border-radius: 0.106667rem;
      border: 1px solid #ededed;
      color: #999;
    }
  }
}
</style>