
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
// 获取当前月的第一天

function getCurrentMonthFirst(d = new Date()) {
    var now = d;
    now.setDate(1);
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    month = month < 10 ? "0" + month : month;
    day = day < 10 ? "0" + day : day;
    return year + "-" + month + "-" + day;
}
// 获取当前月的最后一天
function getCurrentMonthLast(d = new Date()) {
    var date = d;
    var currentMonth = date.getMonth();
    var nextMonth = ++currentMonth;
    var nextMonthFirstDay = new Date(date.getFullYear(), nextMonth, 1);
    var oneDay = 1000 * 60 * 60 * 24;
    var now= new Date(nextMonthFirstDay - oneDay);
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    month = month < 10 ? "0" + month : month;
    day = day < 10 ? "0" + day : day;
    return year + "-" + month + "-" + day;
}
export default {
    formatDate,
    formatTime,
    formatNoDate,
    formatOrderNo,
    getCurrentMonthFirst,
    getCurrentMonthLast
}