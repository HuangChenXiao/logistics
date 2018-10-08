import request from '@/utils/request'

export function WeChatAccredit(data) {
    return request({
        url: 'WeChat/oauth/WeChatAccredit.ashx',
        method: "get",
        params: data
    })
}