import axios from 'axios'
import store from '../store'
import { getToken } from '@/utils/auth'

// 创建axios实例
const service = axios.create({
  baseURL: process.env.WECHAT_API, // api的base_url
  timeout: 15000 // 请求超时时间
})
// request拦截器
service.interceptors.request.use(config => {
  return config
}, error => {
  // Do something with request error
  console.log(error) // for debug
  Promise.reject(error)
})

// respone拦截器
service.interceptors.response.use(
  response => {
    /**
    * code为非20000是抛错 可结合自己业务进行修改
    */
    const res = response.data
    return res
  },
  error => {
    console.log('err' + error)// for debug
    var res = error.response.data;
    return Promise.reject(res)
  }
)

export default service
