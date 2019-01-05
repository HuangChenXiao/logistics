<template>
  <div>
    <n-header title="首页">
    </n-header>
    <div style="height:1.26rem"></div>
    <div class="home">
      <!-- <div class="address">
        当前位置：{{bItem.start_position}}
        <i class="ico ico-ad" onclick="location.reload()"></i>
      </div> -->
      <div class="work" v-if="work_status==0">
        <div class="work-item sb-item" v-if="work_status==0" @click="change_work(1)">
          <i class="ico ico-sb"></i>
          <span>上班</span>
        </div>
        <div class="work-item xb-item" v-if="false" @click="change_work(0)">
          <i class="ico ico-xb"></i>
          <span>下班</span>
        </div>
      </div>
      <div class="info">
        <!-- <div class="item">
          <div class="lbl">当前时间：{{time}}</div>
        </div> -->
        <div class="item">
          <div class="lbl">我的状态：
            <span :class="{'gen':work_status==1}">{{work_status|workMethod}}</span>
          </div>
        </div>
        <div class="item">
          <div class="lbl">我的车辆：
            <span>{{cChePaiHao}}</span>
          </div>
        </div>
        <div class="item">
          <div class="lbl">车辆类型：
            <span>{{cCheLiangLeiBie}}</span>
          </div>
        </div>
      </div>
      <!-- <div class="count-item clearfix">
        <div class="item it_1">
          <div class="title">昨日订单</div>
          <div class="qty">单量：10</div>
        </div>
        <div class="item it_2">
          <div class="title">今日订单</div>
          <div class="qty">单量：20</div>
        </div>
      </div> -->

      <tab :line-width=2 active-color='#f00' v-model="index">
        <tab-item class="vux-center" v-for="(item, index) in list2" @on-item-click="set_tab_item(item)" :key="index">{{item}}</tab-item>
      </tab>
      <div class="task">
        <div class="tab-order" v-if="index==0">
          <div class="title">最新订单</div>
          <div class="item-list" v-if="order_list.length">
            <section v-for="item in order_list">
              <div class="n1">订单号：{{item.cDingDanHao}}</div>
              <div class="n1">工地：{{item.cGongDiMingCheng}}</div>
              <div class="n1">工尾：{{item.cTuWeiMingCheng}}</div>
              <div class="n1">车牌：{{item.cChePaiHao}}</div>
              <div class="n1">派单员：{{item.cGuanLiYuanMingChen}}</div>
              <div class="n1">驾驶员：{{item.cXingMing}}</div>
              <div class="n1">起运时间：{{item.dQiYunShiJian}}</div>
              <div class="n1">状态：{{item.iState|status_filters}}</div>
            </section>
          </div>
          <div class="no-order" v-if="!order_list.length">
            暂无订单信息~
          </div>
        </div>
        <div class="tab-order" v-else>
          <div class="title">最新订单</div>
          <div class="item-list" v-if="wj_order_list.length">
            <section v-for="item in wj_order_list">
              <div class="n1">订单号：{{item.cDingDanHao}}</div>
              <div class="n1">工地：{{item.cGongDiMingCheng}}</div>
              <div class="n1">车牌：{{item.cChePaiHao}}</div>
              <div class="n1">派单员：{{item.cGuanLiYuanMingChen}}</div>
              <div class="n1">驾驶员：{{item.cXingMing}}</div>
              <div class="n1">开始时间：{{item.dKaiShiShiJian}}</div>
              <div class="n1">结束时间：{{item.dKaiShiShiJian}}</div>
              <div class="n1">状态：{{item.iState|status_filters}}</div>
            </section>
          </div>
          <div class="no-order" v-if="!wj_order_list.length">
            暂无订单信息~
          </div>
        </div>
      </div>
    </div>
    <div style="height:1.5rem"></div>
    <!-- <div style="margin:0">
      <group label-width="4.5em" label-margin-right="2em" label-align="right">

        <x-input title="当前位置" placeholder="定位中..." v-model="bItem.start_position"></x-input>
      </group>
    </div> -->
    <c-bottom route_name="home"></c-bottom>
    <!-- 车辆信息 -->
    <x-dialog v-model="showScrollBox" :hide-on-blur="true" class="dialog-demo">
      <c-vehicle :single_drive="true" title="车辆信息" @selectVehicle="select_vehicle" :valueData="vehicle_list" v-model="driver_keyword"></c-vehicle>
    </x-dialog>
    <!-- 班别 -->
    <popup v-model="BanBieShow">
      <!-- group already has a top border, so we need to hide header's bottom border-->
      <popup-header right-text="确定" title="选择班别" :show-bottom-border="false" @on-click-left="BanBieShow = false" @on-click-right="setBanBieShow"></popup-header>
      <group gutter="0">
        <radio v-model="cShangBanBianMa" :options="banbie_list"></radio>
      </group>
    </popup>
  </div>
</template>

