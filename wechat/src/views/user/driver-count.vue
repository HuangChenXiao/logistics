<template>
    <div>
        <u-header title="订单统计数">
        </u-header>
    <div style="height:1.2rem"></div>
          <group label-width="4.5em" label-margin-right="2em" label-align="right" class="group-content">
          <datetime title="开始日期" v-model="w_qeury.BegindDanJuRiQi" format="YYYY-MM-DD" value-text-align="left"></datetime>
          <datetime title="结束日期" v-model="w_qeury.EnddDanJuRiQi" format="YYYY-MM-DD" value-text-align="left"></datetime>
          <div class="wk-btn">
            <x-button class="ok-btn" :class="{'btn-success':validate_order}" type="primary" :disabled="!validate_order" @click.native="getOrderCount"> 查询订单</x-button>
          </div>
        </group>
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
    </div>
    
</template>

<script>
import { Datetime } from "vux";
import { OrderNumberCount } from "@/api/driver.js";
export default {
  components: {
    Datetime
  },
  data() {
    return {
      w_qeury: {
        BegindDanJuRiQi: this.cfg.formatDate(),
        EnddDanJuRiQi: this.cfg.formatDate()
      },
      list:[],
      total: 0
    };
  },
  computed: {
    validate_order() {
      if (this.w_qeury.BegindDanJuRiQi && this.w_qeury.EnddDanJuRiQi) {
        return true;
      }
      return false;
    }
  },
  created() {
  },
  methods: {
    getOrderCount() {
      OrderNumberCount(this.w_qeury).then(res => {
        this.list = res.data;
      });
    }
  }
};
</script>

<style scoped lang="scss">
.group-content .weui-cell {
  font-size: .5rem !important;
}
.od-tb {
  margin-top: 0.266667rem;
  font-size: .5rem;
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
</style>