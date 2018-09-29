const _import = require('@/router/_import_' + process.env.NODE_ENV)
//模块路由
export default [
    
  { name: 'home', path: '/', component: _import('home/index'), meta: { verifylogin: true } },
  { name: 'audit', path: '/audit', component: _import('home/audit'), meta: { verifylogin: false } },
  { name: 'means', path: '/means', component: _import('home/means'), meta: { verifylogin: false } },
  { name: 'oauth', path: '/oauth', component: _import('home/oauth'), meta: { verifylogin: false } },
  { name: 'admin-index', path: '/admin-index', component: _import('home/admin-index'), meta: { verifylogin: true } },
  ]