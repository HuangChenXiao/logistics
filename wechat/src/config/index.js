
var formatDate = function (d = new Date()) {//返回年月日
    var now = d;
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var date = now.getDate();
    month = month < 10 ? "0" + month : month;
    date = date < 10 ? "0" + date : date;
    return year + "-" + month + "-" + date;
}
var formatNoDate = function (d = new Date()) {//返回年月日
    var now = d;
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var date = now.getDate();
    month = month < 10 ? "0" + month : month;
    date = date < 10 ? "0" + date : date;
    return year + "" + month + "" + date;
}
var formatTime = function (d = new Date()) {//返回年月日时分秒
    var now = d;
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var date = now.getDate();
    var hour = now.getHours() < 10 ? "0" + now.getHours() : now.getHours();
    var minute = now.getMinutes() < 10 ? "0" + now.getMinutes() : now.getMinutes();
    var second = now.getSeconds() < 10 ? "0" + now.getSeconds() : now.getSeconds();
    return year + "-" + month + "-" + date + " " + hour + ":" + minute + ":" + second;
}
var formatOrderNo = function (d = new Date()) {//返回定单号
    var now = d;
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var date = now.getDate();
    var hour = now.getHours() < 10 ? "0" + now.getHours() : now.getHours();
    var minute = now.getMinutes() < 10 ? "0" + now.getMinutes() : now.getMinutes();
    var second = now.getSeconds() < 10 ? "0" + now.getSeconds() : now.getSeconds();
    var Milliseconds = now.getMilliseconds();
    return "WX" + year + month + date + hour + minute + second + Milliseconds;
}
export default {
    formatDate,
    formatTime,
    formatNoDate,
    formatOrderNo
}