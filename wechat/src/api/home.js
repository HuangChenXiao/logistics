import request from '@/utils/request'

export function WeChatAccredit(data) {
    return request({
        url: 'WeChat/oauth/WeChatAccredit.ashx',
        method: "get",
        params: data
    })
}
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