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
      <!-- <div class="address">
        限定车辆：
      <check-icon :value.sync="xianding_chelaing" type="plain"> </check-icon>
      </div> -->
      <div class="address">
        当前位置：{{bItem.cDiZhi}}
        <i class="ico ico-ad" onclick="location.reload()"></i>
      </div>

      <div class="work"  v-if="work_status==0">
        <div class="work-item sb-item" @click="change_work(1)">
          <i class="ico ico-sb"></i>
          <span>上班</span>
        </div>
        <div class="work-item xb-item" v-if="false" @click="change_work(0)">
          <i class="ico ico-xb"></i>
          <span>下班</span>
        </div>
      </div>
      <div>
        <tab :line-width=2 active-color='#f00' v-model="index">
          <tab-item class="vux-center"  v-for="(item, index) in list2" @on-item-click="set_tab_item(item)" :key="index">{{item}}</tab-item>
        </tab>
        <swiper v-model="index" height="150px" :show-dots="false">
          <swiper-item v-for="(item, index) in list2" :key="index">
            <div class="tab-swiper vux-center" v-if="index==0">
              <group label-width="4.5em" label-margin-right="2em" label-align="right" class="group-content">
                <!-- <div class="group-item">
                  <div class="lbl">合作单位</div>
                  <div class="info">
                    <div class="btn-select" @click="showCooperation=true">
                      选择
                    </div>
                    <span>{{bItem.cXZDWMingCheng}}</span>
                    <div class="ico-clear" v-if="bItem.cXZDWMingCheng" @click="chepai_change()"></div>
                  </div>
                </div> -->
                <div class="group-item">
                  <div class="lbl">车辆</div>
                  <div class="info">
                    <div class="btn-select" @click="setshowScrollBox">
                      选择
                    </div>
                    <span v-if="bItem.cChePaiHao">{{bItem.cChePaiHao}}—{{bItem.cXingMing}}</span>
                    <div class="ico-clear" v-if="bItem.cChePaiHao" @click="cheliang_change()"></div>
                  </div>
                </div>
                <!-- <div class="group-item" v-if="bItem.cXingMing">
                  <div class="lbl">驾驶员</div>
                  <div class="info">
                    <span>{{bItem.cXingMing}}</span>
                  </div>
                </div> -->
                <div class="group-item">
                  <div class="lbl">土尾</div>
                  <div class="info">
                    <div class="btn-select" @click="setshowWorkRoute">
                      选择
                    </div>
                    <span>{{bItem.cTuWeiMingCheng}}</span>
                    <div class="ico-clear" v-if="bItem.cTuWeiMingCheng" @click="clearTuiwei"></div>
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
                <!-- <div class="group-item">
                  <div class="lbl">合作单位</div>
                  <div class="info">
                    <div class="btn-select" @click="showCooperation=true">
                      选择
                    </div>
                    <span>{{bItem.cXZDWMingCheng}}</span>
                    <div class="ico-clear" v-if="bItem.cXZDWMingCheng" @click="chepai_change()"></div>
                  </div>
                </div> -->
                <div class="group-item">
                  <div class="lbl">车辆</div>
                  <div class="info">
                    <div class="btn-select" @click="setshowScrollBox">
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
        </div>
        <div class="tab-order" v-else>
          <div class="title">最新订单</div>
          <div class="item-list">
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
        </div>
      </div>
    </div>
    <div style="height:1.5rem"></div>
    <!-- <div style="margin:0">
      <group label-width="4.5em" label-margin-right="2em" label-align="right">

        <x-input title="当前位置" placeholder="定位中..." v-model="bItem.cDiZhi"></x-input>
      </group>
    </div> -->
    <admin-bottom route_name="admin-index"></admin-bottom>
    <!-- 合作单位 -->
    <x-dialog v-model="showCooperation" :hide-on-blur="true" class="dialog-demo">
      <cooperation @selectVehicle="select_cooperation" :valueData="cooperation_list" v-model="cooperation_keyword"></cooperation>
    </x-dialog>
    <!-- 车辆信息 -->
    <x-dialog v-model="showScrollBox" :hide-on-blur="true" class="dialog-demo">
      <c-vehicle @selectVehicle="select_vehicle" title="车辆信息" :single_drive="true" :valueData="vehicle_list" v-model="driver_keyword"></c-vehicle>
    </x-dialog>
    <!-- 线路 -->
    <x-dialog v-model="showWorkRoute" :hide-on-blur="true" class="dialog-demo">
      <work-route @selectVehicle="select_wordRoute" :valueData="route_list" v-model="route_keyword">
          <router-link  :to="{name:'tu-wei'}" class="right-btn">
              新增
          </router-link>
      </work-route>
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
  DatetimeRange,
  CheckIcon
} from "vux";
import getformattedAddress from "@/map/index.js";
import { setTimeout } from "timers";
import cVehicle from "@/components/cVehicle";
import cooperation from "@/components/cooperation";
import workSite from "@/components/workSite";
import workRoute from "@/components/workRoute";
import adminBottom from "@/components/adminBottom";
import { defaultCoreCipherList } from "constants";
const list = () => ["工程车", "挖掘机"];

