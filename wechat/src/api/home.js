import request from '@/utils/request'

export function PostWechatUser(data) {
    return request({
        url: 'wechatUser',
        method: "post",
        data
    })
}
export function wechatUser(data) {
    return request({
        url: 'wechatUser',
        method: "get"
    })
}
export function GongDiInfo(data) {
    return request({
        url: 'GongDiInfo',
        method: "get",
        params: data
    })
}
export function CheLiangInfo(data) {
    return request({
        url: 'CheLiangInfo',
        method: "get",
        params: data
    })
}
export function XieZuoDanWeiInfo(data) {
    return request({
        url: 'XieZuoDanWeiInfo',
        method: "get",
        params: data
    })
}
export function XianLuInfo(data) {
    return request({
        url: 'XianLuInfo',
        method: "get",
        params: data
    })
}
export function GongChengCheDingDan(data) {
    return request({
        url: 'GongChengCheDingDan',
        method: "post",
        data
    })
}
export function GetGongChengCheDingDan(data) {
    return request({
        url: 'GongChengCheDingDan',
        method: "get",
        params: data
    })
}

