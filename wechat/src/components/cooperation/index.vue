<template>
    <div>
        <div class="title">选择合作客户</div>
        <div class="search">
            <div class="item">
                <div class="ico"></div>
                <div class="ipt">
                    <input type="text" v-model="search_val" placeholder="请输入要搜索的合作客户">
                </div>
            </div>
        </div>
        <div class="item-list" v-if="list.length>0">
            <div class="item" v-for="item in list">
                <div class="code">{{item.cXZDWMingCheng}}</div>
                <!-- <div class="color">颜色：{{item.color}}</div> -->
                <div class="opt-btn">
                    <div class="sed-btn" @click="select_vehicle(item)">
                        选择
                    </div>
                </div>
            </div>
        </div>
        <div class="no-data" v-else>
            暂无合作客户~
        </div>
    </div>
</template>

<script>
export default {
  props: ['value', 'valueData'],
  data() {
    return {
      list: null,
      search_val: null
    }
  },
  created() {
    this.list = this.valueData
    this.search_val = this.value
  },
  methods: {
    select_vehicle(item) {
      this.$emit('selectVehicle', item)
    }
  },
  watch: {
    valueData(val, oldVal) {
      this.list = val
    },
    value(val, oldVal) {
      this.search_val = val
    },
    search_val(val, oldVal) {
      this.$emit('input', val)
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
  padding-bottom: 0.266667rem;
  .item {
    position: relative;
    padding: 0.266667rem 0.533333rem;
    border-bottom: 1px solid #efefef;
    text-align: left;
    padding-right: 2rem;
    .opt-btn {
      position: absolute;
      top: 0;
      right: 0;
      width: 2.666667rem;
      height: 100%;
      .sed-btn {
        position: absolute;
        top: 0.2rem;
        right: 0.533333rem;
        border: 1px solid #f00;
        color: #f00;
        border-radius: 5px;
        padding: 0.05rem 0.266667rem;
      }
    }
  }
}
.no-data {
  background: #fff;
  text-align: center;
  padding: 0.266667rem 0.8rem;
  font-size: 0.373333rem;
}
.search {
  position: relative;
  padding: 0.266667rem;
  border-bottom: 1px solid #ededed;
  .item {
    background: #ededed;
    padding-left: 1.066667rem;
    padding-right: 0.533333rem;
    border-radius: 3px;
  }
  .ico {
    position: absolute;
    left: 0.5rem;
    top: 0.4rem;
    width: 0.54rem;
    height: 0.54rem;
    background: url(../../assets/img/search.png);
    background-size: 100%;
  }
  .ipt {
    height: 0.8rem;
    line-height: 0.8rem;
    input {
      padding: 0.053333rem 0;
      border: 0;
      padding-left: 0.266667rem;
    }
  }
}
</style>