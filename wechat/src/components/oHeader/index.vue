<template>
    <div>
        <header class="navbar">
            <div class="nav-wrap-left">
                <a class="react back" href="javascript:;" @click="back"><img class="return-icon" src="../../assets/img/back_gray.png">
                    <!-- <span>{{title}}</span> -->

                </a>

            </div>
            <h1 class="nav-header">
                <div class="title-btn-list clearfix" v-if="is_order">
                    <div class="title-btn" :class="{'sed-btn':type==1}" @click="orderBuyType(1)">卖单</div>
                    <div class="title-btn" :class="{'sed-btn':type==2}" @click="orderBuyType(2)">买单</div>
                </div>
                <div v-else>
                    {{title}}
                </div>
            </h1>
            <div class="nav-wrap-right">
                <slot></slot>
            </div>
        </header>
    </div>
</template>

<script>
export default {
  props: {
    title: {
      // 标题
      default: '标题'
    },
    is_order: {
      default: false
    },
    route: {
      default: null
    }
  },
  data() {
    return {
      type: 1
    }
  },
  methods: {
    orderBuyType(type) {
      this.type = type
      this.$emit('orderBuyType', type)
    },
    back() {
      if (this.route) {
        this.$router.push({
          name: this.route
        })
      } else {
        window.history.go(-1)
      }
    }
  }
}
</script>

<style scoped>
.navbar {
  position: fixed;
  top: 0;
  z-index: 999;
  width: 100%;
  height: 1.226667rem;
  line-height: 1.226667rem;
  color: #2b2b2b;
  background: #fff;
  border-bottom: 1px solid #e5e5e5;
  display: -webkit-box;
  display: -ms-flexbox;
}
.navbar .nav-wrap-left {
  position: relative;
  z-index: 10;
  height: 1.226667rem;
  line-height: 1.226667rem;
}
.navbar h1.nav-header,
.navbar .h1.nav-header {
  position: absolute;
  left: 0;
  width: 100%;
  text-align: center;
  display: block;
  font-size: 0.46rem;
  font-weight: 400;
  text-align: center;
  height: 1.226667rem;
  line-height: 1.226667rem;
  margin: 0;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
}
.navbar .nav-wrap-right {
  position: absolute;
  right: 0;
  margin-right: 0.4rem;
}
.navbar .nav-wrap-right {
  height: 100%;
}
.nav-wrap-right a:last-child {
  margin-right: 0.04rem;
}
div.nav-wrap-right a {
  width: auto;
}
.nav-wrap-right a {
  display: block;
  height: 100%;
  line-height: 1rem;
  text-align: center;
  width: 0.94rem;
  color: #5a5a5a;
  font-size: 0.373333rem;
}
.nav-wrap-right a {
  height: 100%;
  line-height: 1.226667rem;
  height: 1.226667rem;
  text-align: center;
  width: 0.94rem;
}
.return-icon {
  height: 0.47rem;
  width: auto;
  margin-left: 0.2rem;
}
img {
  vertical-align: middle;
  border: 0;
}
.nav-wrap-left a.back {
  font-size: 0.426667rem;
  height: 1.226667rem;
  /* width: 0.45rem; */
  line-height: 1.226667rem;
  padding: 0 0.3rem;
}
.nav-wrap-left a.back span {
  position: absolute;
  top: 0.053333rem;
  padding-left: 10px;
}
a.react,
label.react {
  display: block;
  color: inherit;
  height: 100%;
  overflow-x: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
.title-btn-list {
  display: inline-block;
  font-size: 0.4rem;
  width: 3.306667rem;
  border: 1px solid rgb(240, 123, 20);
  color: rgb(240, 123, 20);
  border-radius: 5px;
  margin-top: 0.186667rem;
}
.title-btn-list .title-btn {
  float: left;
  height: 0.8rem;
  line-height: 0.8rem;
  width: 50%;
}
.sed-btn {
  background: rgb(240, 123, 20);
  color: #fff;
}
</style>