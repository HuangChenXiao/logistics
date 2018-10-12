<template>
  <div>
    <n-header title="首页">
    </n-header>
    <div style="height:1.26rem"></div>
    <div class="home">
      <div class="address">
        我的工地：{{cGongDiMingCheng}}
        <i class="ico ico-edit" @click="showWorkSite=true"></i>
      </div>
      <div class="address">
        当前位置：{{bItem.start_position}}
        <i class="ico ico-ad" onclick="location.reload()"></i>
      </div>

      <div class="work">
        <div class="work-item sb-item" v-if="work_status==0">
          <i class="ico ico-sb"></i>
          <span>上班</span>
        </div>
        <div class="work-item xb-item" v-if="work_status==1">
          <i class="ico ico-xb"></i>
          <span>下班</span>
        </div>
      </div>
      <div>
        <tab :line-width=2 active-color='#f00' v-model="index">
          <tab-item class="vux-center" :selected="demo2 === item" v-for="(item, index) in list2" @click="demo2 = item" :key="index">{{item}}</tab-item>
        </tab>
        <swiper v-model="index" height="200px" :show-dots="false">
          <swiper-item v-for="(item, index) in list2" :key="index">
            <div class="tab-swiper vux-center" v-if="index==0">
              <group label-width="4.5em" label-margin-right="2em" label-align="right" class="group-content">
                <div class="group-item">
                  <div class="lbl">合作单位</div>
                  <div class="info">
                    <div class="btn-select" @click="showCooperation=true">
                      选择
                    </div>
                    <span>{{bItem.cXZDWMingCheng}}</span>
                    <div class="ico-clear" v-if="bItem.cXZDWMingCheng" @click="chepai_change()"></div>
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">车辆</div>
                  <div class="info">
                    <div class="btn-select" @click="showScrollBox=true">
                      选择
                    </div>
                    <span>{{bItem.cChePaiHao}}</span>
                    <div class="ico-clear" v-if="bItem.cChePaiHao" @click="cheliang_change()"></div>
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">路线</div>
                  <div class="info">
                    <div class="btn-select" @click="showWorkRoute=true">
                      选择
                    </div>
                    <span>{{bItem.cXianLuBianMa}}</span>
                    <div class="ico-clear" v-if="bItem.cXianLuBianMa" @click="bItem.cXianLuBianMa=null"></div>
                  </div>
                </div>
                <!-- <selector title="合作单位"  v-model="value2"></selector>
                                <selector title="车辆" :options="['闽G123', '其他']" v-model="value2"></selector>
                                <selector title="路线" :options="['高新技术园-软件园', '五缘湾-火车站']" v-model="value2"></selector> -->
                <div class="wk-btn">
                  <!-- <div class="ok-btn">发布订单</div> -->
                  <x-button class="ok-btn" :class="{'btn-success':validate_order}" type="primary" :disabled="!validate_order" @click.native="submit_order"> 发布订单</x-button>
                </div>
              </group>
            </div>
            <div class="tab-swiper vux-center" v-if="index==1">
              <group label-width="4.5em" label-margin-right="2em" label-align="right" class="group-content">
                <div class="group-item">
                  <div class="lbl">合作单位</div>
                  <div class="info">
                    <div class="btn-select" @click="showCooperation=true">
                      选择
                    </div>
                    <span>{{bItem.cXZDWMingCheng}}</span>
                    <div class="ico-clear" v-if="bItem.cXZDWMingCheng" @click="chepai_change()"></div>
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">车辆</div>
                  <div class="info">
                    <div class="btn-select" @click="showScrollBox=true">
                      选择
                    </div>
                    <span>{{bItem.cChePaiHao}}</span>
                    <div class="ico-clear" v-if="bItem.cChePaiHao" @click="cheliang_change()"></div>
                  </div>
                </div>
                <div class="wk-btn">
                  <!-- <div class="ok-btn">发布订单</div> -->
                  <x-button class="ok-btn" :class="{'btn-success':wj_validate_order}" type="primary" :disabled="!wj_validate_order" @click.native="wj_submit_order"> 发布订单</x-button>
                </div>
              </group>
            </div>
          </swiper-item>
        </swiper>
      </div>
      <div class="task">
        <div class="tab-order" v-if="index==0">
          <div class="title">最新订单</div>
          <div class="item-list">
            <section v-for="item in order_list">
              <div class="n1">订单号：AF546465654512</div>
              <div class="n1">工地：{{item.cGongDiMingCheng}}</div>
              <div class="n1">工尾：{{item.cTuWeiMingCheng}}</div>
              <div class="n1">车牌：{{item.cChePaiHao}}</div>
              <div class="n1">驾驶员：{{item.cXingMing}}</div>
              <div class="n1">起运时间：{{item.dQiYunShiJian}}</div>
              <div class="n1">状态：{{item.iState==0?'未确认':'已确认'}}</div>
            </section>
          </div>
        </div>
        <div class="tab-order" v-else>
          <div class="title">最新订单</div>
          <div class="item-list">
            <section v-for="item in wj_order_list">
              <div class="n1">订单号：AF546465654512</div>
              <div class="n1">工地：{{item.cGongDiMingCheng}}</div>
              <div class="n1">车牌：{{item.cChePaiHao}}</div>
              <div class="n1">驾驶员：{{item.cXingMing}}</div>
              <div class="n1">订单时间：{{item.dDanJuRiQi}}</div>
              <div class="n1">状态：{{item.iState==0?'未确认':'已确认'}}</div>
            </section>
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
    <admin-bottom route_name="admin-index"></admin-bottom>
    <!-- 合作单位 -->
    <x-dialog v-model="showCooperation" :hide-on-blur="true" class="dialog-demo">
      <cooperation @selectVehicle="select_cooperation" :valueData="cooperation_list" v-model="cooperation_keyword"></cooperation>
    </x-dialog>
    <!-- 车辆信息 -->
    <x-dialog v-model="showScrollBox" :hide-on-blur="true" class="dialog-demo">
      <c-vehicle @selectVehicle="select_vehicle" :single_drive="true" :valueData="vehicle_list" v-model="driver_keyword"></c-vehicle>
    </x-dialog>
    <!-- 线路 -->
    <x-dialog v-model="showWorkRoute" :hide-on-blur="true" class="dialog-demo">
      <work-route @selectVehicle="select_wordRoute" :valueData="route_list" v-model="route_keyword"></work-route>
    </x-dialog>
    <!-- 工地信息 -->
    <x-dialog v-model="showWorkSite" class="dialog-demo">
      <work-site @selectVehicle="selectWork" :valueData="work_list" v-model="work_keyword"></work-site>
    </x-dialog>
  </div>
