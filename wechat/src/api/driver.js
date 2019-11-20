import request from '@/utils/request'

export function DriverCheLiang(data) {
    return request({
        url: 'DriverCheLiang',
        method: "get",
        params: data
    })
}
export function ShangBanLeiBie(data) {
    return request({
        url: 'ShangBanLeiBie',
        method: "get"
    })
}
export function OrderNumberCount(data) {
    return request({
        url: 'OrderNumberCount',
        method: "get",
        params: data
    })
}

export function BangDingJiLiQrCode(data) {
    return request({
        url: 'BangDingJiLiQrCode',
        method: "get",
        params: data
    })
}

