import request from '@/utils/request'

export function DriverCheLiang(data) {
    return request({
        url: 'DriverCheLiang',
        method: "get"
    })
}
export function ShangBanLeiBie(data) {
    return request({
        url: 'ShangBanLeiBie',
        method: "get"
    })
}
