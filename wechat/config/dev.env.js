'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  BASE_API: '"http://localhost:59358/api/"',
  WECHAT_API: '"http://localhost:59358/"',
  // BASE_API: '"https://mobile.xmxtm.cn/api/API/"',
  // WECHAT_API: '"https://mobile.xmxtm.cn/API/"',
  IMG_URL: '""',
})
