<template>
    <div>

    </div>
</template>

<script>
export default {
  created() {
    this.$store.dispatch('GetInfo').then(res => {
      switch (res.data.role_code) {
        case '001'://现场管理员
            this.$router.push({
                name:'admin-index'
            })
          break;
        case '002'://驾驶员
          break;
        case '003'://土尾管理员
          break;
        case '004'://部门经理
          break;
        case '005'://老板
          break;
        default:
          AlertModule.show({
            title: '提示',
            content: '未分配角色信息',
          })
          return
          break;
      }
    })
  }
}
</script>

<style scoped>
</style>