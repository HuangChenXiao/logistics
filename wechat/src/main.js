// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import FastClick from 'fastclick'
import router from './router'
import App from './App'
import store from '@/store'

//公共样式
import "@/assets/css/flex.css"
import "@/assets/css/reset.css"
import "@/assets/css/common.css"

//动画
import "@/assets/css/animate.css"

//公共js
import "@/assets/js/flexible.js"
import cfg from '@/config/index.js'
Vue.prototype.cfg = cfg
import md5 from 'js-md5';
Vue.prototype.$md5 = md5;

// import "@/assets/css/test.css"


FastClick.attach(document.body)

Vue.config.productionTip = false

import VueLazyload from 'vue-lazyload'
Vue.use(VueLazyload, {
  error: './static/img/zwtp.png',
  loading: './static/img/zwtp.png'
})

//全局注册组件
import nHeader from '@/components/nHeader'
Vue.component('n-header', nHeader)
import uHeader from '@/components/uHeader'
Vue.component('u-header', uHeader)
import cBottom from '@/components/cBottom'
Vue.component('c-bottom', cBottom)

//vux 组件

import { Scroller, LoadMore, XButton, Rater, XInput, XAddress, Selector, XSwitch, } from 'vux'
Vue.component('scroller', Scroller)
Vue.component('load-more', LoadMore)
Vue.component('x-button', XButton)
Vue.component('rater', Rater)
Vue.component('x-input', XInput)
Vue.component('x-address', XAddress)
Vue.component('selector', Selector)
Vue.component('group', Group)
Vue.component('x-switch', XSwitch)

import { Group, Cell, DatetimePlugin, CloseDialogsPlugin, ConfigPlugin, BusPlugin, LocalePlugin, DevicePlugin, ToastPlugin, AlertPlugin, ConfirmPlugin, LoadingPlugin, WechatPlugin, AjaxPlugin, AppPlugin } from 'vux'
// plugins
Vue.use(DevicePlugin)
Vue.use(ToastPlugin)
Vue.use(AlertPlugin)
Vue.use(ConfirmPlugin)
Vue.use(LoadingPlugin)
Vue.use(WechatPlugin)
Vue.use(AjaxPlugin)
Vue.use(BusPlugin)
Vue.use(DatetimePlugin)

Vue.prototype.baseurl = process.env.BASE_API;

import { AlertModule } from 'vux'
router.beforeEach((to, from, next) => {
  var openid = localStorage.getItem("openid");
  if (!openid) {
    if (to.name != 'audit') {
      location.href = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx42cd9994ca8711a5&redirect_uri=http%3a%2f%2ftest.chaomafu.com%2faudit&response_type=code&scope=snsapi_userinfo&state=1#wechat_redirect";
    }
    else {
      next()
    }
  }
  else {
    getUserInfo(to, from, next);
  }
})

function getUserInfo(to, from, next) {
  store.dispatch('GetInfo').then(res => { // 拉取user_info
    if (res.data.audit == 0 && to.name != 'means') {
      next({ name: 'means' })
    }
    else {
      switch (res.data.role_code) {
        case '001'://现场管理员
          if (to.name != 'admin-index') {
            next({ name: 'admin-index' })
          }
          break;
        case '002'://驾驶员

          break;
        case '003'://土尾管理员

          break;
        case '004'://部门经理

          break;
        case '005'://老板

          break;
      }
      next()
    }
    next()
  })
    .catch((res) => {
      location.href = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx42cd9994ca8711a5&redirect_uri=http%3a%2f%2ftest.chaomafu.com%2faudit&response_type=code&scope=snsapi_userinfo&state=1#wechat_redirect";
    })
}

//全局过滤器
import * as custom from '@/utils/filter'

Object.keys(custom).forEach(key => {
  Vue.filter(key, custom[key])
})


import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
Vue.use(ElementUI);

/* eslint-disable no-new */
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app-box')
