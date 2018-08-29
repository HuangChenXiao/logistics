<template>
    <div :style="{height:fullHeight+'px'}" style="background:#fff">
        <u-header title="帮助中心">
        </u-header>
        <div style="height:1.2rem"></div>
        <div class="gs-btn">
            <div class="exit-btn" @click="exit()">
                退出登录
            </div>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      fullHeight: document.documentElement.clientHeight
    }
  },
  methods:{
      exit(){
          localStorage.clear();
          this.$router.push({
              path:'/login'
          })
      }
  }
}
</script>

<style scoped lang="scss">
.gs-btn {
  .exit-btn {
    position: absolute;
    left: 5%;
    bottom: .4rem;
    font-size: 0.373333rem;
    height: 1.066667rem;
    line-height: 1.066667rem;
    width: 90%;
    border: 1px solid #999;
    color: #999;
    text-align: center;
    border-radius: 4px;
    background: #fff;
  }
}
</style>