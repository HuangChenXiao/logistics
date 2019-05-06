<template>
  <div>
    <u-header title="我的订单" :route="menu_route">
      <div class="s-where" @click="showWhere=true">查询</div>
    </u-header>
    <div style="height:46px"></div>
    <div>
      <div v-if="role_code!='003'">
        <tab :line-width="2" v-model="sed_index" active-color="#f00" bar-active-color="#f00">
          <tab-item
            class="vux-center"
            :selected="sed_index === 0"
            @on-item-click="change_tab_index(0)"
            :key="0"
          >工程车订单</tab-item>
          <tab-item
            class="vux-center"
            :selected="sed_index === 1"
            @on-item-click="change_tab_index(1)"
            :key="1"
          >挖掘机订单</tab-item>
        </tab>
      </div>
      <!-- <div style="height:2rem">{{sed_index==1}}</div> -->
      <scroller
        lock-x
        @on-scroll-bottom="onScrollBottom"
        ref="scrollerBottom"
        height="-90"
        :scroll-bottom-offst="200"
      >
        <div class="item-list">
          <div class="item" v-if="sed_index==0" v-for="item in order_list">
            <section>
              <div class="n1">订单号：{{item.cDingDanHao}}</div>
              <div class="n1">工地：{{item.cGongDiMingCheng}}</div>
              <div class="n1">工尾：{{item.cTuWeiMingCheng}}</div>
              <div class="n1">车牌：{{item.cChePaiHao}}</div>
              <div class="n1">派单员：{{item.cGuanLiYuanMingChen}}</div>
              <div class="n1">驾驶员：{{item.cXingMing}}</div>
              <div class="n1">起运时间：{{item.dQiYunShiJian}}</div>
              <div class="n1">确认时间：{{item.dQueRenShiJian}}</div>
              <div class="n1">状态：{{item.cState}}</div>
              <div class="n1">土尾状态：{{item.cTWState}}</div>
            </section>
            <div class="op-btn">
              <div
                class="reset-btn"
                @click="edit_order(item)"
                v-if="role_code=='001' && item.iState!=110"
              >作废</div>
              <div class="confirm-btn" @click="sendDuiDuiJi(item)" v-if="role_code=='001'">补打印</div>
              <div
                class="confirm-btn"
                @click="ok_order(item)"
                v-if="role_code=='002' && item.iState==0"
              >确认订单</div>
              <div
                class="confirm-btn"
                @click="desend_order(item)"
                v-if="role_code=='003' && item.iTWState==0"
              >结束订单</div>
              <!-- <div class="confirm-btn" v-else>
                确认订单
              </div>-->
            </div>
          </div>
          <div class="item" v-if="sed_index==1" v-for="item in wj_order_list">
            <section>
              <div class="n1">订单号：{{item.cDingDanHao}}</div>
              <div class="n1">工地：{{item.cGongDiMingCheng}}</div>
              <div class="n1">车牌：{{item.cChePaiHao}}</div>
              <div class="n1">派单员：{{item.cGuanLiYuanMingChen}}</div>
              <div class="n1">驾驶员：{{item.cXingMing}}</div>
              <div class="n1">开始时间：{{item.dKaiShiShiJian}}</div>
              <div class="n1">结束时间：{{item.dJieShuShiJian}}</div>
              <div class="n1">状态：{{item.cState}}</div>
            </section>
            <div class="op-btn">
              <div
                class="reset-btn"
                @click="edit_order(item)"
                v-if="role_code=='001' && item.iState!=110"
              >作废</div>
              <div
                class="confirm-btn"
                @click="end_order(item)"
                v-if="role_code=='001' && item.iState==0"
              >结束订单</div>
            </div>
          </div>
          <load-more tip="加载中" v-if="!onFetching"></load-more>
          <div class="no-more-data" v-if="noData">
            <span>我是有底线的~</span>
          </div>
        </div>
      </scroller>
    </div>
    <c-bottom v-if="menu_order" route_name="order"></c-bottom>
    <div class="mask" v-show="showWhere">
      <div class="mask-content">
        <div class="title">查询条件</div>
        <group
          label-width="4.5em"
          label-margin-right="2em"
          label-align="right"
          class="group-content"
        >
          <x-input title="订单号" placeholder="请输入订单号" v-model="w_qeury.cDingDanHao"></x-input>
          <div class="cus-item">
            <div class="lbl">工地</div>
            <div class="set-btn" @click="showWorkSite=true">选择</div>
            <span>{{w_qeury.cGongDiMingCheng}}</span>
            <div class="ico-clear" v-if="w_qeury.cGongDiMingCheng" @click="gongdi_change()"></div>
          </div>
          <div class="cus-item">
            <div class="lbl">车辆</div>
            <div class="set-btn" @click="showScrollBox=true">选择</div>
            <span>{{w_qeury.cChePaiHao}}</span>
            <div class="ico-clear" v-if="w_qeury.cChePaiHao" @click="cheliang_change()"></div>
          </div>
          <datetime
            title="开始日期"
            v-model="w_qeury.begindate"
            format="YYYY-MM-DD"
            value-text-align="left"
          ></datetime>
          <datetime
            title="结束日期"
            v-model="w_qeury.enddate"
            format="YYYY-MM-DD"
            value-text-align="left"
          ></datetime>
          <div class="wk-btn">
            <div class="reset-btn" @click="showWhere=false">取消</div>
            <div class="ok-btn" @click="search_order()">确定</div>
            <!-- <div class="ok-btn" @click="end_all_order()">批量结束订单</div> -->
          </div>
        </group>
      </div>
    </div>
    <!-- 车辆信息 -->
    <x-dialog v-model="showScrollBox" :hide-on-blur="true" class="dialog-demo">
      <c-vehicle
        @selectVehicle="select_vehicle"
        :single_drive="true"
        :valueData="vehicle_list"
        v-model="driver_keyword"
      ></c-vehicle>
    </x-dialog>
    <!-- 工地信息 -->
    <x-dialog v-model="showWorkSite" :hide-on-blur="true" class="dialog-demo">
      <work-site @selectVehicle="selectWork" :valueData="work_list" v-model="work_keyword"></work-site>
    </x-dialog>
  </div>
