<template>
    <div>
        <n-header title="资料完善">
        </n-header>
        <div class="audit-content">
            <div style="height:2.26rem"></div>
            <div class="item-list">
                <div class="item">
                    <div class="lbl">姓名</div>
                    <div class="ipt"><input type="text" placeholder="请输入姓名"></div>
                </div>
                <div class="item">
                    <div class="lbl">手机号</div>
                    <div class="ipt"><input type="text" placeholder="请输入手机号"></div>
                </div>
            </div>
            <div class="ibtn">
                <div class="ibtn-default" @click="submit_info">提交</div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
  methods: {
    submit_info() {
      var _this = this
      this.$vux.confirm.show({
        title: '提示',
        content: '请认真填写后提交，是否继续？',
        onConfirm() {
          _this.$router.push({
            name: 'means'
          })
        }
      })
    }
  }
}
</script>

<style scoped lang="scss">
.audit-content {
  font-size: 0.373333rem;
  background: #fff;
}
.item-list {
  padding: 0.266667rem;
  .item {
    position: relative;
    padding: 0.266667rem;
    padding-left: 1.8rem;
    border-bottom: 1px solid #ededed;
    .lbl {
      position: absolute;
      left: 0;
      text-align: right;
    }
    .ipt {
      input {
        border: 0;
      }
    }
  }
  padding-bottom: 0.533333rem;
}
.ibtn {
  padding: 0.266667rem 0.266667rem 1rem 0.266667rem;
  .ibtn-default {
    width: 100%;
    background: #f00;
    color: #fff;
    text-align: center;
    padding: 0.266667rem 0;
    border-radius: 5px;
  }
}
</style>