
import { getToken, setToken, removeToken } from '@/utils/auth'
import { wechatUser } from '@/api/home.js'

const user = {
  state: {
    token: getToken(),
    user_info: localStorage.getItem("user_info") != null ? JSON.parse(localStorage.getItem("user_info")) : {},//用户信息
    gongdi_info: {},//工地名称
    cChePaiHao: localStorage.getItem("cChePaiHao")!= null ? JSON.parse(localStorage.getItem("cChePaiHao")) : {},//车辆信息
    xianding_chelaing:localStorage.getItem('xianding_chelaing')=="true"?Boolean(localStorage.getItem('xianding_chelaing')):false,//限定车辆
  },

  mutations: {
    SET_TOKEN: (state, token) => {
      state.token = token
    },
    SET_USER_INFO: (state, user_info) => {
      state.user_info = user_info
    },
    SET_gongdi_info: (state, gongdi_info) => {
      state.gongdi_info = gongdi_info
    }, 
    SET_cChePaiHao: (state, cChePaiHao) => {
      state.cChePaiHao = cChePaiHao
    }, 
    SET_Xianding_Chelaing: (state, xianding_chelaing) => {
      state.xianding_chelaing = xianding_chelaing
    }
  },

  actions: {
    //限定车辆
    SXianding_Chelaing({ commit }, val) {
      return new Promise((resolve, reject) => {
        commit('SET_Xianding_Chelaing', val)
        localStorage.setItem('xianding_chelaing',val)
        resolve(val)
      })
    },
    //工地名称
    SGongDiMingCheng({ commit }, val) {
      return new Promise((resolve, reject) => {
        commit('SET_gongdi_info', val)
        resolve(val)
      })
    },
    // 登录
    Login({ commit }, userInfo) {
      return new Promise((resolve, reject) => {
        login({ "mobile": userInfo.mobile, "password": userInfo.password }).then(res => {
          setToken(res.data.token)
          commit('SET_TOKEN', res.data.token)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    },
    ValidateUser({ commit }) {
      return new Promise((resolve, reject) => {
        wechatUser().then(res => {
          setToken(res.data.token)
          commit('SET_TOKEN', res.data.token)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    },
    setcChePaiHao({ commit }, cChePaiHao) {
      return new Promise((resolve, reject) => {
        commit('SET_cChePaiHao', JSON.parse(cChePaiHao))
        localStorage.setItem('cChePaiHao',cChePaiHao)
        resolve(cChePaiHao)
      })
    },
    setToken({ commit }, token) {
      return new Promise((resolve, reject) => {
        if (token) {
          commit('SET_TOKEN', token)
          resolve(token)
        }
        else {
          reject('null token')
        }
      })
    },
    // 获取用户信息
    GetInfo({ commit, state }) {
      return new Promise((resolve, reject) => {
        wechatUser({}).then(res => {
          commit('SET_USER_INFO', res.data)
          resolve(res)
        }).catch(error => {
          reject(error)
        })
      })
    },
    // 前端 登出
    FedLogOut({ commit }) {
      return new Promise(resolve => {
        commit('SET_TOKEN', '')
        removeToken()
        resolve()
      })
    }
  }
}

export default user