import {
  GongDiInfo,
  GongDiCheLiang,
  XieZuoDanWeiInfo,
  GongDiXianLu,
  GongChengCheDingDan,
  GetGongChengCheDingDan,
  WaJueJiDingDan,
  GetWaJueJiDingDan,
  BangDingJiLu,
  wechatUser,
  TuWeiInfo,
  getChePaiTuWei,
  UpdateTuWeriSortDate,
  EditBangDingJiLuGongDi
} from "@/api/home.js";
import { TemplateMsg } from "@/api/wechat.js";
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
    workRoute,
    CheckIcon
  },
  data() {
    return {
      xianding_chelaing: false,
      work_status: 1,
      isavailable: true,
      showCooperation: false,
      showScrollBox: false,
      showWorkRoute: false,
      list2: list(),
      index: 0,
      showWorkSite: false,
      bItem: {
        cDiZhi: "",
        cGongDiBianMa: null, //工地编码
        cGongDiMingCheng:null,//工地名称
        cChePaiHao: null, //车牌号
        openid: null, //驾驶员编码
        cTuWeiBianMa: null, //土尾编码
        // cXZDWBianMa: null, //协作单位编码
        cXZDWMingCheng: null, //协作单位名称
        cTuWeiMingCheng: null, //土尾名称
        cGuanLiYuanBianMa: localStorage.getItem("openid"), //现场管理员编码
        cXingMing:null,//驾驶员姓名
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
    };
  },
  filters: {
    status_filters(val) {
      var valMap = {
        0: "未确认",
        100: "确认",
        110: "作废"
      };
      return valMap[val];
    }
  },
  computed: {
    cGongDiMingCheng() {
      if (this.$store.getters.gongdi_info.cGongDiMingCheng) {
        return this.$store.getters.gongdi_info.cGongDiMingCheng;
      } else {
        return "请选择";
      }
    },
    cGongDiBianMa() {
      if (this.$store.getters.gongdi_info.cGongDiBianMa) {
        return this.$store.getters.gongdi_info.cGongDiBianMa;
      } else {
        return "";
      }
    },
    bXianZhi() {
      if (this.$store.getters.gongdi_info.bXianZhi) {
        return this.$store.getters.gongdi_info.bXianZhi;
      } else {
        return false;
      }
    },
    validate_order() {
      if (
        this.bItem.cChePaiHao &&
        this.bItem.openid &&
        this.bItem.cTuWeiBianMa &&
        this.bItem.cGuanLiYuanBianMa &&
        this.isavailable &&
        this.work_status == 1
      ) {
        return true;
      }
      return false;
    },
    wj_validate_order() {
      if (
        this.bItem.cChePaiHao &&
        this.bItem.openid &&
        this.bItem.cGuanLiYuanBianMa &&
        this.isavailable &&
        this.work_status == 1
      ) {
        return true;
      }
      return false;
    }
  },
  created() {
    let _this = this;
    if (!this.$store.getters.gongdi_info.cGongDiMingCheng) {
      //未选择工地时弹出
      this.showWorkSite = true;
    }
    if (_this.store_query.cDiZhi) {
      _this.bItem.cDiZhi = _this.store_query.cDiZhi
    } else {
      _this.get_address()
    }
    this.set_time();
    // setTimeout(() => {
    //   _this.$vux.alert.show({
    //     title: '提示',
    //     content: '接收到一个订单，请尽快执行',
    //     onShow() {
    //       _this.task_status = true
    //     }
    //   })
    // }, 1000000)
    this.xianding_chelaing = this.$store.getters.xianding_chelaing;
    this.get_worksite(); //工地信息
    this.get_driver(); //车辆信息
    this.get_route(); //线路
    // this.get_cooperation() //合作单位
    this.get_order(); //工程车订单列表
    this.get_wj_order(); //挖掘机订单
    this.get_bangding(); //绑定记录
  },
  methods: {
    clearTuiwei() {
      this.bItem.cTuWeiBianMa = null;
      this.bItem.cTuWeiMingCheng = null;
    },
    //选择时重新查询车辆
    setshowScrollBox() {
      this.get_driver(); //车辆信息
      this.showScrollBox = true;
    },
    //选择时重新查询土尾
    setshowWorkRoute(){
      this.get_route();//土尾信息
      this.showWorkRoute = true;
    },
    //切换时清空数据
    set_tab_item(item) {
      this.demo2 = item;
      this.bItem.cChePaiHao = null;
      this.bItem.openid = null;
    },
    //查询上班状态
    get_bangding() {
      wechatUser().then(res => {
        this.work_status = res.data.status;
      });
    },
    change_work(work_status) {
      var _this = this;
      if (work_status == 0) {
        this.$vux.confirm.show({
          title: "提示",
          content: "下班后将不能发布订单，是否继续？",
          onConfirm() {
            BangDingJiLu({
              iBangDingLeiXing: work_status,
              cShangBanBianMa: _this.$store.getters.user_info.role_code
            }).then(res => {
              _this.work_status = work_status;
            });
          }
        });
      } else {
        BangDingJiLu({
          iBangDingLeiXing: work_status,
          cShangBanBianMa: _this.$store.getters.user_info.role_code,
          cGongDiBianMa:_this.cGongDiBianMa,
          cDiZhi:_this.bItem.cDiZhi
        }).then(res => {
          _this.work_status = work_status;
        });
      }
    },
    //清空车辆
    cheliang_change() {
      this.bItem.cChePaiHao = null;
      this.bItem.openid = null;
    },
    //清空合作单位
    chepai_change() {
      this.bItem.cXZDWMingCheng = null;
      this.driver_cXZDWBianMa = null;
      this.bItem.cChePaiHao = null;
      this.bItem.openid = null;
      this.get_driver(); //车辆信息
    },
    //工程车订单列表
    get_order() {
      GetGongChengCheDingDan().then(res => {
        this.order_list = res.data;
      });
    },
    //挖掘机订单列表
    get_wj_order() {
      GetWaJueJiDingDan().then(res => {
        this.wj_order_list = res.data;
      });
    },
    //工地列表
    get_worksite() {
      GongDiInfo({ keyword: this.work_keyword }).then(res => {
        this.work_list = res.data;
      });
    },
    //工地名称
    selectWork(val) {
      this.$store.dispatch("SGongDiMingCheng", val).then(res => {
        localStorage.setItem("cGongDiMingCheng", JSON.stringify(val));
        EditBangDingJiLuGongDi({cGongDiBianMa:val.cGongDiBianMa}).then(res=>{
          console.log('修改成功')
        })
      });
      this.showWorkSite = false;
    },
    //车辆列表
    get_driver() {
      if (this.cGongDiBianMa) {
        GongDiCheLiang({
          keyword: this.driver_keyword,
          cGongDiBianMa: this.cGongDiBianMa,
          bXianZhi: this.bXianZhi,
          cCheLiangLeiBie: this.index == 0 ? "土方车" : "挖掘机"
        }).then(res => {
          this.vehicle_list = res.data;
        });
      }
    },
    //车辆名称
    select_vehicle(item) {
      this.showScrollBox = false;
      this.bItem.cChePaiHao = item.cChePaiHao;
      this.bItem.openid = item.openid;
      this.bItem.cXingMing=item.cXingMing;
      // getChePaiTuWei({ cChePaiHao: item.cChePaiHao }).then(res => {
      //   if (res.data) {
      //     this.bItem.cTuWeiBianMa = res.data.cTuWeiBianMa;
      //     this.bItem.cTuWeiMingCheng = res.data.cTuWeiMingCheng;
      //   } else {
      //     this.bItem.cTuWeiBianMa = null;
      //     this.bItem.cTuWeiMingCheng = null;
      //   }
      // });
    },
    //合作单位
    get_cooperation() {
      XieZuoDanWeiInfo({ keyword: this.cooperation_keyword }).then(res => {
        this.cooperation_list = res.data;
      });
    },
    //合作单位名称
    select_cooperation(item) {
      this.showCooperation = false;
      this.bItem.cXZDWBianMa = item.cXZDWBianMa;
      this.bItem.cXZDWMingCheng = item.cXZDWMingCheng;
      //选择合作单位后车辆条件加上 driver_cXZDWBianMa 清空驾驶员
      this.driver_cXZDWBianMa = item.cXZDWBianMa;
      this.bItem.cChePaiHao = null;
      this.bItem.openid = null;
      this.get_driver(); //车辆信息
    },
    //路线
    get_route() {
      TuWeiInfo({
        keyword: this.route_keyword
      }).then(res => {
        this.route_list = res.data;
      });
    },
    //线路名称
    select_wordRoute(item) {
      this.showWorkRoute = false;
      this.bItem.cTuWeiBianMa = item.cTuWeiBianMa;
      this.bItem.cTuWeiMingCheng = item.cTuWeiMingCheng;
      UpdateTuWeriSortDate({cTuWeiBianMa:item.cTuWeiBianMa})
    },
    sendTplMsg(item) {
      TemplateMsg(item).then(res => {
        console.log(res);
      });
    },
    //发布订单
    submit_order() {
      this.isavailable = false;
      this.bItem.cGongDiBianMa = this.$store.getters.gongdi_info.cGongDiBianMa;
      this.bItem.cGongDiMingCheng = this.cGongDiMingCheng
      this.bItem.cDingDanHao = this.cfg.formatOrderNo();
      GongChengCheDingDan(this.bItem)
        .then(res => {
          this.$vux.alert.show({
            title: "提示",
            content: "工程车订单发布成功"
          });
          this.sendTplMsg(this.bItem);
          this.resetItem();
          this.get_order(); //订单列表
          this.isavailable = true;
          return;
        })
        .catch(res => {
          this.isavailable = true;
        });
    },
    //挖掘机订单
    wj_submit_order() {
      this.isavailable = false;
      this.bItem.cGongDiBianMa = this.$store.getters.gongdi_info.cGongDiBianMa;
      this.bItem.cGongDiMingCheng = this.cGongDiMingCheng
      this.bItem.cDingDanHao = this.cfg.formatOrderNo();
      WaJueJiDingDan(this.bItem)
        .then(res => {
          this.$vux.alert.show({
            title: "提示",
            content: "挖掘机订单发布成功"
          });
          this.sendTplMsg(this.bItem);
          this.resetItem();
          this.get_wj_order(); //挖掘机订单
          this.isavailable = true;
          return;
        })
        .catch(res => {
          this.isavailable = true;
        });
    },
    resetItem() {
      this.bItem = {
        cGongDiBianMa: null, //工地编码
        cChePaiHao: null, //车牌号
        openid: null, //驾驶员编码
        cTuWeiBianMa: this.bItem.cTuWeiBianMa, //线路编码
        cTuWeiBianMa:this.bItem.cTuWeiBianMa,//土尾编码
        cTuWeiMingCheng:this.bItem.cTuWeiMingCheng,//土尾名称
        cXZDWBianMa: null, //协作单位编码
        cXZDWMingCheng: null, //协作单位名称
        cGuanLiYuanBianMa: localStorage.getItem("openid") //现场管理员编码
      };
    },
    complete_order() {
      this.$vux.confirm.show({
        title: "提示",
        content: "只有到达目的地与管理员确认后才能操作，是否继续？",
        onConfirm() {
          _this.work_status = work_status;
        }
      });
    },
    get_address() {
      let _this = this;
      _this.bItem.cDiZhi = "正在定位。。。";
      _this.$store.dispatch("setisLoading", true);
      
      getformattedAddress({windowurl:window.location.href}).then(res => {
        _this.bItem.cDiZhi = res.detail.address;
        // console.log(res.regeocode.formattedAddress)
        _this.$store.dispatch("setisLoading", false);
      });
    },
    set_time() {
      var _this = this;
      var t = null;
      t = setTimeout(time, 1000); //開始运行
      function time() {
        clearTimeout(t); //清除定时器
        var dt = new Date();
        var h = checkTime(dt.getHours()); //获取时
        var m = checkTime(dt.getMinutes()); //获取分
        var s = checkTime(dt.getSeconds()); //获取秒
        _this.time = h + "：" + m + "：" + s;
        t = setTimeout(time, 1000); //设定定时器，循环运行
      }
      function checkTime(i) {
        //将0-9的数字前面加上0，例1变为01
        if (i < 10) {
          i = "0" + i;
        }
        return i;
      }
    }
  },
  watch: {
    index(val, oldVal) {
      this.get_driver(); //车辆信息
    },
    cGongDiMingCheng(val, oldVal) {
      this.get_driver(); //车辆信息
      this.get_route(); //线路
    },
    work_keyword(val, oldVal) {
      //工地列表
      this.get_worksite();
    },
    driver_keyword(val, oldVal) {
      //车辆列表
      this.get_driver();
    },
    cooperation_keyword(val, oldVal) {
      //合作单位列表
      this.get_cooperation();
    },
    route_keyword(val, oldVal) {
      //线路列表
      this.get_route();
    },
    xianding_chelaing(val, oldVal) {
      this.$store.dispatch("SXianding_Chelaing", val);
    }
  }
};
</script>

<style>
.group-content .weui-cells {
  font-size: .5rem;
}
</style>
<style scoped lang="scss">
.home {
  font-size: .5rem;
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
    height: 2rem;
    width: 2rem;
    border-radius: 2rem;
    font-weight: 600;
    .ico {
      position: absolute;
      left: 0.5rem;
      top: 0.1rem;
      width: 1rem;
      height: 1rem;
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
    content: " ";
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
    font-size: .5rem;
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
      background: url("../../assets/img/delete.png");
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
      content: " ";
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
.right-btn {
  position: absolute;
  right: 0.27rem;
  top: 0;
  font-size: .5rem;
  color: #42a0ff;
  display: block;
  padding-left: .5rem;
}
</style>