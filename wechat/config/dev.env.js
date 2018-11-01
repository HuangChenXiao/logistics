'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  BASE_API: '"http://localhost:59358/api/"',
  WECHAT_API: '"http://localhost:59358/"',
  // BASE_API: '"http://test.chaomafu.com/api/API/"',
  // WECHAT_API: '"http://test.chaomafu.com/API/"',
  IMG_URL: '""',
})
