<template>
    <div>
        <div id="container" style="height:500px"></div>
    </div>
</template>

<script>
export default {
  mounted() {
    // 创建地图实例
    var map = new AMap.Map("container", {
      zoom: 13,
      center: [118.043129, 24.565805],
      resizeEnable: true
    });

    // 创建点覆盖物
    var marker = new AMap.Marker({
      position: new AMap.LngLat(118.043129, 24.565805),
      icon:"//a.amap.com/jsapi_demos/static/demo-center/icons/poi-marker-default.png"
    });
    map.add(marker);
  }
};
</script>

<style>
.amap-icon img {
            width: 25px;
            height: 34px;
        }
</style>