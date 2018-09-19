<template>
    <div>
        <div class="mask">
            <div class="container">
                <n-header title="选择工地">
                </n-header>
                <div style="height:1.26rem"></div>
                <div class="search">
                    <div class="search-item">
                        <div class="ipt">
                            <input type="text" placeholder="请输入工地名称">
                        </div>
                        <div class="btn-search">搜索</div>
                    </div>
                    <div class="search-content">
                        <div class="item" v-for="item in 6">
                            <div class="t1">厦门市集美区杏林湾工地1号</div>
                            <div class="t-btn" @click="selectWork">选择</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {}
  },
  methods:{
      selectWork(){
          this.$emit('selectWork',false)
      }
  }
}

</script>

<style scoped lang="scss">
.mask {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background: #f4f4f4;
  z-index: 1000;
}
.container {
  background: #fff;
  font-size: 0.373333rem;
  .search {
    .search-item {
      position: relative;
      padding: 0.266667rem;
      padding-right: 2.3rem;
      .ipt {
        input {
          background: #f4f4f4;
          border: 0;
          height: 0.8rem;
          line-height: 0.8rem;
          border-radius: 3px;
          padding: 0 0.266667rem;
        }
      }
      .btn-search {
        position: absolute;
        right: 0.266667rem;
        top: 0.23rem;
        background: #f00;
        color: #fff;
        border-radius: 3px;
        height: 0.8rem;
        line-height: 0.8rem;
        padding: 0 .266667rem;
      }
    }
    .search-content{
        border-top: 1px solid #efefef;
        padding-left: .266667rem;
        .item{
            position: relative;
            border-bottom: 1px solid #efefef;
            padding: .266667rem .266667rem .266667rem 0;
            .t1{
                
            }
            .t-btn{
                position: absolute;
                right: .266667rem;
                top: .23rem;
                color: #f00;
                border:1px solid #f00;
                border-radius: 3px;
                padding: 0 .133333rem;
            }
        }
    }
  }
}
</style>