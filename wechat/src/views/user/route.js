const _import = require('@/router/_import_' + process.env.NODE_ENV)
//模块路由
export default [
    
  { name: 'admin-user', path: '/admin-user', component: _import('user/admin-user'), meta: { verifylogin: true } },
  { name: 'user', path: '/user', component: _import('user/index'), meta: { verifylogin: true } },
  { name: 'advertisement', path: '/user/advertisement', component: _import('user/advertisement'), meta: { verifylogin: true } },
  { name: 'help', path: '/user/help', component: _import('user/help'), meta: { verifylogin: true } },
  { name: 'order', path: '/user/order', component: _import('user/order'), meta: { verifylogin: true } },
  { name: 'wages', path: '/user/wages', component: _import('user/wages'), meta: { verifylogin: true } },
  { name: 'transaction', path: '/user/transaction', component: _import('user/transaction'), meta: { verifylogin: true } },
  { name: 'transaction-setting', path: '/user/transaction/transaction-setting', component: _import('user/transaction-setting'), meta: { verifylogin: true } },
  { name: 'setting', path: '/user/setting', component: _import('user/setting'), meta: { verifylogin: true } },
  { name: 'download', path: '/user/download', component: _import('user/download'), meta: { verifylogin: true } },
  ]