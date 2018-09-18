<template>
  <div>
    <n-header title="首页">
    </n-header>
    <div style="height:1.26rem"></div>
    <div class="home">
      <div class="address">
        当前位置：{{bItem.start_position}}
        <i class="ico ico-ad" onclick="location.reload()"></i>
      </div>
      <div class="work">
        <div class="work-item sb-item" v-if="work_status==2" @click="change_work(1)">
          <i class="ico ico-sb"></i>
          <span>上班</span>
        </div>
        <div class="work-item xb-item" v-if="work_status==1" @click="change_work(2)">
          <i class="ico ico-xb"></i>
          <span>下班</span>
        </div>
      </div>
      <div class="info">
        <div class="item">
          <div class="lbl">当前时间：{{time}}</div>
        </div>
        <div class="item">
          <div class="lbl">我的状态：
            <span :class="{'gen':work_status==1}">{{work_status|workMethod}}</span>
          </div>
        </div>
        <div class="item">
          <div class="lbl">我的车辆：
            <span>{{vehicle_info}}</span>
          </div>
        </div>
      </div>
      <div class="count-item clearfix">
        <div class="item it_1">
          <div class="title">昨日订单</div>
          <div class="qty">单量：10</div>
        </div>
        <div class="item it_2">
          <div class="title">今日订单</div>
          <div class="qty">单量：20</div>
        </div>
      </div>

      <div class="task">
        <div class="title">最新订单</div>
        <div class="item-list">
          <div class="item">订单号：TGF13301111133332</div>
          <div class="item">开始地址：厦门市软件园二期</div>
          <div class="item">结束地址：厦门市软件园三期</div>
          <!-- <div class="sus-btn" @click="complete_order">
            完成订单
          </div> -->
        </div>
      </div>
    </div>
    <!-- <div style="margin:0">
      <group label-width="4.5em" label-margin-right="2em" label-align="right">

        <x-input title="当前位置" placeholder="定位中..." v-model="bItem.start_position"></x-input>
      </group>
    </div> -->
    <c-bottom route_name="home"></c-bottom>
    <x-dialog v-model="showScrollBox" :hide-on-blur="true" class="dialog-demo">
      <c-vehicle @selectVehicle="select_vehicle" v-model="vehicle_list"></c-vehicle>
    </x-dialog>
  </div>
</template>

<script>
import { Group, XDialog } from 'vux'
import { getAdList } from '@/api/home.js'
import getformattedAddress from '@/map/index.js'
import { setTimeout } from 'timers'
import cVehicle from '@/components/cVehicle'
import workSite from '@/components/workSite'
import { defaultCoreCipherList } from 'constants';
export default {
  components: {
    Group,
    XDialog,
    cVehicle,
    workSite
  },
  data() {
    return {
      vehicle_list: [
        { id: 1, code: '闽A12345', color: '红色' },
        { id: 2, code: '闽A12345', color: '白色' },
        { id: 2, code: '闽A4564', color: '黑色' }
      ],
      showScrollBox: false,
      vehicle_info: null,
      task_status: false,
      work_status: 2,
      time: null,
      bItem: {
        start_position: '厦门市'
      },
      store_query: {
        longitude: this.$store.getters.longitude,
        latitude: this.$store.getters.latitude,
        startrow: 1,
        pagesize: 10
      }
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

<style scoped lang="scss">
.home {
  font-size: 0.373333rem;
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
.info {
  background: #fff;
  padding: 0.266667rem;
  border-top: 1px solid #efefef;
}
.count-item {
  background: #fff;
  border-top: 1px solid #efefef;
  .item {
    float: left;
    width: 50%;
    text-align: center;
    padding: 0.533333rem 0;
    font-size: 0.373333rem;
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
</style>