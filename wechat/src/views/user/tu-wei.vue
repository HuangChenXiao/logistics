<template>
  <div>
    <u-header title="土尾列表" route="admin-user" :route="route" :iButton="true">
      <div class="s-where">
        <span class="op-btn" @click="getAddForm">新增</span>
        <span class="op-btn" @click="showWhere=true">查询</span>
      </div>
    </u-header>
    <div style="height:46px"></div>
    <div>
      <scroller lock-x @on-scroll-bottom="onScrollBottom" ref="scrollerBottom" height="-50" :scroll-bottom-offst="200">
        <div class="item-list">
          <div class="item" v-if="sed_index==0" v-for="item in order_list">
            <section>
              <div class="n1">土尾编码：{{item.cTuWeiBianMa}}</div>
              <div class="n1">土尾名称：{{item.cTuWeiMingCheng}}</div>
              <div class="n1" v-if="item.cTuWeiDiZhi">土尾地址：{{item.cTuWeiDiZhi}}</div>
              <div class="n1">收费方式：{{item.cShouFeiFangShi}}</div>
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
    <div class="mask" v-show="showWhere">
      <div class="mask-content">
        <div class="title">查询条件</div>
        <group label-width="4.5em" label-margin-right="2em" label-align="right" class="group-content">
          <x-input title="条件" placeholder="土尾名称/土尾编码" v-model="w_qeury.keyword"></x-input>
          
          <div class="wk-btn">
            <div class="reset-btn" @click="showWhere=false">取消</div>
            <div class="ok-btn" @click="search_order()">确定</div>
            <!-- <div class="ok-btn" @click="end_all_order()">批量结束订单</div> -->
          </div>
        </group>
      </div>
    </div>
    <x-dialog v-model="showTuWei" :hide-on-blur="true" class="dialog-demo" mask-z-index="1">
        <group label-width="4.5em" label-margin-right="2em" label-align="right" class="group-content">
            <div class="title">
                <span>新增土尾信息</span> 
            </div>
            <!-- <x-input title="土尾编码" placeholder="请输入土尾编码" v-model="ruleForm.cTuWeiBianMa"></x-input> -->
            <x-input title="土尾名称" placeholder="请输入土尾名称" v-model="ruleForm.cTuWeiMingCheng"></x-input>
            <selector title="收费方式" placeholder="请选择收费方式" :options="['免费', '付费']" v-model="ruleForm.cShouFeiFangShi"></selector>
            <x-input title="地址选择" readonly>
                <x-button slot="right" type="primary" @click.native="getMapCenter()"  mini style="background-color: #fff;;color:#f00;border:1px solid #f00">{{select_map}}</x-button>
            </x-input>
            <x-input title="土尾地址" v-model="ruleForm.cTuWeiDiZhi"></x-input>
            <div class="wk-btn">
                <div class="reset-btn" @click="showTuWei=false">取消</div>
                <div class="ok-btn" @click="add_tuwei()">确定</div>
                <!-- <div class="ok-btn" @click="end_all_order()">批量结束订单</div> -->
            </div>
        </group>
    </x-dialog>
    <x-dialog v-model="showTuWeiMap" class="dialog-demo" mask-z-index="2">
        <div class="map-content">
            <div class="search-title">土尾地址选择</div>
            <!-- <div class="map-search">
              <div class="lbl">搜索</div> <input type="text">
            </div> -->
            <div id="container"></div>
            <div class="wk-btn">
                <div class="ok-btn" @click="showTuWeiMap=false" style="padding: .05rem .8rem;">确定</div>
                <!-- <div class="ok-btn" @click="end_all_order()">批量结束订单</div> -->
            </div>
        </div>
    </x-dialog>
  </div>
</template>

