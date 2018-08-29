import request from '@/utils/request'

export function userInfo(data) {
    return request({
        url: 'gs/app/user/userInfo.doGs',
        method: "post",
        data: data
    })
}

export function userTradetype(data) {
    return request({
        url: 'gs/app/user/userTradetype.doGs',
        method: "post",
        data: data
    })
}
export function getOrderMyList(data) {
    return request({
        url: 'gs/app/user/getOrderMyList.doGs',
        method: "post",
        data: data
    })
}

export function getAdMyList(data) {
    return request({
        url: 'gs/app/user/getAdMyList.doGs',
        method: "post",
        data: data
    })
}

export function userQrcodelist(data) {
    return request({
        url: 'gs/app/user/userQrcodelist.doGs',
        method: "post",
        data: data
    })
}
export function helpList(data) {
    return request({
        url: 'gs/app/common/helpList.doGs',
        method: "post",
        data: data
    })
}

export function userAddqrcode(data) {
    return request({
        url: 'gs/app/user/userAddqrcode.doGs',
        method: "post",
        data: data
    })
}

export function userDelqrcode(data) {
    return request({
        url: 'gs/app/user/userDelqrcode.doGs',
        method: "post",
        data: data
    })
}

export function adUpdate(data) {
    return request({
        url: 'gs/app/ad/adUpdate.doGs',
        method: "post",
        data: data
    })
}

export function memberOnline(data) {
    return request({
        url: 'gs/app/user/memberOnline.doGs',
        method: "post",
        data: data
    })
}
