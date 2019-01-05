<template>
    <div :style="{height:fullHeight+'px'}" style="background:#fff">
        <div class="logo">
            <img src="../../assets/img/logo.png" alt="">
        </div>
        <div class="item-list">
            <div class="item">
                <i class="ico ico-phone"></i>
                <div class="ipt">
                    <input type="text" placeholder="请输入手机号" v-model="bItem.mobile">
                </div>
            </div>
            <div class="item">
                <i class="ico ico-password"></i>
                <div class="ipt">
                    <input type="password" placeholder="请输入密码" v-model="bItem.password">
                </div>
            </div>
        </div>
        <div style="height:1rem"></div>
        <x-button class="pruchase-btn" :disabled="!is_validate" :class="{'sus-btn':is_validate}" @click.native="login()">登录</x-button>

    </div>
</template>

<script>
import { login } from '@/api/login.js'
export default {
  data() {
    return {
      fullHeight: document.documentElement.clientHeight,
      bItem: {
        mobile: null,
        password: null
      }
    }
  },
  computed: {
    is_validate() {
      if (!this.bItem.mobile || !this.bItem.password) {
        return false
      }
      return true
    }
  },
  mounted() {},
  methods: {
    login() {
      this.bItem.password = this.$md5(this.bItem.password).toUpperCase()
      //   login(this.bItem)
      //     .then(res => {

      //     })
      //     .catch(res => {
      //       this.bItem.password = null
      //       this.$vux.alert.show({
      //         title: '系统提示',
      //         content: res.message
      //       })
      //     })
      this.$store.dispatch('Login', this.bItem).then(() => {
          this.loading = false
          this.$router.push({ path: '/' })
        })
        .catch((res) => {
          this.loading = false
          this.$vux.alert.show({
            title: '系统提示',
            content: res.message
          })
        })
    }
  }
}
</script>

<style scoped lang="scss">
.logo {
  padding-top: 1.333333rem;
  text-align: center;
  img {
    width: 60%;
  }
}
.item-list {
  padding: 0.4rem 0.533333rem;
  .item {
    position: relative;
    padding-left: 0.8rem;
    height: 1.2rem;
    line-height: 1.2rem;
    border-bottom: 1px solid #ededed;
    .ico {
      position: absolute;
      left: 0;
      top: 0.22rem;
      display: block;
      width: 0.48rem;
      height: 0.64rem;
    }
    .ico-phone {
      background: url(../../assets/img/p_2.png);
      background-size: 100%;
    }
    .ico-password {
      background: url(../../assets/img/p_1.png);
      background-size: 100%;
    }
    .ipt {
      height: 100%;
      input {
        height: 80%;
        border: 0;
      }
    }
  }
}
.pruchase-btn {
  //   position: fixed;
  bottom: 0.266667rem;
  //   left: 5%;
  width: 90%;
  height: 1.173333rem;
  line-height: 1.173333rem;
  background: #faaf6b;
  border-radius: 1.173333rem;
  text-align: center;
  color: #fff !important;
  font-size: .5rem;
}
.pruchase-btn:disabled {
  background: #faaf6b;
  color: #fff;
}
</style>