<script>
// import { getOrderMyList } from '@/api/user.js'
import { Tab, TabItem, XDialog, Datetime } from "vux";
import cVehicle from "@/components/cVehicle";
import workSite from "@/components/workSite";
import { TuWeiInfo, EditTuWeiInfo } from "@/api/home.js";
import getformattedAddress from "@/map/index.js";
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
      showTuWeiMap: false,
      loading_tuwei: false,
      showTuWei: false,
      showScrollBox: false,
      showWorkSite: false,
      showWhere: false,
      index: 0,
      sed_index: 0,
      w_qeury: {
        page: 1,
        pagesize: 10,
        keyword: null //模糊搜索
      },
      role_code: localStorage.getItem("role_code"),
      wj_order_list: [],
      order_list: [],
      onFetching: false,
      noData: false,
      vehicle_list: [],
      work_list: [],
      driver_keyword: null,
      work_keyword: null,
      ruleForm: {
        cTuWeiMingCheng: null,
        cShouFeiFangShi: "免费",
        cTuWeiDiZhi: null,
        longitude: null,
        latitude: null
      },
      map: null
    };
  },
  computed: {
    route() {
      var role_code = localStorage.getItem("role_code");
      if (role_code == "001") {
        return "admin-user";
      } else {
        return "destination-user";
      }
    },
    select_map() {
      if (this.ruleForm.cTuWeiDiZhi) {
        return "删除地址";
      } else {
        return "选择地址";
      }
    }
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
    this.get_order();
  },
  mounted() {},
  methods: {
    getMapCenter() {
      var _this = this;
      if (this.ruleForm.cTuWeiDiZhi) {
        _this.ruleForm.cTuWeiDiZhi = null;
        _this.ruleForm.longitude = null;
        _this.ruleForm.latitude = null;
        return;
      }

      this.$vux.loading.show({
        text: "Loading"
      });
      getformattedAddress.then(res => {
        var lat = res.regeocode.crosses[0].location.lat;
        var lng = res.regeocode.crosses[0].location.lng;
        //土尾赋值
        _this.ruleForm.cTuWeiDiZhi = res.regeocode.formattedAddress;
        _this.ruleForm.longitude = lng;
        _this.ruleForm.latitude = lat;
        // 隐藏
        _this.$vux.loading.hide();
        // 创建地图实例
        _this.map = new AMap.Map("container", {
          zoom: 13,
          center: [lng, lat],
          resizeEnable: true
        });

        // 创建点覆盖物
        var marker = new AMap.Marker({
          position: new AMap.LngLat(lng, lat),
          icon:
            "http://a.amap.com/jsapi_demos/static/demo-center/icons/poi-marker-default.png"
        });
        _this.map.add(marker);

        _this.map.on("click", function(e) {
          lat = e.lnglat.lat;
          lng = e.lnglat.lng;
          _this.map.remove(marker);
          marker = new AMap.Marker({
            position: new AMap.LngLat(lng, lat),
            icon:
              "http://a.amap.com/jsapi_demos/static/demo-center/icons/poi-marker-default.png"
          });
          _this.map.add(marker);

          var geocoder = new AMap.Geocoder({
              radius: 1000,
              extensions: 'all'
          })
          geocoder.getAddress([lng, lat], function (status, result) {
              // alert(JSON.stringify(result))
              if (status === 'complete' && result.info === 'OK') {
                  _this.ruleForm.cTuWeiDiZhi = result.regeocode.formattedAddress;
              }
          })
        });

        _this.showTuWeiMap = true;
      });
    },
    getAddForm() {
      this.resetForm();
      this.showTuWei = true;
    },
    resetForm() {
      this.ruleForm.cTuWeiMingCheng = null;
      this.ruleForm.cTuWeiDiZhi = null;
    },
    add_tuwei() {
      if (this.loading_tuwei) {
        return;
      }
      if (!this.ruleForm.cTuWeiMingCheng) {
        this.$vux.alert.show({
          title: "提示",
          content: "请输入土尾名称"
        });
        return;
      }
      // if (!this.ruleForm.cTuWeiDiZhi) {
      //   this.$vux.alert.show({
      //     title: "提示",
      //     content: "请选择土尾地址"
      //   });
      //   return;
      // }
      this.loading_tuwei = true;
      EditTuWeiInfo(this.ruleForm)
        .then(res => {
          this.loading_tuwei = false;
          this.showTuWei = false;
          this.search_order();
          this.$vux.alert.show({
            title: "提示",
            content: "土尾新增成功"
          });
        })
        .catch(res => {
          this.loading_tuwei = false;
        });
    },
    search_order() {
      this.init_query();
      this.get_order();
    },
    //工程车订单列表
    get_order() {
      this.onFetching = true;
      TuWeiInfo(this.w_qeury).then(res => {
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
    },
    init_query() {
      this.order_list = [];
      this.onFetching = false;
      this.w_qeury.page = 1;
      this.$nextTick(() => {
        this.$refs.scrollerBottom.reset({ top: 0 });
      });
    },
    onScrollBottom() {
      if (this.onFetching) {
        // do nothing
      } else {
        this.onFetching = true;
        this.get_order();
      }
    }
  }
};
</script>
<style>
.group-content .weui-cells {
  font-size: 0.5rem;
}
.amap-icon img {
  width: 0.67rem;
  height: 0.91rem;
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
  position: relative;
  color: #f00;
  font-size: 0.5rem;
  .op-btn:nth-child(1) {
    position: absolute;
    right: 2rem;
    top: 0.08rem;
    display: block;
    // padding: 0 0.27rem;
    background: #fff;
    border: 1px solid #f00;
    height: 1.07rem;
    line-height: 1.07rem;
    width: 1.6rem;
    border-radius: 5px;
    text-align: center;
  }
  .op-btn:nth-child(2) {
    position: absolute;
    right: 0;
    top: 0.08rem;
    display: block;
    // padding: 0 0.27rem;
    background: #fff;
    border: 1px solid #f00;
    height: 1.07rem;
    line-height: 1.07rem;
    width: 1.6rem;
    border-radius: 5px;
    text-align: center;
  }
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
    border: 1px solid #f00;
    display: inline-block;
    padding: 0.053333rem 0.2rem;
    border-radius: 3px;
    margin: 0 0.266667rem;
  }
  .del-btn {
    background: #fff;
    color: #999;
    border: 1px solid #999;
    display: inline-block;
    padding: 0.053333rem 0.2rem;
    border-radius: 3px;
    margin: 0 0.266667rem;
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
.map-content {
  height: 400px;
  .map-search {
    height: 1.07rem;
    line-height: 1.07rem;
    .lbl {
      display: inline-block;
      width: 20%;
    }
    input {
      display: inline-block;
      width: 70%;
      height: 0.6rem;
      line-height: 0.6rem;
    }
  }
  #container {
    width: 100%;
    height: 300px;
  }
  .search-title {
    height: 1.07rem;
    line-height: 1.07rem;
    font-size: 0.43rem;
    border-bottom: 1px solid #ddd;
  }
}
</style>