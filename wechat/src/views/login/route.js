const _import = require('@/router/_import_' + process.env.NODE_ENV)
//模块路由
export default [
  { name: 'login', path: '/login', component: _import('login/index'), meta: { verifylogin: false } },
  ]