<script>
import { Radio, Popup, PopupHeader, Group, XDialog, Tab, TabItem } from 'vux'
import getformattedAddress from '@/map/index.js'
import { setTimeout } from 'timers'
import cVehicle from '@/components/cVehicle'
import workSite from '@/components/workSite'
import { defaultCoreCipherList } from 'constants'
import { DriverCheLiang, ShangBanLeiBie } from '@/api/driver.js'
import { wechatUser } from '@/api/home.js'

import {
  BangDingJiLu,
  GetGongChengCheDingDan,
  GetWaJueJiDingDan
} from '@/api/home.js'

const list = () => ['工程车', '挖掘机']
export default {
  components: {
    Radio,
    Popup,
    PopupHeader,
    Group,
    XDialog,
    cVehicle,
    workSite,
    Tab,
    TabItem
  },
  data() {
    return {
      cShangBanBianMa: '001', //班别值
      BanBieShow: false, //班别
      index: 0,
      list2: list(),
      vehicle_list: [],
      showScrollBox: false,
      task_status: false,
      work_status: 2,
      time: null,
      bItem: {
        start_position: '厦门市'
      },
      order_list: [],
      wj_order_list: [],
      banbie_list: [],
      store_query: {
        longitude: this.$store.getters.longitude,
        latitude: this.$store.getters.latitude,
        startrow: 1,
        pagesize: 10
      },
      driver_keyword: null
    }
  },
  filters: {
    status_filters(val) {
      var valMap = {
        0: '未确认',
        100: '确认',
        110: '作废'
      }
      return valMap[val]
    }
  },
  computed: {
    cChePaiHao() {
      if (this.$store.getters.cChePaiHao) {
        return this.$store.getters.cChePaiHao.cChePaiHao
      }
      return ''
    },
    cCheLiangLeiBie() {
      if (this.$store.getters.cChePaiHao) {
        return this.$store.getters.cChePaiHao.cCheLiangLeiBie
      }
      return ''
    }
  },
  created() {
    let _this = this
    // if (_this.store_query.start_position) {
    //   _this.bItem.start_position = _this.store_query.start_position
    // } else {
    //   _this.get_address()
    // }
    // this.set_time();
    // setTimeout(() => {
    //   _this.$vux.alert.show({
    //     title: '提示',
    //     content: '接收到一个订单，请尽快执行',
    //     onShow() {
    //       _this.task_status = true
    //     }
    //   })
    // }, 1000000)
    this.get_driver() //车辆列表
    this.get_bangding() //查询上班状态
    this.get_order() //订单
    this.get_banbieinfo() //班别
  },
  methods: {
    get_banbieinfo() {
      ShangBanLeiBie().then(res => {
        this.banbie_list = res.data.map(o => {
          var data = {}
          data.key = o.cShangBanBianMa
          data.value = o.cShangBanMingCheng
          return data
        })
      })
    },
    setBanBieShow() {
      console.log(this.cShangBanBianMa)
      this.BanBieShow = false
      this.showScrollBox = true
    },
    //切换时清空数据
    set_tab_item(item) {
      if (this.index == 0) {
        this.get_order()
      } else {
        this.get_wj_order()
      }
    },
    //工程车订单列表
    get_order() {
      GetGongChengCheDingDan({
        page: 1,
        pagesize: 10,
        js_openid: localStorage.getItem('openid')
      }).then(res => {
        this.order_list = res.data
      })
    },
    //挖掘机订单列表
    get_wj_order() {
      GetWaJueJiDingDan({
        page: 1,
        pagesize: 10,
        js_openid: localStorage.getItem('openid')
      }).then(res => {
        this.wj_order_list = res.data
      })
    },
    get_driver() {
      DriverCheLiang({ keyword: this.driver_keyword }).then(res => {
        this.vehicle_list = res.data
      })
    },
    complete_order() {
      this.$vux.confirm.show({
        title: '提示',
        content: '只有到达目的地与管理员确认后才能操作，是否继续？',
        onConfirm() {
          _this.work_status = work_status
        }
      })
    },
    select_vehicle(item) {
      this.showScrollBox = false
      BangDingJiLu({
        iBangDingLeiXing: 1,
        cChePaiHao: item.cChePaiHao,
        cShangBanBianMa: this.cShangBanBianMa
      })
        .then(res => {
          this.work_status = 1
          this.$store.dispatch('setcChePaiHao', JSON.stringify(item)) //车牌号
        })
        .catch(res => {
          this.get_driver() //车辆列表
        })
    },
    get_address() {
      let _this = this
      _this.bItem.start_position = '正在定位。。。'
      _this.$store.dispatch('setisLoading', true)
      getformattedAddress.then(res => {
        _this.bItem.start_position = res.regeocode.formattedAddress
        // console.log(res.regeocode.formattedAddress)
        _this.$store.dispatch('setisLoading', false)
      })
    },
    set_time() {
      var _this = this
      var t = null
      t = setTimeout(time, 1000) //開始运行
      function time() {
        clearTimeout(t) //清除定时器
        var dt = new Date()
        var h = checkTime(dt.getHours()) //获取时
        var m = checkTime(dt.getMinutes()) //获取分
        var s = checkTime(dt.getSeconds()) //获取秒
        _this.time = h + '：' + m + '：' + s
        t = setTimeout(time, 1000) //设定定时器，循环运行
      }
      function checkTime(i) {
        //将0-9的数字前面加上0，例1变为01
        if (i < 10) {
          i = '0' + i
        }
        return i
      }
    },
    //查询上班状态
    get_bangding() {
      wechatUser().then(res => {
        this.work_status = res.data.status
      })
    },
    change_work(work_status) {
      var _this = this
      if (work_status == 0) {
        this.$vux.confirm.show({
          title: '提示',
          content: '下班后将不能接收订单，是否继续？',
          onConfirm() {
            BangDingJiLu({
              iBangDingLeiXing: work_status,
              cChePaiHao: _this.cChePaiHao,
              cShangBanBianMa: _this.cShangBanBianMa
            }).then(res => {
              _this.work_status = work_status
              _this.$store.dispatch('setcChePaiHao', null)
            })
          }
        })
      } else {
        // this.showScrollBox = true;
        this.BanBieShow = true
        this.get_driver() //车辆列表
      }
    }
  },
  watch: {
    driver_keyword(val, oldVal) {
      this.get_driver() //车辆列表
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  font-size: .5rem;
}
.work {
  position: relative;
  text-align: center;
  background: #fff;
  padding: 0.266667rem 0;
  .sb-item {
    border: 3px solid #67c23a;
    color: #67c23a;
  }
  .xb-item {
    border: 3px solid #f56c6c;
    color: #f56c6c;
  }
  .work-item {
    position: relative;
    display: inline-block;
    height: 1.6rem;
    width: 1.6rem;
    border-radius: 1.6rem;
    font-weight: 600;
    .ico {
      position: absolute;
      left: 0.5rem;
      top: 0.1rem;
      width: 0.8rem;
      height: 0.8rem;
    }
    span {
      position: absolute;
      bottom: 0.2rem;
      left: 0.5rem;
    }
    .ico-sb {
      background: url(../../assets/img/sb.png);
      background-size: 100%;
    }
    .ico-xb {
      background: url(../../assets/img/xb.png);
      background-size: 100%;
    }
  }
}
.address {
  position: relative;
  background: #fff;
  font-size: .5rem;
  padding: 0.266667rem;
  border-bottom: 1px solid #efefef;
  padding-right: 1.2rem;
  .ico {
    position: absolute;
    top: 0.266667rem;
    right: 0.266667rem;
    width: 0.6rem;
    height: 0.6rem;
  }
  .ico-ad {
    background: url(../../assets/img/address.png) no-repeat;
    background-size: 100%;
  }
  .ico-edit {
    background: url(../../assets/img/edit.png) no-repeat;
    background-size: 100%;
  }
  .ico:before {
    content: ' ';
    position: absolute;
    left: -0.266667rem;
    display: block;
    height: 0.6rem;
    width: 1px;
    background: #efefef;
  }
}
.info {
  background: #fff;
  padding: 0.266667rem;
  border-top: 1px solid #efefef;
}
.count-item {
  background: #fff;
  border-top: 1px solid #efefef;
  border-bottom: 1px solid #efefef;
  .item {
    float: left;
    width: 50%;
    text-align: center;
    padding: 0.533333rem 0;
    font-size: .5rem;
  }
  .it_1 {
    border-right: 1px solid #efefef;
    box-sizing: border-box;
    .title {
      font-size: 0.426667rem;
      font-weight: 500;
      color: blue;
      margin-bottom: 0.266667rem;
    }
  }
  .it_2 {
    .title {
      font-size: 0.426667rem;
      font-weight: 500;
      color: green;
      margin-bottom: 0.266667rem;
    }
  }
}
.gen {
  color: green;
}
.task {
  background: #fff;
  .title {
    height: 1.066667rem;
    line-height: 1.066667rem;
    padding: 0 0.266667rem;
    border-top: 1px solid #efefef;
    border-bottom: 1px solid #efefef;
  }
  .item-list {
    position: relative;
    padding: 0.266667rem 0;
    padding-right: 2.5rem;
    .item {
      padding: 0.06rem 0.266667rem;
    }
  }
  .sus-btn {
    position: absolute;
    right: 0.266667rem;
    top: 0.6rem;
    color: #f00;
    border: 1px solid #f00;
    text-align: center;
    padding: 0.133333rem 0;
    border-radius: 3px;
    width: 2rem;
  }
}
.task .item-list section {
  border-bottom: 1px solid #ededed;
  padding: 0.266667rem;
}
.no-order {
  text-align: center;
  height: 1.333333rem;
  line-height: 1.333333rem;
}
</style>