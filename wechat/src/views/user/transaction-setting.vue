<template>
  <div>
    <u-header title="交易设置">
    </u-header>
    <div style="height:1.2rem"></div>
    <div class="pay-title">
      账户信息
    </div>
    <div class="item-1">
      <div class="row">
        <div class="lbl">姓名</div>
        <div class="ipt">
          <input type="text" placeholder="请输入姓名" v-model="bItem.name">
        </div>
      </div>
      <div class="row">
        <div class="lbl">{{bItem.paytype==1?'微信':'支付宝'}}账号</div>
        <div class="ipt">
          <input type="text" placeholder="请输入账号" v-model="bItem.account">
        </div>
      </div>
    </div>
    <div class="item-2 clearfix">
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess1" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_1" class="ico-close" @click.stop="delete_qrcode(10,qr_amount_1)"></i>
          <img v-if="qr_amount_1" :src="qr_amount_1" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>10元</span>
      </div>
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess2" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_2" class="ico-close" @click.stop="delete_qrcode(20,qr_amount_2)"></i>
          <img v-if="qr_amount_2" :src="qr_amount_2" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>20元</span>
      </div>
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess3" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_3" class="ico-close" @click.stop="delete_qrcode(50,qr_amount_3)"></i>
          <img v-if="qr_amount_3" :src="qr_amount_3" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>50元</span>
      </div>
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess4" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_4" class="ico-close" @click.stop="delete_qrcode(100,qr_amount_4)"></i>
          <img v-if="qr_amount_4" :src="qr_amount_4" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>100元</span>
      </div>
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess5" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_5" class="ico-close" @click.stop="delete_qrcode(200,qr_amount_5)"></i>
          <img v-if="qr_amount_5" :src="qr_amount_5" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>200元</span>
      </div>
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess6" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_6" class="ico-close" @click.stop="delete_qrcode(500,qr_amount_6)"></i>
          <img v-if="qr_amount_6" :src="qr_amount_6" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>500元</span>
      </div>
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess7" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_7" class="ico-close" @click.stop="delete_qrcode(1000,qr_amount_7)"></i>
          <img v-if="qr_amount_7" :src="qr_amount_7" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>1000元</span>
      </div>
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess8" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_8" class="ico-close" @click.stop="delete_qrcode(2000,qr_amount_8)"></i>
          <img v-if="qr_amount_8" :src="qr_amount_8" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>2000元</span>
      </div>
      <div class="row">
        <el-upload class="avatar-uploader" :action="baseurl+'gs/app/common/upload.doGs'" :show-file-list="false" :on-success="handleAvatarSuccess9" :before-upload="beforeAvatarUpload">
          <i v-if="qr_amount_9" class="ico-close" @click.stop="delete_qrcode(5000,qr_amount_9)"></i>
          <img v-if="qr_amount_9" :src="qr_amount_9" class="avatar">
          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
        </el-upload>
        <span>5000元</span>
      </div>
    </div>
    <div class="msg-title">
      请上传与金额相符的收款二维码
    </div>
    <div style="height:2rem"></div>
    <x-button class="pruchase-btn" :disabled="!is_validate" :class="{'sus-btn':is_validate}" @click.native="create()">
      提交
    </x-button>
  </div>
</template>

