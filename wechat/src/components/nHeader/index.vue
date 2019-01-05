<template>
    <div>
        <header class="navbar">
            <div class="nav-wrap-left">
                <a class="react back" href="javascript:;" >
                    <!-- <span>{{title}}</span> -->

                </a>

            </div>
            <h1 class="nav-header">
                {{title}}
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
    route: {
      default: null
    }
  },
  methods: {
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
  height: 1.173333rem;
  line-height: 1.173333rem;
  color: #2b2b2b;
  background: #fff;
  border-bottom: 1px solid #e5e5e5;
  display: -webkit-box;
  display: -ms-flexbox;
}
.navbar .nav-wrap-left {
  position: relative;
  z-index: 10;
  height: 1.173333rem;
  line-height: 1.173333rem;
}
.navbar h1.nav-header,
.navbar .h1.nav-header {
  position: absolute;
  left: 0;
  width: 100%;
  text-align: center;
  display: block;
  font-size: .5rem;
  font-weight: 400;
  text-align: center;
  height: 1.173333rem;
  line-height: 1.173333rem;
  margin: 0;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
}
.navbar .nav-header {
  /* margin-left: 0.5rem !important; */
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
  font-size: .5rem;
}
.nav-wrap-right a {
  height: 100%;
  line-height: 1.173333rem;
  height: 1.173333rem;
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
  height: 1.173333rem;
  /* width: 0.45rem; */
  line-height: 1.173333rem;
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
</style>