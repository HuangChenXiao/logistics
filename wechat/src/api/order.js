import request from '@/utils/request'

export function getFrontOrderList(data) {
    return request({
        url: 'front/order/getFrontOrderList.doAdminJJ',
        method: "post",
        data: data
    })
}

export function getFrontOrdersnPay(data) {
    return request({
        url: 'front/order/getFrontOrdersnPay.doAdminJJ',
        method: "post",
        data: data
    })
}
