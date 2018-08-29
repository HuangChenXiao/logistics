import request from '@/utils/request'

export function login(data) {
    return request({
        url: 'gs/app/user/login.doGs',
        method: "post",
        data: data
    })
}