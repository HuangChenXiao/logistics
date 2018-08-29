<template>
  <div id="app">
    <loading :show="isLoading" :text="loadingText"></loading>
    <router-view></router-view>
  </div>
</template>

<script>
import { Loading } from 'vux'
import { mapState } from 'vuex'
export default {
  name: 'app',
  components: {
    Loading
  },
  computed: {
    ...mapState({
      isLoading: state => state.com.isLoading,
      loadingText: state => state.com.loadingText
    })
  }
}
</script>

<style lang="less">
@import '~vux/src/styles/reset.less';
</style>
