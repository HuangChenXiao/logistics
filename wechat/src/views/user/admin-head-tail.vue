<template>
    <div>
        <u-header title="订单统计数">
        </u-header>
    <div style="height:1.2rem"></div>
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
                  <div class="lbl">工地</div>
                  <div class="info">
                    <!-- <input type="text" placeholder="模糊搜索" v-model="bItem.cGongDiMingCheng" class="g-ipt"> -->
                    <div class="btn-select" @click="showWorkSite=true">
                      选择
                    </div>
                    <span>{{bItem.cGongDiMingCheng}}</span>
                    <div class="ico-clear" v-if="bItem.cGongDiMingCheng" @click="clear_gongdi()"></div>
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">土尾</div>
                  <div class="info">
                    <!-- <input type="text" placeholder="模糊搜索" v-model="bItem.cTuWeiMingCheng" class="g-ipt"> -->
                    <div class="btn-select" @click="showTuwei=true">
                      选择
                    </div>
                    <span>{{bItem.cTuWeiMingCheng}}</span>
                    <div class="ico-clear" v-if="bItem.cTuWeiMingCheng" @click="clearTuiwei"></div>
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">驾驶员</div>
                  <div class="info">
                    <input type="text" placeholder="模糊搜索" v-model="bItem.cXingMing" class="g-ipt">
                  </div>
                </div>
                <div class="group-item">
                  <div class="lbl">车牌号</div>
                  <div class="info">
                    <input type="text" placeholder="模糊搜索" v-model="bItem.cChePaiHao" class="g-ipt">
                  </div>
                </div>
                <!-- <selector title="合作单位"  v-model="value2"></selector>
                                <selector title="车辆" :options="['闽G123', '其他']" v-model="value2"></selector>
                                <selector title="路线" :options="['高新技术园-软件园', '五缘湾-火车站']" v-model="value2"></selector> -->
                                
                <datetime title="开始日期" v-model="bItem.BegindDanJuRiQi" format="YYYY-MM-DD" value-text-align="left"></datetime>
               <datetime title="结束日期" v-model="bItem.EnddDanJuRiQi" format="YYYY-MM-DD" value-text-align="left"></datetime>
                <div class="wk-btn">
                  <!-- <div class="ok-btn">发布订单</div> -->
                  <x-button class="ok-btn" :class="{'btn-success':validate_order}" type="primary" :disabled="!validate_order" @click.native="getOrderCount"> 查询订单</x-button>
                </div>
              </group>
         <!-- <div class="od-tb">
            <div class="head flex f-d-r">
                <div class="th">工地</div>
                <div class="th">土尾</div>
            </div>
            <div class="body flex f-d-r">
                <div class="td">{{bItem.cGongDiMingCheng}}</div>
                <div class="td">{{bItem.cTuWeiMingCheng}}</div>
            </div>
        </div> -->
        <div class="od-tb">
            <div class="head flex f-d-r">
                <div class="th">序号</div>
                <div class="th">工地</div>
                <div class="th">土尾</div>
                <div class="th">车数</div>
            </div>
            <div class="body flex f-d-r" v-for="item in list">
                <div class="td f-w600">{{item.RowNo}}</div>
                <div class="td f-w600">{{item.cGongDiMingCheng}}</div>
                <div class="td f-w600">{{item.cTuWeiMingCheng}}</div>
                <div class="td f-w600">{{item.iNumber}}</div>
            </div>
            <div v-if="list.length<=0" style="text-align:center">暂无数据</div>
        </div>
        
    <!-- 工地信息 -->
    <x-dialog v-model="showWorkSite" class="dialog-demo">
      <work-site @selectVehicle="selectWork" :hide-on-blur="true"  :valueData="work_list" v-model="work_keyword"></work-site>
    </x-dialog>
    <!-- 土尾信息 -->
    <x-dialog v-model="showTuwei" class="dialog-demo">
      <c-tuwei @selectVehicle="selectTuwei" :hide-on-blur="true"  :valueData="tuwei_list" v-model="tuwei_keyword"></c-tuwei>
    </x-dialog>
    </div>
    
</template>

