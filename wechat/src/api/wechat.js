import request from '@/utils/wechat_request'

export function WeChatAccredit(data) {
    return request({
        url: 'WeChat/oauth/WeChatAccredit.ashx',
        method: "get",
        params: data
    })
}
export function TemplateMsg(data) {
    return request({
        url: 'WeChat/TemplateAlert/TemplateMsg.ashx',
        method: "get",
        params: data
    })
}