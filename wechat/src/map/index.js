
import store from '@/store'

const getformattedAddress = resData => {
    return new Promise(function (resolve, reject) {
        $.post("https://mobile.xmxtm.cn/API/WeChat/JS-SDK/GetJS_SDK_Config.ashx", { "action": "SDK_Config", "windowurl": resData.windowurl }, function (data) {
            data = JSON.parse(data);
            // alert(JSON.stringify(data))
            wx.config({
                debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                appId: data.appId, // 必填，公众号的唯一标识
                timestamp: data.timeStamp, // 必填，生成签名的时间戳
                nonceStr: data.nonceStr, // 必填，生成签名的随机串
                signature: data.signature,// 必填，签名，见附录1
                jsApiList: ["getLocation"] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
            });
            wx.ready(function () {
                //获取地理位置接口
                wx.getLocation({
                    type: 'gcj02', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                    success: function (res) {
                        var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                        var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                        var speed = res.speed; // 速度，以米/每秒计
                        var accuracy = res.accuracy; // 位置精度
                        var latLng = new qq.maps.LatLng(latitude, longitude);
                        var wxMapInfo = {}
                        wxMapInfo.lat = res.latitude
                        wxMapInfo.lng = res.longitude
                        //调用获取位置方法
                        var geocoder = new qq.maps.Geocoder({
                            complete: function (result) {
                                wxMapInfo.address = result.detail.address
                                resolve(wxMapInfo)
                            }
                        });
                        geocoder.getAddress(latLng);
                        //若服务请求失败，则运行以下函数
                        geocoder.setError(function () {
                            alert("出错了，请输入正确的经纬度！！！");
                        });
                    },
                    fail: function (res) {
                        reject(res)
                        // location.reload()
                        alert("定位错误，请检查手机GPS功能是否开启！" + JSON.stringify(res));
                    }
                });
            })
        })
    })
}
export default getformattedAddress