<template>
    <div>
        <u-header title="桌面APP">
        </u-header>
        <div style="height:1.2rem"></div>
        <tab active-color="#FFB549" bar-active-color="#FFB549">
            <tab-item selected @on-item-click="onItemClick">苹果</tab-item>
            <tab-item @on-item-click="onItemClick">安卓</tab-item>
        </tab>
        <div class="doc-list" v-if="index==0">
            <div class="doc-title">苹果桌面APP-制作教程</div>
            <section>
                <div class="doc-subtitle">Step1:用手机浏览器打开
                    <a href="http://ylf.chaomafu.com/#/">http://ylf.chaomafu.com/#/</a> （在微信里打开该链接后，可以点击右上角的三个小点点“…”，再选择用“在Safari中打开”即可）
                </div>
                <div class="doc-img">
                    <img src="../../assets/img/ios/ios_1.jpg" alt="">
                </div>
                <div class="doc-img">
                    <img src="../../assets/img/ios/ios_2.jpg" alt="">
                </div>
            </section>
            <section>
                <div class="doc-subtitle">Step2：在Safari中打开后，点击页面下部的分享按钮（一般为底部第一个按钮），在弹出的页面里，选择第二行的第五个图标“添加到主屏幕”。
                </div>
                <div class="doc-img">
                    <img src="../../assets/img/ios/ios_3.jpg" alt="">
                </div>
                <div class="doc-img">
                    <img src="../../assets/img/ios/ios_4.jpg" alt="">
                </div>
                <div class="doc-img">
                    <img src="../../assets/img/ios/ios_5.jpg" alt="">
                </div>
            </section>
            <section>
                <div class="doc-subtitle">Step3：手机返回到主屏幕，就能看到了。
                </div>
                <div class="doc-img">
                    <img src="../../assets/img/ios/ios_6.jpg" alt="">
                </div>
            </section>
        </div>
    </div>
</template>
<script>
import { Tab, TabItem } from 'vux'
export default {
  components: {
    Tab,
    TabItem
  },
  data() {
    return {
      index: 0
    }
  },
  methods: {
    onItemClick(index) {
      this.index = index
    }
  }
}
</script>

<style scoped lang="scss">
.doc-list {
  font-size: .5rem;
  background: #fff;
  padding: 0.266667rem;
  .doc-title {
    text-align: center;
    font-size: 0.666667rem;
    font-weight: 600;
  }
  .doc-subtitle {
    margin: 0.4rem 0;
    a {
      color: blue;
      text-decoration: underline;
      display: inline;
    }
  }
}
</style>