</template>

<script>
import {
  Group,
  XDialog,
  Tab,
  TabItem,
  Swiper,
  SwiperItem,
  DatetimeRange
} from 'vux'
import getformattedAddress from '@/map/index.js'
import { setTimeout } from 'timers'
import cVehicle from '@/components/cVehicle'
import cooperation from '@/components/cooperation'
import workSite from '@/components/workSite'
import workRoute from '@/components/workRoute'
import adminBottom from '@/components/adminBottom'
import { defaultCoreCipherList } from 'constants'
const list = () => ['工程车', '挖掘机']

import {
  GongDiInfo,
  CheLiangInfo,
  XieZuoDanWeiInfo,
  XianLuInfo,
  GongChengCheDingDan,
  GetGongChengCheDingDan,
  WaJueJiDingDan,
  GetWaJueJiDingDan
} from '@/api/home.js'
export default {
  components: {
    Group,
    XDialog,
    cVehicle,
    workSite,
    adminBottom,
    Tab,
    TabItem,
    Swiper,
    SwiperItem,
    DatetimeRange,
    cooperation,
    workRoute
  },
  data() {
    return {
      work_status: 0,
      isavailable: true,
      showCooperation: false,
      showScrollBox: false,
      showWorkRoute: false,
      list2: list(),
      index: 0,
      demo2: '美食',
      showWorkSite: false,
      bItem: {
        start_position: '厦门市',
        cGongDiBianMa: null, //工地编码
        cChePaiHao: null, //车牌号
        openid: null, //驾驶员编码
        cXianLuBianMa: null, //线路编码
        cXZDWBianMa: null, //协作单位编码
        cXZDWMingCheng: null, //协作单位名称
        cGuanLiYuanBianMa: localStorage.getItem('openid') //现场管理员编码
      },
      store_query: {
        longitude: this.$store.getters.longitude,
        latitude: this.$store.getters.latitude,
        startrow: 1,
        pagesize: 10
      },
      vehicle_list: [],
      cooperation_list: [],
      route_list: [],
      work_list: [],
      order_list: [],
      wj_order_list: [],
      work_keyword: null,
      driver_keyword: null,
      driver_cXZDWBianMa: null,
      cooperation_keyword: null,
      route_keyword: null
    }
  },
  computed: {
    cGongDiMingCheng() {
      if (this.$store.getters.gongdi_info.cGongDiMingCheng) {
        return this.$store.getters.gongdi_info.cGongDiMingCheng
      } else {
        return '请选择'
      }
    },
    validate_order() {
      if (
        this.bItem.cChePaiHao &&
        this.bItem.openid &&
        this.bItem.cXianLuBianMa &&
        this.bItem.cGuanLiYuanBianMa &&
        this.isavailable
      ) {
        return true
      }
      return false
    },
    wj_validate_order() {
      if (
        this.bItem.cChePaiHao &&
        this.bItem.openid &&
        this.bItem.cGuanLiYuanBianMa &&
        this.isavailable
      ) {
        return true
      }
      return false
    }
  },
  created() {
    let _this = this
    if (!this.$store.getters.gongdi_info.cGongDiMingCheng) {
      //未选择工地时弹出
      this.showWorkSite = true
    }
    // if (_this.store_query.start_position) {
    //   _this.bItem.start_position = _this.store_query.start_position
    // } else {
    //   _this.get_address()
    // }
    this.set_time()
    // setTimeout(() => {
    //   _this.$vux.alert.show({
    //     title: '提示',
    //     content: '接收到一个订单，请尽快执行',
    //     onShow() {
    //       _this.task_status = true
    //     }
    //   })
    // }, 1000000)
    this.get_worksite() //工地信息
    this.get_driver() //车辆信息
    this.get_cooperation() //合作单位
    this.get_route() //线路
    this.get_order() //工程车订单列表
    this.get_wj_order() //挖掘机订单
  },
  methods: {
    //清空车辆
    cheliang_change() {
      this.bItem.cChePaiHao = null
      this.bItem.openid = null
    },
    //清空合作单位
    chepai_change() {
      this.bItem.cXZDWMingCheng = null
      this.driver_cXZDWBianMa = null
      this.bItem.cChePaiHao = null
      this.bItem.openid = null
      this.get_driver() //车辆信息
    },
    //工程车订单列表
    get_order() {
      GetGongChengCheDingDan().then(res => {
        this.order_list = res.data
      })
    },
    //挖掘机订单列表
    get_wj_order() {
      GetWaJueJiDingDan().then(res => {
        this.wj_order_list = res.data
      })
    },
    //工地列表
    get_worksite() {
      GongDiInfo({ keyword: this.work_keyword }).then(res => {
        this.work_list = res.data
      })
    },
    //工地名称
    selectWork(val) {
      this.$store.dispatch('SGongDiMingCheng', val).then(res => {
        localStorage.setItem('cGongDiMingCheng', JSON.stringify(val))
      })
      this.showWorkSite = false
    },
    //车辆列表
    get_driver() {
      CheLiangInfo({
        keyword: this.driver_keyword,
        cXZDWBianMa: this.driver_cXZDWBianMa
      }).then(res => {
        this.vehicle_list = res.data
      })
    },
    //车辆名称
    select_vehicle(item) {
      this.showScrollBox = false
      this.bItem.cChePaiHao = item.cChePaiHao
      this.bItem.openid = item.openid
    },
    //合作单位
    get_cooperation() {
      XieZuoDanWeiInfo({ keyword: this.cooperation_keyword }).then(res => {
        this.cooperation_list = res.data
      })
    },
    //合作单位名称
    select_cooperation(item) {
      this.showCooperation = false
      this.bItem.cXZDWBianMa = item.cXZDWBianMa
      this.bItem.cXZDWMingCheng = item.cXZDWMingCheng
      //选择合作单位后车辆条件加上 driver_cXZDWBianMa 清空驾驶员
      this.driver_cXZDWBianMa = item.cXZDWBianMa
      this.bItem.cChePaiHao = null
      this.bItem.openid = null
      this.get_driver() //车辆信息
    },
    //路线
    get_route() {
      XianLuInfo({ keyword: this.route_keyword }).then(res => {
        this.route_list = res.data
      })
    },
    //线路名称
    select_wordRoute(item) {
      this.showWorkRoute = false
      this.bItem.cXianLuBianMa = item.cXianLuBianMa
    },
    //发布订单
    submit_order() {
      this.isavailable = false
      this.bItem.cGongDiBianMa = this.$store.getters.gongdi_info.cGongDiBianMa
      GongChengCheDingDan(this.bItem)
        .then(res => {
          this.$vux.alert.show({
            title: '提示',
            content: '工程车订单发布成功'
          })
          this.resetItem()
          this.get_order() //订单列表
          this.isavailable = true
          return
        })
        .catch(res => {
          this.isavailable = true
        })
    },
    //挖掘机订单
    wj_submit_order() {
      this.isavailable = false
      this.bItem.cGongDiBianMa = this.$store.getters.gongdi_info.cGongDiBianMa
      WaJueJiDingDan(this.bItem)
        .then(res => {
          this.$vux.alert.show({
            title: '提示',
            content: '挖掘机订单发布成功'
          })
          this.resetItem()
          this.get_wj_order() //挖掘机订单
          this.isavailable = true
          return
        })
        .catch(res => {
          this.isavailable = true
        })
    },
    resetItem() {
      this.bItem = {
        cGongDiBianMa: null, //工地编码
        cChePaiHao: null, //车牌号
        openid: null, //驾驶员编码
        cXianLuBianMa: null, //线路编码
        cXZDWBianMa: null, //协作单位编码
        cXZDWMingCheng: null, //协作单位名称
        cGuanLiYuanBianMa: localStorage.getItem('openid') //现场管理员编码
      }
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
    change_work(work_status) {
      var _this = this
      if (work_status == 1) {
        this.showScrollBox = true
      } else {
        this.$vux.confirm.show({
          title: '提示',
          content: '下班后将接收不到订单，是否继续？',
          onConfirm() {
            _this.work_status = work_status
          }
        })
      }
    }
  },
  watch: {
    work_keyword(val, oldVal) {
      //工地列表
      this.get_worksite()
    },
    driver_keyword(val, oldVal) {
      //车辆列表
      this.get_driver()
    },
    cooperation_keyword(val, oldVal) {
      //合作单位列表
      this.get_cooperation()
    },
    route_keyword(val, oldVal) {
      //线路列表
      this.get_route()
    }
  }
}
</script>

<style>
.group-content .weui-cells {
  font-size: 0.373333rem;
}
</style>
<style scoped lang="scss">
.home {
  font-size: 0.373333rem;
}

.work {
  position: relative;
  text-align: center;
  background: #fff;
  padding: 0.266667rem 0;
  border-bottom: 1px solid #efefef;
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
  font-size: 0.373333rem;
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
.task {
  background: #fff;
  margin-top: 0.3rem;
  .title {
    height: 1.066667rem;
    line-height: 1.066667rem;
    padding: 0 0.266667rem;
    border-top: 1px solid #efefef;
    border-bottom: 1px solid #efefef;
  }
  .item-list {
    position: relative;
    padding-right: 2.5rem;
    section {
      border-bottom: 1px solid #ededed;
      padding: 0.266667rem;
    }
    .item {
      padding: 0.06rem 0.266667rem 0.06rem 0;
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
.wk-btn {
  border-top: 1px solid #ededed;
  text-align: center;
  padding: 0.4rem 0;
  .ok-btn {
    background: #ddd;
    color: #fff;
    display: inline-block;
    // padding: 0.16rem 0.8rem;
    border-radius: 3px;
    width: 3rem;
    height: 0.8rem;
    line-height: 0.8rem;
    font-size: 0.373333rem;
  }
}
.group-item {
  position: relative;
  height: 1.066667rem;
  line-height: 1.066667rem;
  padding-left: 2.3rem;
  .lbl {
    position: absolute;
    width: 1.5rem;
    left: 0.266667rem;
    text-align: right;
  }
  .info {
    position: relative;
    .ico-clear {
      position: absolute;
      right: 0.266667rem;
      top: 0.32rem;
      width: 0.426667rem;
      height: 0.426667rem;
      background: url('../../assets/img/delete.png');
      background-size: 100%;
    }
    .btn-select {
      position: relative;
      color: #f00;
      border: 1px solid #f00;
      border-radius: 3px;
      height: 0.6rem;
      line-height: 0.6rem;
      width: 1.6rem;
      text-align: center;
      display: inline-block;
      margin-right: 0.266667rem;
    }
    .btn-select:after {
      content: ' ';
      position: absolute;
      right: -0.266667rem;
      top: 0;
      display: block;
      height: 0.6rem;
      width: 1px;
      background: #efefef;
    }
  }
}
</style>