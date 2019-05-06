import request from '@/utils/request'

export function DuiDuiJi(data) {
    return request({
        url: 'DuiDuiJi',
        method: "get",
        params: data
    })
}