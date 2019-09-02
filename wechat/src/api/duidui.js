import request from '@/utils/request'

export function DuiDuiJi(data) {
    return request({
        url: 'DuiDuiJi',
        method: "get",
        params: data
    })
}

export function EditbPrint(data) {
    return request({
        url: 'EditbPrint',
        method: "get",
        params: data
    })
}