</template>

<script>
// import { getOrderMyList } from '@/api/user.js'
import { Tab, TabItem, XDialog, Datetime } from "vux";
import cVehicle from "@/components/cVehicle";
import workSite from "@/components/workSite";
import getformattedAddress from "@/map/index.js";
import { DuiDuiJi } from "@/api/duidui.js";
import {
  CheLiang,
  GongDiInfo,
  GetGongChengCheDingDan,
  GetWaJueJiDingDan,
  EndWaJueJiDingDan,
  ConfirmOrder,
  DesEndOrder
} from "@/api/home.js";
const list = () => [
  { key: 0, value: "工程车订单" },
  { key: 1, value: "挖掘机订单" }
];
export default {
  components: {
    Tab,
    TabItem,
    XDialog,
    Datetime,
    workSite,
    cVehicle
  },
  data() {
    return {
      showScrollBox: false,
      showWorkSite: false,
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
        cDingDanHao: null, //订单号
        cGongDiBianMa: null, //工地编码
        cGongDiMingCheng: null, //工地名称
        cChePaiHao: null, //车牌号
        js_openid: null, //驾驶员编码
        cGuanLiYuanBianMa: null, //现场管理员编码
        begindate: "",
        enddate: "",
        cTuWeiBianMa: null //土尾编码
      },
      role_code: localStorage.getItem("role_code"),
      wj_order_list: [],
      order_list: [],
      onFetching: false,
      noData: false,
      vehicle_list: [],
      work_list: [],
      driver_keyword: null,
      work_keyword: null
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
  created() {
    //管理员
    if (this.role_code == "001") {
      this.w_qeury.cGuanLiYuanBianMa = localStorage.getItem("openid");
    }
    //驾驶员
    else if (this.role_code == "002") {
      this.w_qeury.js_openid = localStorage.getItem("openid");
    }
    //目的地管理员
    else if (this.role_code == "003") {
      this.w_qeury.cTuWeiBianMa = this.$store.getters.user_info.cTuWeiBianMa;
    }
    //老板
    else if (this.role_code == "005") {
    } else {
      this.$vux.alert.show({
        title: "提示",
        content: "网络异常，请重新登录！"
      });
      document.write("");
      return;
    }
    this.get_worksite();
    this.get_driver();
    this.get_order();
  },
  mounted() {},
  methods: {
    //清空工地条件
    gongdi_change() {
      this.w_qeury.cGongDiBianMa = null;
      this.w_qeury.cGongDiMingCheng = null;
    },
    //清空车辆条件
    cheliang_change() {
      this.w_qeury.cChePaiHao = null;
    },
    //工地名称
    get_worksite() {
      GongDiInfo({ keyword: this.work_keyword }).then(res => {
        this.work_list = res.data;
      });
    },
    //工地名称
    selectWork(val) {
      this.w_qeury.cGongDiBianMa = val.cGongDiBianMa;
      this.w_qeury.cGongDiMingCheng = val.cGongDiMingCheng;
      this.showWorkSite = false;
    },
    //车辆列表
    get_driver() {
      CheLiang({
        keyword: this.driver_keyword
      }).then(res => {
        this.vehicle_list = res.data;
      });
    },
    //车辆名称
    select_vehicle(item) {
      this.showScrollBox = false;
      this.w_qeury.cChePaiHao = item.cChePaiHao;
    },
    search_order() {
      this.init_query();
      this.get_order();
    },
    sendDuiDuiJi(item) {
      var _this=this
      this.$vux.confirm.show({
        title: "提示",
        content: "继续打印订单信息，是否继续？",
        onConfirm() {
          var data = {};
          data.cDingDanHao = item.cDingDanHao;
          data.sign = _this.$md5("71f727c6a543xiamen!@#" + data.cDingDanHao);
          DuiDuiJi(data).then(res => {
            console.log(res);
            _this.$vux.alert.show({
              title: "提示",
              content: res.message
            });
          });
        }
      });
    },
    //目的地管理员结束订单
    desend_order(item) {
      var _this = this;
      this.$vux.confirm.show({
        title: "提示",
        content: "结束后订单完结，是否继续？",
        onConfirm() {
          console.log(_this.sed_index);
          if (_this.sed_index == 0) {
            DesEndOrder({
              cDingDanHao: item.cDingDanHao
            }).then(res => {
              item.iTWState = 100;
              item.cTWState = "确认";
              _this.$vux.alert.show({
                title: "提示",
                content: "操作成功！"
              });
            });
          }
        }
      });
    },
    //确认订单
    ok_order(item) {
      var _this = this;
      this.$vux.confirm.show({
        title: "提示",
        content: "确认后订单完结，是否继续？",
        onConfirm() {
          _this.$store.dispatch("setisLoading", true);
          if (_this.sed_index == 0) {
            getformattedAddress({
              windowurl: decodeURIComponent(window.location.href)
            })
              .then(res => {
                var lat = res.lat;
                var lng = res.lng;
                ConfirmOrder({
                  cDingDanHao: item.cDingDanHao,
                  cTuWeiBianMa: item.cTuWeiBianMa,
                  lng: lng,
                  lat: lat
                })
                  .then(res => {
                    item.iState = 100;
                    item.cState = "确认";
                    item.dQueRenShiJian = res.data;
                    _this.$vux.alert.show({
                      title: "提示",
                      content: "操作成功！"
                    });
                    _this.$store.dispatch("setisLoading", false);
                  })
                  .catch(res => {
                    _this.$store.dispatch("setisLoading", false);
                  });
              })
              .catch(res => {
                _this.$store.dispatch("setisLoading", false);
              });
          }
        }
      });
    },
    //作废订单
    edit_order(item) {
      var _this = this;
      this.$vux.confirm.show({
        title: "提示",
        content: "只有未确认的订单才能作废，是否继续？",
        onConfirm() {
          console.log(_this.sed_index);
          if (_this.sed_index == 0) {
            GetGongChengCheDingDan({
              cDingDanHao: item.cDingDanHao
            }).then(res => {
              item.iState = 110;
              item.cState = "作废";
              _this.$vux.alert.show({
                title: "提示",
                content: "操作成功！"
              });
            });
          } else {
            GetWaJueJiDingDan({ cDingDanHao: item.cDingDanHao }).then(res => {
              item.iState = 110;
              _this.$vux.alert.show({
                title: "提示",
                content: "操作成功！"
              });
            });
          }
        }
      });
    },
    end_order(item) {
      var _this = this;
      this.$vux.confirm.show({
        title: "提示",
        content: "只有到达目的地后才能结束订单，是否继续？",
        onConfirm() {
          EndWaJueJiDingDan({ cDingDanHao: item.cDingDanHao }).then(res => {
            item.iState = 100;
            item.cState = "确认";
            item.dJieShuShiJian = res.data;
            _this.$vux.alert.show({
              title: "提示",
              content: "操作成功！"
            });
          });
        }
      });
    },
    //工程车订单列表
    get_order() {
      console.log(this.sed_index);
      if (this.sed_index == 0) {
        this.onFetching = true;
        GetGongChengCheDingDan(this.w_qeury).then(res => {
          this.showWhere = false;
          if (res.data.length) {
            this.order_list = this.order_list.concat(res.data);
            // this.$nextTick(() => {
            //   this.$refs.scrollerBottom.reset()
            // })
            this.w_qeury.page++;
            if (res.data.length < 10) {
              this.onFetching = true;
              this.noData = true;
            } else {
              this.onFetching = false;
            }
          } else {
            this.onFetching = true;
            this.noData = true;
          }
        });
      } else {
        GetWaJueJiDingDan(this.w_qeury).then(res => {
          this.showWhere = false;
          if (res.data.length) {
            this.wj_order_list = this.wj_order_list.concat(res.data);
            // this.$nextTick(() => {
            //   this.$refs.scrollerBottom.reset()
            // })
            this.w_qeury.page++;
            if (res.data.length < 10) {
              this.onFetching = true;
              this.noData = true;
            } else {
              this.onFetching = false;
            }
          } else {
            this.onFetching = true;
            this.noData = true;
          }
        });
      }
    },
    //批量结束订单
    end_all_order() {
      var _this = this;
      this.$vux.confirm.show({
        title: "提示",
        content: "批量结束满足条件的订单？",
        onConfirm() {
          var data = Object.assign({}, _this.w_qeury);
        }
      });
    },
    select_wordRoute(res) {
      this.showWorkRoute = false;
    },
    init_query() {
      this.order_list = [];
      this.wj_order_list = [];
      this.onFetching = false;
      this.w_qeury.page = 1;
      this.$nextTick(() => {
        this.$refs.scrollerBottom.reset({ top: 0 });
      });
    },
    change_tab_index(index) {
      this.sed_index = index;
      this.init_query();
      this.onScrollBottom();
    },
    goPayment(item) {
      if (item.status == 0) {
        this.$router.push({
          name: "purchase-details_payment",
          query: {
            orderno: item.orderno,
            tradetype: this.w_qeury.tradetype
          }
        });
      }
    },
    onScrollBottom() {
      if (this.onFetching) {
        // do nothing
      } else {
        this.onFetching = true;
        this.get_order();
      }
    }
  },
  watch: {
    driver_keyword(val, oldVal) {
      this.get_driver();
    },
    work_keyword(val, oldVal) {
      this.get_worksite();
    }
  }
};
</script>
<style>
.vux-tab .vux-tab-item {
  font-size: 0.5rem !important;
}
.group-content .weui-cells {
  font-size: 0.5rem;
}
</style>
<style scoped lang="scss">
.item-list {
  background: #fff;
  font-size: 0.5rem;
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
      .reset-btn {
        display: inline-block;
        color: #b3b3b3;
        border: 1px solid #ddd;
        background: #fff;
        padding: 0.08rem 0.133333rem;
        border-radius: 3px;
      }
    }
  }
}
.s-where {
  color: #f00;
  font-size: 0.5rem;
}
.mask-content {
  padding-bottom: 0.333333rem;
  font-size: 0.5rem;
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
    font-size: 0.5rem;
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
  .ico-clear {
    position: absolute;
    right: 0.266667rem;
    top: 0.32rem;
    width: 0.426667rem;
    height: 0.426667rem;
    background: url("../../assets/img/delete.png");
    background-size: 100%;
  }
  .lbl {
    position: absolute;
    width: 4.5em;
    text-align: right;
    margin-right: 2em;
    left: 15px;
  }
  .set-btn {
    position: relative;
    border: 1px solid #f00;
    color: #f00;
    text-align: center;
    display: inline-block;
    border-radius: 3px;
    padding: 2px 5px;
    margin-right: 0.3rem;
  }
  .set-btn:after {
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
</style>