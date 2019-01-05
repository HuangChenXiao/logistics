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

export function EditGongDiInfo(data) {
    return request({
        url: 'GongDiInfo',
        method: "post",
        data
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

export function WaJueJiDingDan(data) {
    return request({
        url: 'WaJueJiDingDan',
        method: "post",
        data
    })
}
export function GetWaJueJiDingDan(data) {
    return request({
        url: 'WaJueJiDingDan',
        method: "get",
        params: data
    })
}
export function CheLiang(data) {
    return request({
        url: 'CheLiang',
        method: "get",
        params: data
    })
}

export function BangDingJiLu(data) {
    return request({
        url: 'BangDingJiLu',
        method: "get",
        params: data
    })
}

export function GongDiCheLiang(data) {
    return request({
        url: 'GongDiCheLiang',
        method: "get",
        params: data
    })
}
export function GongDiXianLu(data) {
    return request({
        url: 'GongDiXianLu',
        method: "get",
        params: data
    })
}

export function EndWaJueJiDingDan(data) {
    return request({
        url: 'EndWaJueJiDingDan',
        method: "get",
        params: data
    })
}

export function ConfirmOrder(data) {
    return request({
        url: 'ConfirmOrder',
        method: "get",
        params: data
    })
}
export function DesEndOrder(data) {
    return request({
        url: 'DesEndOrder',
        method: "get",
        params: data
    })
}
export function HeadTailOrderCount(data) {
    return request({
        url: 'HeadTailOrderCount',
        method: "get",
        params: data
    })
}
export function TuWeiInfo(data) {
    return request({
        url: 'TuWeiInfo',
        method: "get",
        params: data
    })
}
export function EditTuWeiInfo(data) {
    return request({
        url: 'TuWeiInfo',
        method: "post",
        data
    })
}
export function getChePaiTuWei(data) {
    return request({
        url: 'getChePaiTuWei',
        method: "get",
        params: data
    })
}
export function UpdateTuWeriSortDate(data) {
    return request({
        url: 'UpdateTuWeriSortDate',
        method: "get",
        params: data
    })
}

