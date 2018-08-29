<template>
  <div>
    <transition name="slide-fade">
      <div class="mask" v-show="imgcode_mask" @click="imgcode_mask=false">
        <transition name="slide-up">
          <div class="mask-content" @click.stop v-show="imgcode_mask">
            <section>
              <div class="img-item clearfix">
                <div class="img-ipt">
                  <input type="text" placeholder="请输入验证码" v-model="input_code">
                </div>
                <div class="img" @click="change_imgCode()">
                  <img :src="ver_imgcode" />
                </div>
              </div>
              <div class="err-code">
                &nbsp;
              </div>
              <slot></slot>
            </section>
          </div>
        </transition>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  props: ['value', 'ver_imgcode'],
  data() {
    return {
      imgcode_mask: false,
      input_code: null
    }
  },
  created() {
    this.$store.dispatch('imgCode', null)
    this.imgcode_mask = this.value
  },
  methods:{
    change_imgCode(){
      this.$emit('getCode')
    }
  },
  watch: {
    value(val, oldVal) {
      this.imgcode_mask = this.value
    },
    imgcode_mask(val) {
      if (!val) {
        this.input_code = null
      } else {
        this.$emit('getCode')
      }
      this.$emit('input', val)
    },
    input_code(val, oldVal) {
      this.$store.dispatch('imgCode', val)
    }
  }
}
</script>

<style scoped lang="scss">
/* 可以设置不同的进入和离开动画 */
/* 设置持续时间和动画函数 */
.slide-fade-enter-active {
  transition: all 0.5s ease;
}
.slide-fade-leave-active {
  transition: all 0.5s;
}
.slide-fade-enter, .slide-fade-leave-to
/* .slide-fade-leave-active for below version 2.1.8 */ {
  opacity: 0;
}
.slide-up-enter-active {
  transition: all 0.4s ease;
}
.slide-up-leave-active {
  transition: all 0.3s ease;
}
.slide-up-enter, .slide-up-leave-to
/* .slide-fade-leave-active for below version 2.1.8 */ {
  transform: translateY(100%);
}

.mask {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.6);
  .mask-content {
    position: relative;
    font-size: 0.426667rem;
    width: 80%;
    margin: 0 auto;
    margin-top: 5rem;
    section {
      position: relative;
      background: #fff;
      padding: 0.533333rem;
    }
    .img-item {
      font-size: 0.4rem;
      margin: 0.4rem 0;
      .img-ipt {
        position: absolute;
        width: 55%;
        height: 0.8rem;
        line-height: 0.8rem;
        border-bottom: 1px solid #ededed;
        input {
          border: 0;
          padding-left: 0.133333rem;
        }
      }
      .img {
        position: absolute;
        right: 0.533333rem;
        width: 25%;
        img {
          width: 100%;
        }
      }
    }
  }
}
</style>