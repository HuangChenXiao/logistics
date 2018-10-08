<template>
  <div>
    <n-header title="资料完善">
    </n-header>
    <div class="audit-content">
      <div style="height:2.26rem"></div>
      <div class="item-list">
        <div class="item">
          <div class="lbl">姓名</div>
          <div class="ipt"><input type="text" placeholder="请输入姓名" v-model="user.cXingMing"></div>
        </div>
        <div class="item">
          <div class="lbl">手机号</div>
          <div class="ipt"><input type="text" placeholder="请输入手机号" v-model="user.cLianXiDianHua"></div>
        </div>
      </div>
      <div class="ibtn">
        <div class="ibtn-default" @click="submit_info">提交</div>
      </div>
    </div>
  </div>
</template>

<script>
import { validatePhone } from '@/utils/validate.js'
import { WeChatAccredit } from '@/api/home.js'
export default {
  data() {
    return {
      user: {
        cXingMing: null,
        cLianXiDianHua: null,
        openid: null,
        nickname: null,
        sex: null,
        province: null,
        city: null,
        country: null,
        headimgurl: null
      }
    }
  },
  created() {
    var Request = this.GetRequest()
    var openid = localStorage.getItem('openid')
    if (openid) {
      var res = localStorage.setItem('user_info', JSON.parse(res))
      this.user.openid = res.openid
      this.user.nickname = res.nickname
      this.user.sex = res.sex
      this.user.province = res.province
      this.user.city = res.city
      this.user.country = res.country
      this.user.headimgurl = res.headimgurl
    } else {
      WeChatAccredit({ code: Request.code }).then(res => {
        localStorage.setItem('openid', res.openid)
        localStorage.setItem('user_info', JSON.stringify(res))
        this.user.openid = res.openid
        this.user.nickname = res.nickname
        this.user.sex = res.sex
        this.user.province = res.province
        this.user.city = res.city
        this.user.country = res.country
        this.user.headimgurl = res.headimgurl
      })
    }
  },
  methods: {
    GetRequest() {
      var url = location.search //获取url中"?"符后的字串
      var theRequest = new Object()
      if (url.indexOf('?') != -1) {
        var str = url.substr(1)
        var strs = str.split('&')
        for (var i = 0; i < strs.length; i++) {
          theRequest[strs[i].split('=')[0]] = strs[i].split('=')[1]
        }
      }
      return theRequest
    },
    submit_info() {
      // if(){

      // }
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