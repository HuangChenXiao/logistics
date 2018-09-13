<template>
    <div :style="{height:fullHeight+'px'}" style="background:#fff">
        <n-header title="正在审核">
        </n-header>
        <div style="height:2.26rem"></div>
        <div class="ico-sus">
            <img src="../../assets/img/success.png" alt="">
        </div>
        <div class="msg">资料正在审核中，请耐心等待。。。</div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      fullHeight: document.documentElement.clientHeight
    }
  }
}
</script>

<style scoped lang="scss">
.ico-sus {
  text-align: center;
  img {
    width: 2.666667rem;
    height: 2.666667rem;
  }
}
.msg {
  text-align: center;
  font-size: 0.426667rem;
}
</style>