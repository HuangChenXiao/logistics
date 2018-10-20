let orderType = value => {
    var valMap = {
        0: '等待付款',
        1: '等待放币',
        2: '交易完成',
        3: '已取消',
    }
    return valMap[value]
}

let workMethod = value => {
    var valMap = {
        0: '休息中',
        1: '正在上班',
    }
    return valMap[value]
}

export { orderType,workMethod }