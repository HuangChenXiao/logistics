
import store from '@/store'
const getformattedAddress = new Promise(function (resolve, reject) {
    var map, geolocation
    //加载地图，调用浏览器定位服务
    map = new AMap.Map('container', {
        resizeEnable: true
    })
    map.plugin('AMap.Geolocation', function () {
        geolocation = new AMap.Geolocation({
            enableHighAccuracy: true, //是否使用高精度定位，默认:true
            timeout: 10000, //超过10秒后停止定位，默认：无穷大
            buttonOffset: new AMap.Pixel(10, 20), //定位按钮与设置的停靠位置的偏移量，默认：Pixel(10, 20)
            zoomToAccuracy: true, //定位成功后调整地图视野范围使定位位置及精度范围视野内可见，默认：false
            buttonPosition: 'RB'
        })
        map.addControl(geolocation)
        geolocation.getCurrentPosition()
        AMap.event.addListener(geolocation, 'complete', onComplete) //返回定位信息
        AMap.event.addListener(geolocation, 'error', onError) //返回定位出错信息
    })
    //解析定位结果
    function onComplete(data) {
        let lng = data.position.getLng()
        let lat = data.position.getLat()
        var geocoder = new AMap.Geocoder({
            radius: 1000,
            extensions: 'all'
        })
        geocoder.getAddress([lng, lat], function (status, result) {
            if (status === 'complete' && result.info === 'OK') {
                store.dispatch("setformattedAddress", result.regeocode.formattedAddress)
                store.dispatch("setlongitude", lng)
                store.dispatch("setlatitude", lat)
                var lnglat = {}
                lnglat.longitude = lng;
                lnglat.latitude = lat
                var lnglatXY = [lng, lat]; //已知点坐标
                var geocoder = new AMap.Geocoder({
                    radius: 1000,
                    extensions: "all"
                });
                geocoder.getAddress(lnglatXY, function (status, result) {
                    if (status === 'complete' && result.info === 'OK') {
                        resolve(result)
                    }
                    else {
                        resolve(result)
                    }
                });

            }
        })
    }
    //解析定位错误信息
    function onError(data) {
        console.log('定位失败')
        reject(data)
    }
})
export default getformattedAddress