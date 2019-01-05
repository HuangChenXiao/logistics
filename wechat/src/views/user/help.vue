<template>
    <div>
        <u-header title="帮助中心">
        </u-header>
        <div style="height:1.2rem"></div>
        <div class="item-list" v-for="item in list">
            <div class="title">{{item.type}}</div>
            <div class="item" v-for="dtl in item.helps">
                <a :href="dtl.url" class="row">{{dtl.title}}（{{dtl.addtime}}）</a>
            </div>
        </div>
    </div>
</template>

<script>
import { helpList } from '@/api/user.js'
export default {
  data() {
    return {
      list: []
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      helpList().then(res => {
        this.list = res.data
      })
    }
  }
}
</script>

<style scoped lang="scss">
.item-list {
  font-size: .43rem;
  color: rgb(51, 51, 51);
  .title {
    background: rgb(243, 243, 243);
    color: rgb(0, 0, 0);
    padding: 0.426667rem 0 0.266667rem 0.266667rem;
  }
  .item {
    background: #fff;
    padding: 0 0.266667rem;
    .row {
      padding: .43rem 0;
      border-bottom: 1px solid #ededed;
    }
    .row:last-child {
      border-bottom: 0;
    }
  }
}
</style>