<template>
  <div>
    <n-header title="首页">
    </n-header>
    <div style="height:1.26rem"></div>
    <div class="home">
      <div class="address">
        我的工地：厦门市软件园二期
        <i class="ico ico-edit" @click="showWorkSite=true"></i>
      </div>
      <div class="address">
        当前位置：{{bItem.start_position}}
        <i class="ico ico-ad" onclick="location.reload()"></i>
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
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">车辆</div>
                  <div class="info">
                    <div class="btn-select" @click="showScrollBox=true">
                      选择
                    </div>
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">路线</div>
                  <div class="info">
                    <div class="btn-select" @click="showWorkRoute=true">
                      选择
                    </div>
                  </div>
                </div>
                <!-- <selector title="合作单位"  v-model="value2"></selector>
                                <selector title="车辆" :options="['闽G123', '其他']" v-model="value2"></selector>
                                <selector title="路线" :options="['高新技术园-软件园', '五缘湾-火车站']" v-model="value2"></selector> -->
                <div class="wk-btn">
                  <div class="ok-btn">发布订单</div>
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
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">车辆</div>
                  <div class="info">
                    <div class="btn-select" @click="showScrollBox=true">
                      选择
                    </div>
                  </div>
                </div>
                <div class="wk-btn">
                  <div class="ok-btn">发布订单</div>
                </div>
              </group>
            </div>
          </swiper-item>
        </swiper>
      </div>
      <div class="task">
        <div class="title">最新订单</div>
        <div class="item-list">
          <section v-for="item in order_list">
            <div class="n1">订单号：{{item.orderno}}</div>
            <div class="n1">工地：{{item.work}}</div>
            <div class="n1">工尾：{{item.tail_work}}</div>
            <div class="n1">车牌：{{item.license_plate}}</div>
            <div class="n1">驾驶员：{{item.name}}</div>
            <div class="n1">起运时间：{{item.addtime}}</div>
            <div class="n1">状态：{{item.status}}</div>
          </section>
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
    <x-dialog v-model="showCooperation" :hide-on-blur="true" class="dialog-demo">
      <cooperation @selectVehicle="select_cooperation" v-model="cooperation_list"></cooperation>
    </x-dialog>
    <x-dialog v-model="showScrollBox" :hide-on-blur="true" class="dialog-demo">
      <c-vehicle @selectVehicle="select_vehicle" v-model="vehicle_list"></c-vehicle>
    </x-dialog>
    <x-dialog v-model="showWorkRoute" :hide-on-blur="true" class="dialog-demo">
      <work-route @selectVehicle="select_wordRoute" v-model="route_list"></work-route>
    </x-dialog>
    <x-dialog v-model="showWorkSite" class="dialog-demo">
      <work-site @selectVehicle="selectWork" v-model="work_list"></work-site>
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
import { getAdList } from '@/api/home.js'
import getformattedAddress from '@/map/index.js'
import { setTimeout } from 'timers'
import cVehicle from '@/components/cVehicle'
import cooperation from '@/components/cooperation'
import workSite from '@/components/workSite'
import workRoute from '@/components/workRoute'
import adminBottom from '@/components/adminBottom'
import { defaultCoreCipherList } from 'constants'
const list = () => ['工程车', '挖掘机']
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
      showCooperation: false,
      showScrollBox: false,
      showWorkRoute: false,
      list2: list(),
      index: 0,
      demo2: '美食',
      showWorkSite: false,
      vehicle_info: null,
      bItem: {
        start_position: '厦门市'
      },
      store_query: {
        longitude: this.$store.getters.longitude,
        latitude: this.$store.getters.latitude,
        startrow: 1,
        pagesize: 10
      },
      vehicle_list: [
        { id: 1, name: '黄臣晓', code: '闽A12345', color: '红色' },
        { id: 2, name: '黄臣晓', code: '闽A12345', color: '白色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' },
        { id: 2, name: '黄臣晓', code: '闽A4564', color: '黑色' }
      ],
      cooperation_list: [
        { id: 1, code: '阿里巴巴集团' },
        { id: 2, code: '腾讯控股' }
      ],
      route_list: [
        { id: 1, code: '莲花新城-中美新城' },
        { id: 2, code: '厦门大学-火车站' }
      ],
      work_list: [
        { id: 1, code: '厦门市集美区软件园一号' },
        { id: 2, code: '厦门北站' }
      ],
      order_list: [
        {
          orderno: 'DX95546346546464161',
          work: '厦门市软件园二期何厝路口东路',
          tail_work: '厦门市杏林路杏林湾东二路',
          license_plate: '闽A123',
          name: '黄臣晓',
          addtime: '2018-9-29 10:52:24',
          status: '未确认'
        },{
          orderno: 'DX95546346546464161',
          work: '厦门市软件园二期何厝路口东路',
          tail_work: '厦门市杏林路杏林湾东二路',
          license_plate: '闽A123',
          name: '黄臣晓',
          addtime: '2018-9-29 10:52:24',
          status: '未确认'
        }
      ]
    }
  },
  created() {
    let _this = this
    // if (_this.store_query.start_position) {
    //   _this.bItem.start_position = _this.store_query.start_position
    // } else {
    //   _this.get_address()
    // }
    this.set_time()
    setTimeout(() => {
      _this.$vux.alert.show({
        title: '提示',
        content: '接收到一个订单，请尽快执行',
        onShow() {
          _this.task_status = true
        }
      })
    }, 1000000)
  },
  methods: {
    selectWork(val) {
      this.showWorkSite = false
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
      this.vehicle_info = item.code
      this.work_status = 1
    },
    select_cooperation(item) {
      this.showCooperation = false
    },
    select_wordRoute(res) {
      this.showWorkRoute = false
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
    padding: 0.16rem 0.8rem;
    border-radius: 3px;
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
    .btn-select {
      color: #f00;
      border: 1px solid #f00;
      border-radius: 3px;
      height: 0.6rem;
      line-height: 0.6rem;
      width: 1.6rem;
      text-align: center;
      display: inline-block;
    }
  }
}
</style>