<script>
import { Datetime } from "vux";
import { HeadTailOrderCount } from "@/api/home.js";
import { GongDiInfo, TuWeiInfo } from "@/api/home.js";
import workSite from "@/components/workSite";
import cTuwei from "@/components/cTuwei";
import { XDialog } from "vux";
export default {
  components: {
    Datetime,
    XDialog,
    workSite,
    cTuwei
  },
  data() {
    return {
      showWorkSite: false,
      showTuwei: false,
      total: 0,
      list:[],
      work_list: [],
      work_keyword: null,
      tuwei_list: [],
      tuwei_keyword: null,
      role_code: localStorage.getItem("role_code"),
      bItem: {
        cGongDiMingCheng: null, //工地名称
        cTuWeiMingCheng: null, //土尾名称
        cXZDWMingCheng: null, //协作单位
        cXingMing: null, //驾驶员
        cChePaiHao: null, //车牌号
        BegindDanJuRiQi: this.cfg.formatDate(),
        EnddDanJuRiQi: this.cfg.formatDate(),
        cGuanLiYuanBianMa: null //现场管理员编码
      }
    };
  },
  computed: {
    validate_order() {
      if (this.bItem.BegindDanJuRiQi && this.bItem.EnddDanJuRiQi) {
        return true;
      }
      return false;
    }
  },
  created() {
    //现场管理员
    if (this.role_code == "001") {
      this.bItem.cGuanLiYuanBianMa = localStorage.getItem("openid");
    }
    //老板
    else if (this.role_code == "005") {
      
    } else {
      this.$vux.alert.show({
        title: "提示",
        content: "网络异常，请重新登录！"
      });
      window.history.go(-1);
    }
    this.get_worksite();
    this.get_tuwei();
    this.getOrderCount();
  },
  methods: {
    clearTuiwei() {
      this.bItem.cTuWeiBianMa = null;
      this.bItem.cTuWeiMingCheng = null;
    },
    clear_gongdi() {
      this.bItem.cGongDiBianMa = null;
      this.bItem.cGongDiMingCheng = null;
    },
    //土尾列表
    get_tuwei() {
      TuWeiInfo({ keyword: this.tuwei_keyword }).then(res => {
        this.tuwei_list = res.data;
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
      this.bItem.cGongDiBianMa = val.cGongDiBianMa;
      this.bItem.cGongDiMingCheng = val.cGongDiMingCheng;
      this.showWorkSite = false;
    },
    //土尾名称
    selectTuwei(val) {
      this.bItem.cTuWeiBianMa = val.cTuWeiBianMa;
      this.bItem.cTuWeiMingCheng = val.cTuWeiMingCheng;
      this.showTuwei = false;
    },
    getOrderCount() {
      // var data = {};
      // data.cTuWeiBianMa = this.bItem.cTuWeiBianMa;
      // data.cGongDiBianMa = this.bItem.cGongDiBianMa;
      // data.BegindDanJuRiQi = this.bItem.BegindDanJuRiQi;
      // data.EnddDanJuRiQi = this.bItem.EnddDanJuRiQi;
      HeadTailOrderCount(this.bItem).then(res => {
        this.list = res.data;
        // this.$vux.alert.show({
        //   title: "提示",
        //   content: "查询成功"
        // });
      });
    }
  },
  watch: {
    work_keyword(val, oldVal) {
      //工地列表
      this.get_worksite();
    },
    tuwei_keyword(val, oldVal) {
      //工地列表
      this.get_tuwei();
    }
  }
};
</script>

<style scoped lang="scss">
.od-tb {
  margin-top: 0.266667rem;
  font-size: 0.5rem;
  .head {
    background: #fff;
    .th {
      text-align: center;
      border: 1px solid #ededed;
      width: 100%;
      height: 1.066667rem;
      line-height: 1.066667rem;
      margin: -1px -1px 0 0;
      font-weight: 500;
    }
  }
  .body {
    background: #fff;
    .td {
      text-align: center;
      border: 1px solid #ededed;
      width: 100%;
      height: 1.066667rem;
      line-height: 1.066667rem;
      margin: -1px -1px 0 0;
    }
  }
  .f-w600 {
    font-size: 0.426667rem;
    font-weight: 600;
  }
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
    background: #ddd;
    color: #fff;
    display: inline-block;
    border-radius: 3px;
    font-size: 0.426667rem;
    width: 80%;
  }
}
.group-item {
  position: relative;
  height: 1.066667rem;
  line-height: 1.066667rem;
  padding-left: 2.3rem;
  font-size: 0.5rem;
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
    .g-ipt {
      width: 90%;
      border: 1px solid #ddd;
      padding: 0.05rem 0.13rem;
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
</style>