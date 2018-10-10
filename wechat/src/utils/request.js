import axios from 'axios'
import store from '../store'
import { getToken } from '@/utils/auth'

// 创建axios实例
const service = axios.create({
  baseURL: process.env.BASE_API, // api的base_url
  timeout: 15000 // 请求超时时间
})
// request拦截器
service.interceptors.request.use(config => {
  // if (getToken()) {
  //   config.headers['Authorization'] = getToken() // 让每个请求携带自定义token 请根据实际情况自行修改
  // }
  var openid = localStorage.getItem('openid');
  if (openid) {
    config.headers['openid'] = openid // 让每个请求携带自定义token 请根据实际情况自行修改
  }
  // if (config.data && !config.upload_type) {//upload_type：为true时，不需要转义，上传图片表单类型
  //   config.data = `data=${encodeURIComponent(JSON.stringify(config.data))}`
  // }
  // config.headers['Content-Type'] = "application/x-www-form-urlencoded; charset=UTF-8";
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
    if (res.status_code == 402) {
      localStorage.clear()
      location.reload()
    }
    return Promise.reject(res)
  }
)

export default service
