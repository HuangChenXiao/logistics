<template>
    <div>
        <div class="title">当前车辆信息</div>
        <div class="item-list" v-if="list.length>0">
            <div class="item" v-for="item in list">
                <div class="code">车牌：{{item.code}}</div>
                <div class="color">颜色：{{item.color}}</div>
                <div class="opt-btn">
                    <div class="sed-btn" @click="select_vehicle(item)">
                        选择
                    </div>
                </div>
            </div>
        </div>
        <div class="no-data" v-else>
            暂无可用车辆~
        </div>
    </div>
</template>

<script>
export default {
  props: ['value'],
  data() {
    return {
      list: null
    }
  },
  created() {
    this.list = this.value
  },
  methods: {
    select_vehicle(item) {
      this.$emit('selectVehicle', item)
    }
  }
}
</script>

<style scoped lang="scss">
// .item-list::before{
//     position:absolute;
//     right: 0;
//     top: 0;
//     display: block;
//     content: ' ';
//     width: .533333rem;
//     height: .533333rem;
//     background:url(../../assets/img/delete.png) no-repeat;
//     background-size: 100%;
// }
.title {
  font-size: 0.373333rem;
  height: 1.066667rem;
  line-height: 1.066667rem;
  padding: 0 0.266667rem;
  border-bottom: 1px solid #efefef;
  font-weight: 600;
}
.item-list {
  position: relative;
  font-size: 0.373333rem;
  max-height: 10rem;
  overflow: auto;
  .item {
    position: relative;
    padding: 0.266667rem 0.533333rem;
    border-bottom: 1px solid #efefef;
    text-align: left;
    .opt-btn {
      position: absolute;
      top: 0;
      right: 0;
      width: 2.666667rem;
      height: 100%;
      .sed-btn {
        position: absolute;
        top: 0.4rem;
        right: 0.533333rem;
        border: 1px solid #f00;
        color: #f00;
        border-radius: 5px;
        padding: 0.133333rem 0.266667rem;
      }
    }
  }
}
.no-data {
  background: #fff;
  text-align: center;
  padding: 0.266667rem 0.8rem;
  font-size: .373333rem;
}
</style>