<script>
import { userQrcodelist, userAddqrcode, userDelqrcode } from '@/api/user.js'
export default {
  data() {
    return {
      qr_amount_1: null,
      qr_amount_2: null,
      qr_amount_3: null,
      qr_amount_4: null,
      qr_amount_5: null,
      qr_amount_6: null,
      qr_amount_7: null,
      qr_amount_8: null,
      qr_amount_9: null,
      bItem: {
        name: null,
        account: null,
        amount: null,
        qrcode: null,
        paytype: this.$route.query.paytype
      }
    }
  },
  computed: {
    is_validate() {
      if (this.bItem.name || this.bItem.account) {
        return true
      }
      return false
    }
  },
  created() {
    userQrcodelist({ paytype: this.bItem.paytype }).then(res => {
      if (res.data.length) {
        this.bItem.name = res.data[0].name
        this.bItem.account = res.data[0].account
        for (var i = 0; i < res.data.length; i++) {
          if (res.data[i].amount == 10) {
            this.qr_amount_1 = res.data[i].qrcode
          }
          if (res.data[i].amount == 20) {
            this.qr_amount_2 = res.data[i].qrcode
          }
          if (res.data[i].amount == 50) {
            this.qr_amount_3 = res.data[i].qrcode
          }
          if (res.data[i].amount == 100) {
            this.qr_amount_4 = res.data[i].qrcode
          }
          if (res.data[i].amount == 200) {
            this.qr_amount_5 = res.data[i].qrcode
          }
          if (res.data[i].amount == 500) {
            this.qr_amount_6 = res.data[i].qrcode
          }
          if (res.data[i].amount == 1000) {
            this.qr_amount_7 = res.data[i].qrcode
          }
          if (res.data[i].amount == 2000) {
            this.qr_amount_8 = res.data[i].qrcode
          }
          if (res.data[i].amount == 5000) {
            this.qr_amount_9 = res.data[i].qrcode
          }
        }
      }
    })
  },
  methods: {
    handleAvatarSuccess1(res, file) {
      this.qr_amount_1 = res.data[0].imgUrl + res.data[0].qrcode
    },
    handleAvatarSuccess2(res, file) {
      this.qr_amount_2 = res.data[0].imgUrl + res.data[0].qrcode
    },
    handleAvatarSuccess3(res, file) {
      this.qr_amount_3 = res.data[0].imgUrl + res.data[0].qrcode
    },
    handleAvatarSuccess4(res, file) {
      this.qr_amount_4 = res.data[0].imgUrl + res.data[0].qrcode
    },
    handleAvatarSuccess5(res, file) {
      this.qr_amount_5 = res.data[0].imgUrl + res.data[0].qrcode
    },
    handleAvatarSuccess6(res, file) {
      this.qr_amount_6 = res.data[0].imgUrl + res.data[0].qrcode
    },
    handleAvatarSuccess7(res, file) {
      this.qr_amount_7 = res.data[0].imgUrl + res.data[0].qrcode
    },
    handleAvatarSuccess8(res, file) {
      this.qr_amount_8 = res.data[0].imgUrl + res.data[0].qrcode
    },
    handleAvatarSuccess9(res, file) {
      this.qr_amount_9 = res.data[0].imgUrl + res.data[0].qrcode
    },
    beforeAvatarUpload(file) {
      const isJPG = file.type === 'image/jpeg'
      const isLt2M = file.size / 1024 / 1024 < 2

      // if (!isJPG) {
      //   this.$message.error('上传头像图片只能是 JPG 格式!')
      // }
      if (!isLt2M) {
        this.$message.error('上传头像图片大小不能超过 2MB!')
      }
      return isLt2M
    },
    delete_qrcode(amount, qrcode) {
      var _this = this
      this.$vux.confirm.show({
        title: '系统提示',
        content: '是否删除二维码',
        onConfirm() {
          userDelqrcode({
            amount: amount,
            qrcode: qrcode,
            paytype: _this.bItem.paytype
          })
            .then(res => {
              if (amount == 10) {
                _this.qr_amount_1 = null
              }
              if (amount == 20) {
                _this.qr_amount_2 = null
              }
              if (amount == 50) {
                _this.qr_amount_3 = null
              }
              if (amount == 100) {
                _this.qr_amount_4 = null
              }
              if (amount == 200) {
                _this.qr_amount_5 = null
              }
              if (amount == 500) {
                _this.qr_amount_6 = null
              }
              if (amount == 1000) {
                _this.qr_amount_7 = null
              }
              if (amount == 2000) {
                _this.qr_amount_8 = null
              }
              if (amount == 5000) {
                _this.qr_amount_9 = null
              }
              _this.$vux.toast.show({
                text: '删除成功'
              })
            })
            .catch(res => {
              _this.$vux.alert.show({
                title: '系统提示',
                content: res.data.message
              })
            })
        }
      })
    },
    create() {
      var datalist = []
      if (this.qr_amount_1) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 10
        data.qrcode = this.qr_amount_1
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      if (this.qr_amount_2) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 20
        data.qrcode = this.qr_amount_2
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      if (this.qr_amount_3) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 50
        data.qrcode = this.qr_amount_3
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      if (this.qr_amount_4) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 100
        data.qrcode = this.qr_amount_4
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      if (this.qr_amount_5) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 200
        data.qrcode = this.qr_amount_5
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      if (this.qr_amount_6) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 500
        data.qrcode = this.qr_amount_6
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      if (this.qr_amount_7) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 1000
        data.qrcode = this.qr_amount_7
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      if (this.qr_amount_8) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 2000
        data.qrcode = this.qr_amount_8
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      if (this.qr_amount_9) {
        var data = {}
        data.name = this.bItem.name
        data.account = this.bItem.account
        data.amount = 5000
        data.qrcode = this.qr_amount_9
        data.paytype = this.bItem.paytype
        datalist.push(data)
      }
      userAddqrcode(datalist)
        .then(res => {
          var _this = this
          this.$vux.toast.show({
            text: '操作成功'
          })
        })
        .catch(res => {
          this.$vux.alert.show({
            title: '系统提示',
            content: res.message
          })
        })
    }
  }
}
</script>

<style>
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  /* overflow: hidden; */
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 2.986667rem;
  height: 4rem;
  line-height: 4rem;
  text-align: center;
}
.avatar {
  width: 2.986667rem;
  height: 4rem;
  display: block;
}
</style>
<style scoped lang="scss">
.item-1 {
  background: #fff;
  font-size: .43rem;
  padding: 0 0.266667rem;
  .row {
    border-bottom: 1px solid #ededed;
    padding: .43rem 0;
    .lbl {
      position: relative;
      display: inline-block;
      width: 25%;
    }
    .ipt {
      display: inline-block;
      width: 70%;
      input {
        border: 0;
      }
    }
  }
  .row:last-child {
    border-bottom: 0;
  }
}
.pay-title {
  font-size: .43rem;
  padding: 0.426667rem 0 0.266667rem 0.266667rem;
  background: #f5f5f5;
}
.item-2 {
  background: #fff;
  margin-top: 0.266667rem;
  padding-top: 0.266667rem;
  .row {
    float: left;
    margin: 0.133333rem;
    text-align: center;
    width: 30%;
    height: 4.56rem;
  }
}
.msg-title {
  color: rgb(248, 143, 44);
  text-align: center;
  background: #fff;
  padding: 0.3rem 0;
}
.pruchase-btn {
  position: fixed;
  bottom: 0.266667rem;
  left: 5%;
  width: 90%;
  height: 1.173333rem;
  line-height: 1.173333rem;
  background: #faaf6b;
  border-radius: 1.173333rem;
  text-align: center;
  color: #fff;
  font-size: .43rem;
}
.pruchase-btn:disabled {
  background: #faaf6b;
  color: #fff;
}
.sus-btn {
  background: rgb(248, 143, 44) !important;
}
.ico-close {
  position: absolute;
  right: -0.2rem;
  top: -0.266667rem;
  width: 0.533333rem;
  height: 0.533333rem;
  background: url(../../assets/img/delete.png);
  background-size: 100%;
}
</style>