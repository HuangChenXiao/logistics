import request from '@/utils/request'

export function adCreate(data) {
    return request({
        url: 'gs/app/ad/adCreate.doGs',
        method: "post",
        data: data
    })
}
export function getAdList(data) {
    return request({
        url: 'gs/app/ad/getAdList.doGs',
        method: "post",
        data: data
    })
}
export function userAmountQrcode(data) {
    return request({
        url: 'gs/app/ad/userAmountQrcode.doGs',
        method: "post",
        data: data
    })
}

export function adNewprice(data) {
    return request({
        url: 'gs/app/common/adNewprice.doGs',
        method: "post",
        data: data
    })
}

export function getOrderBuyerinfo(data) {
    return request({
        url: 'gs/app/order/getOrderBuyerinfo.doGs',
        method: "post",
        data: data
    })
}

export function orderCancel(data) {
    return request({
        url: 'gs/app/order/orderCancel.doGs',
        method: "post",
        data: data
    })
}

export function orderBuyerconfirm(data) {
    return request({
        url: 'gs/app/order/orderBuyerconfirm.doGs',
        method: "post",
        data: data
    })
}

export function userAmountQrcode2(data) {
    return request({
        url: 'gs/app/order/appOrderCreate.doGs',
        method: "post",
        data: data
    })
}

export function orderSellerconfirm(data) {
    return request({
        url: 'gs/app/order/orderSellerconfirm.doGs',
        method: "post",
        data: data
    })
}